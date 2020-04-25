using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CapaEntidades;
using CapaNegocioAPP;

namespace XamarinAPP.Pages.Replanteo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReplanteoFinalizacion : basePage
    {
        private string codigoFinalizacion = "";
        public ReplanteoFinalizacion()
        {
            InitializeComponent();
            checkValidaciones();
        }

        private async void btnFinalizar_Clicked(object sender, EventArgs e)
        {
            if (await checkValidaciones())
            {
                finalizarReplanteo();
            }
            else
            {
                await DisplayAlert("Aviso", "No se puede finalizar la intervención mientras haya pasos sin validar. Contacte con TC Group", "Volver");
            }
        }

        private async void btnNoTerminada_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IntervencionNoTerminadaPage());
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            await checkValidaciones();
        }

        private async Task<bool> checkValidaciones()
        {
            bool validado = false;
            try
            {
                IntervencionFinalizacionCE oFinalizacion = await new ReplanteoCRN_APP().getReplanteoFinalizacionByIntervencion(App.oIntervencion.idIntervencion);
                if (oFinalizacion != null)
                {
                    codigoFinalizacion = oFinalizacion.codigoFinalizacion;

                    chkFirma.IsChecked = oFinalizacion.firmaValidado;
                    chkFotos.IsChecked = oFinalizacion.fotografiasValidado;
                    chkMedidas.IsChecked = oFinalizacion.medidasValidado;

                    chkMedidas.Color = chkMedidas.IsChecked ? Color.Green : Color.Black;
                    chkFirma.Color = chkFirma.IsChecked ? Color.Green : Color.Black;
                    chkFotos.Color = chkFotos.IsChecked ? Color.Green : Color.Black;

                    validado = oFinalizacion.firmaValidado && oFinalizacion.fotografiasValidado && oFinalizacion.medidasValidado;
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Volver");
            }
            return validado;
        }

        private async void finalizarReplanteo()
        {

            if (codigoFinalizacion == (txtCodigo.Text ?? ""))
            {
                IntervencionFinalizacionCE oFinalizacion = new IntervencionFinalizacionCE();
                oFinalizacion.idIntervencion = App.oIntervencion.idIntervencion;
                oFinalizacion.tecnico = App.oTecnico.tecnico;
                oFinalizacion.telefonoTecnico = App.oTecnico.telefonoTecnico;
                oFinalizacion.idEstado = 5;
                
                oFinalizacion = await new ReplanteoCRN_APP().replanteoFinalizacionActualizarEstado(oFinalizacion);
                await DisplayAlert("Intervencion Finalizada", oFinalizacion.conclusionIntervencion, "OK");


                //Eliminamos toda la navegacion anterior
                Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack.First());
                await Navigation.PopToRootAsync();                
            }
            else
            {
                await DisplayAlert("Error", "Código de finalización no válido", "Volver");
            }


        }

        private void btnVolver_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}