using CapaEntidades;
using CapaNegocioAPP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAPP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntervencionFirmaPage : basePage
    {
        public const int _ID_TIPO_IMAGEN_FIRMA = 1;
        public const int _ID_TIPO_IMAGEN_PARTE = 1;
        public IntervencionFirmaPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.getImagenesCargadasAsync();
        }

        private async void btnConfirmar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (signatureView.Strokes.Count() > 0)
                {
                    Stream bitMap = await signatureView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
                    string nombreImagen = "Firma_" + App.oIntervencion.idIntervencion.ToString() + "_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";
                    ImagenCE oImagen = getImagenFromPage();                    
                    oImagen.idTipoImagen = _ID_TIPO_IMAGEN_FIRMA;                    

                    await new IntervencionCRN_APP().enviarImagenIntervencionFirma(bitMap, nombreImagen, nombreImagen, oImagen);
                    await DisplayAlert("Imagen subida", "Se ha enviado la firma correctamente.", "Volver");
                }
                else
                {
                    await DisplayAlert("Firma obligatoria", "Es obligatorio introducir una firma", "Volver");
                }
            } catch (Exception ex)
            {
                await DisplayAlert("Error",ex.Message,"Volver");
            }
            
        }

        private async void btnGaleria_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Large
                });
                
                if (file == null)
                    return;
                ImagenCE oImagen = getImagenFromPage();
                oImagen.idTipoImagen = _ID_TIPO_IMAGEN_PARTE;
                string nombreImagen = "Parte_" + App.oIntervencion.idIntervencion.ToString() + "_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";

                await new IntervencionCRN_APP().enviarImagenIntervencionFirma(file.GetStream(), nombreImagen, nombreImagen, oImagen);
                await DisplayAlert("Imagen subida", "Se ha enviado el parte correctamente.", "Volver");

            } 
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Volver");
            }
        }

        private async void btnHacerFoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "TCGroup",
                    Name = App.oIntervencion.idIntervencion.ToString() + "_" + DateTime.Now + "_" + PhotoSize.Large + ".jpg",
                    SaveToAlbum = true,
                    PhotoSize = PhotoSize.Large,
                    DefaultCamera = CameraDevice.Front
                });                
                if (file == null)
                    return;

                ImagenCE oImagen = getImagenFromPage();
                oImagen.idTipoImagen = _ID_TIPO_IMAGEN_PARTE;
                string nombreImagen = "Parte_" + App.oIntervencion.idIntervencion.ToString() + "_" + DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".png";

                await new IntervencionCRN_APP().enviarImagenIntervencionFirma(file.GetStream(), nombreImagen, nombreImagen, oImagen);
                await DisplayAlert("Imagen subida", "Se ha enviado el parte correctamente.", "Volver");

            } 
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Volver");
            }
        }

        private ImagenCE getImagenFromPage()
        {
            ImagenCE oImagen = new ImagenCE();
            oImagen.idIntervencion = App.oIntervencion.idIntervencion;
            oImagen.idUsuario = App.oUsuarioLogged.idUsuario;            
            oImagen.tecnico = App.oTecnico.tecnico;
            oImagen.comentario = observacionesEditor.Text == null ? "" : observacionesEditor.Text;
            oImagen.telefonoTecnico = App.oTecnico.telefonoTecnico;
            return oImagen;
        }

        private async void getImagenesCargadasAsync()
        {
            List<ImagenCE> lstImagenes = await new IntervencionCRN_APP().getImagenesFirmaByIntervencion(App.oIntervencion.idIntervencion);
            if(lstImagenes.Any(x=> x.idTipoImagen== _ID_TIPO_IMAGEN_FIRMA))
            {
                chkFirma.IsChecked = true;
                chkFirma.Color = Color.Green;
            }
            else
            {
                chkFirma.IsChecked = false;
                chkFirma.Color = Color.Red;
            }

            lblFotosAgregadas.Text = "Imágenes agregadas: " + lstImagenes.FindAll(x => x.idTipoImagen != _ID_TIPO_IMAGEN_FIRMA).Count().ToString();
            
        }

        private  void btnSiguiente_Clicked(object sender, EventArgs e)
        {
            switch (App.oIntervencion.idTipoIntervencion)
            {
                case (int)App.tipoIntervencion.Replanteo:
                    //Navigation.InsertPageBefore(new XamarinAPP.Pages.Replanteo.ReplanteoFinalizacion(), this);                    
                    Navigation.PushAsync(new Replanteo.ReplanteoFinalizacion());
                    break;
            }
            //Navigation.InsertPageBefore(new XamarinAPP.Pages.Replanteo.ReplanteoMedidasPage(), this);

        }

        private void btnAtras_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}