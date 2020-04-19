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

namespace XamarinAPP.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntervencionNoTerminadaPage : basePage
    {
        List<MediaFile> lstImagenes = new List<MediaFile>();
        public IntervencionNoTerminadaPage()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargarComboMotivos();
        }

        private async void cargarComboMotivos()
        {
            List<IntervencionNoTerminadaMotivosCE> lstMotivos = await new IntervencionCRN_APP().getMotivosIntervencionNoTerminada();
            pckMotivos.ItemDisplayBinding = new Binding("descripcion");
            pckMotivos.ItemsSource = lstMotivos;
        }

        private async void btnGaleria_Clicked(object sender, EventArgs e)
        {
            try
            {
                int indx = -1;
                if (pckMotivos.SelectedItem != null)
                {
                    indx = pckMotivos.SelectedIndex;
                }
                await CrossMedia.Current.Initialize();
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Large
                });
                if (indx >= 0) pckMotivos.SelectedIndex = indx;
                if (file == null)
                {
                    return;
                }
                else
                {
                    lstImagenes.Add(file);
                    actualizarConteoImagenes();
                }
                
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
                int indx = -1;
                if (pckMotivos.SelectedItem != null)
                {
                    indx = pckMotivos.SelectedIndex;
                }
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("Sin camara", "La camara no está disponible.", "OK");
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
                if (indx >= 0) pckMotivos.SelectedIndex = indx;
                
                if (file == null)
                {
                    return;
                } 
                else
                {
                    lstImagenes.Add(file);
                    actualizarConteoImagenes();
                }
                    

            } 
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Volver");
            }
        }

        private async void btnConfirmar_Clicked(object sender, EventArgs e)
        {
            try
            {
                IntervencionCRN_APP oIntervencionCRN = new IntervencionCRN_APP();
                if (pckMotivos.SelectedItem == null)
                {
                    await DisplayAlert("Error", "Es obligatorio introducir un motivo", "Volver");
                }
                else
                {
                    IntervencionNoTerminadaCE oNoterminada = new IntervencionNoTerminadaCE();
                    oNoterminada.idIntervencion = App.oIntervencion.idIntervencion;
                    oNoterminada.idUsuario = App.oUsuarioLogged.idUsuario;
                    oNoterminada.tecnico = App.oTecnico.tecnico;
                    oNoterminada.telefonoTecnico = App.oTecnico.telefonoTecnico;
                    oNoterminada.idMotivo = ((IntervencionNoTerminadaMotivosCE)pckMotivos.SelectedItem).idMotivo;
                    oNoterminada.observaciones = txtComentario.Text;

                    oNoterminada = await oIntervencionCRN.insertarIntervencionNoTerminada(oNoterminada);

                    if (oNoterminada != null && oNoterminada.idIntervencionNoTerminada != 0)
                    {
                        
                        foreach(MediaFile imagen in lstImagenes)
                        {
                            string nombreImagen = System.IO.Path.GetFileName(imagen.Path);
                            ImagenCE oImagen = new ImagenCE();
                            oImagen.idIntervencion = App.oIntervencion.idIntervencion;
                            oImagen.idUsuario = App.oUsuarioLogged.idUsuario;
                            oImagen.tecnico = App.oTecnico.tecnico;
                            oImagen.comentario = txtComentario.Text == null ? "" : txtComentario.Text;
                            oImagen.telefonoTecnico = App.oTecnico.telefonoTecnico;

                            await oIntervencionCRN.insertarImagenNoTerminada(imagen.GetStream(),nombreImagen, imagen.Path, oImagen, oNoterminada.idIntervencionNoTerminada);
                        }
                        lstImagenes.Clear();
                        actualizarConteoImagenes();
                    }
                }
            }
            catch (Exception ex)
            {
                actualizarConteoImagenes();
                await DisplayAlert("Error", ex.Message, "Volver");
            }
        }

        private void actualizarConteoImagenes()
        {
            lblNumeroImagenes.Text = "Imagenes seleccionadas: " + lstImagenes.Count();
        }
    }
}