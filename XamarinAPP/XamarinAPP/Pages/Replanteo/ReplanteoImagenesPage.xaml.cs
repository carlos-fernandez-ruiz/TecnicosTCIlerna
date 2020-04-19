using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CapaEntidades;
using CapaNegocioAPP;
using Plugin.Media;
using Xamarin.Essentials;
using System.Threading;
using Plugin.Media.Abstractions;

namespace XamarinAPP.Pages.Replanteo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReplanteoImagenesPage : basePage
    {
        //List<MediaFile> lstImagenes = new List<MediaFile>();
        Dictionary<MediaFile, int> dictImagenes = new Dictionary<MediaFile, int>();
        public ReplanteoImagenesPage()
        {
            
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargarComboTipoImagenes();

        }

        private async void cargarComboTipoImagenes()
        {
            List<ImagenTiposCE> lstImagenesTipos = await new ReplanteoCRN_APP().getImagenesTipos();
            pckTipoImagenes.ItemDisplayBinding = new Binding("descripcionTipoImagen");
            pckTipoImagenes.ItemsSource = lstImagenesTipos;
        }

        private async void btnHacerFoto_Clicked(object sender, EventArgs e)
        {
            if (pckTipoImagenes.SelectedItem == null)
            {
                await DisplayAlert("Aviso", "Seleccionar tipo de imagen", "Volver");
            }
            else
            {
                ImagenTiposCE tipoImagen = (ImagenTiposCE)pckTipoImagenes.SelectedItem;
                int indx = pckTipoImagenes.SelectedIndex;
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Sin camara", "La camara no está disponible.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "TCGroup",
                    Name = App.oIntervencion.idIntervencion.ToString() + "_" + DateTime.Now + "_" + PhotoSize.Medium + ".jpg",
                    SaveToAlbum = true,
                    CompressionQuality = 92,
                    PhotoSize = PhotoSize.Medium,
                    DefaultCamera = CameraDevice.Rear
                });
                pckTipoImagenes.SelectedIndex = indx;
                if (file == null)
                    return;

                //lstImagenes.Add(file);
                dictImagenes.Add(file, tipoImagen.idTipoImagen);                
                
                imagenPrueba.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

            }
        }

        private async void btnGaleria_Clicked(object sender, EventArgs e)
        {
            if (pckTipoImagenes.SelectedItem == null)
            {
                await DisplayAlert("Aviso", "Seleccionar tipo de imagen", "Volver");
            }
            else
            {
                int indx = pckTipoImagenes.SelectedIndex;
                ImagenTiposCE tipoImagen = (ImagenTiposCE)pckTipoImagenes.SelectedItem;
                await CrossMedia.Current.Initialize();
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {                    
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,                    
                    CompressionQuality = 92

                });                
                pckTipoImagenes.SelectedIndex = indx;
                if (file == null)
                    return;

                //lstImagenes.Add(file);

                dictImagenes.Add(file, tipoImagen.idTipoImagen);
                
                imagenPrueba.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

            }

        }

        private async void btnSubirImagen_Clicked(object sender, EventArgs e)
        {
            try
            {
                foreach (KeyValuePair<MediaFile, int> imagen in dictImagenes)
                {                   

                    string nombreImagen = System.IO.Path.GetFileName(imagen.Key.Path);
                    ImagenCE oImagen = new ImagenCE();
                    oImagen.idIntervencion = App.oIntervencion.idIntervencion;
                    oImagen.idUsuario = App.oUsuarioLogged.idUsuario;
                    oImagen.idTipoImagen = imagen.Value;
                    oImagen.tecnico = App.oTecnico.tecnico;
                    oImagen.comentario = txtComentario.Text == null ? "" : txtComentario.Text;
                    oImagen.telefonoTecnico = App.oTecnico.telefonoTecnico;

                    spinnerEnviarImagen.IsVisible = true;
                    spinnerEnviarImagen.IsRunning = true;
                    string resultado = await new ReplanteoCRN_APP().enviarImagenReplanteo(imagen.Key.GetStream(), nombreImagen, imagen.Key.Path, oImagen);
                    spinnerEnviarImagen.IsVisible = false;
                    spinnerEnviarImagen.IsRunning = false;
                    if (resultado != "OK")
                    {
                        await DisplayAlert("Aviso", "Se ha producido un error al subir la imagen " + nombreImagen, "Volver" );
                    } 
                    else
                    {
                        await DisplayAlert("Aviso", "Imágen cargada correctamente.", "Volver");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Volver");
            }

        }

        private async void btnVerListadoImagenes_Clicked(object sender, EventArgs e)
        {
            popupListView.IsVisible = true;
            imagenPrueba.IsVisible = false;
            List<ImagenCE> lstImagenesCE = await new ReplanteoCRN_APP().getImagenesByIntervencion(App.oIntervencion.idIntervencion);
            var result = lstImagenesCE.GroupBy(s => s.descripcion).Select(g => new { tipoImagen = g.Key, numeroImagenes = "Número de fotos: " + g.Count().ToString() });
            lstImagenes.ItemsSource = result;
        }

        private void btnVolver_Clicked(object sender, EventArgs e)
        {
            popupListView.IsVisible = false;
            imagenPrueba.IsVisible = true;
            lstImagenes.ItemsSource = null;
        }

        private void btnSiguiente_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IntervencionFirmaPage());
        }

        private async void btnAtras_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}