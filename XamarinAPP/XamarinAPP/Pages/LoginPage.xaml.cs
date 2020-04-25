using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CapaEntidades;
using CapaNegocioAPP;
using Xamarin.Essentials;
using System.Data;

namespace XamarinAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {			
			InitializeComponent();			
        }

		protected override bool OnBackButtonPressed()
		{
			return base.OnBackButtonPressed();
		}


		async void OnLoginButtonClicked(object sender, EventArgs e)
		{			
			if (await AreCredentialsCorrect(usernameEntry.Text, intervencionEntry.Text))
			{
				await Navigation.PushAsync(new  DatosTecnicoPage());				
			}
			else
			{
				messageLabel.Text = "Usuario o intervención incorrectos.";
				intervencionEntry.Text = string.Empty;
			}
		}


		async Task<bool> AreCredentialsCorrect(string usuario, string codigoIntervencion)
		{			
			try
			{
				(UsuariosCE oUsuarioCE, IntervencionCE oIntervencionCE) = await new UsuariosCRN_APP().getUsuarioIntervencionLogin(usuario, codigoIntervencion);
				if (oUsuarioCE != null && oIntervencionCE != null)
				{
					if (oIntervencionCE.idEstado == 5)
					{
						IntervencionFinalizacionCE oFinalizacion = new IntervencionFinalizacionCE(); ;
						switch (oIntervencionCE.idTipoIntervencion)
						{
							case (int)App.tipoIntervencion.Replanteo:
								oFinalizacion = await new ReplanteoCRN_APP().getReplanteoFinalizacionByIntervencion(oIntervencionCE.idIntervencion);
								break;

						}
						await DisplayAlert("Finalizada", "Esta intervención está finalizada con las siguientes conclusiones: " + Environment.NewLine + oFinalizacion.conclusionIntervencion, "Volver");
						return false;
					}
					else
					{
						App.IsUserLoggedIn = true;
						App.oUsuarioLogged = oUsuarioCE;
						App.oIntervencion = oIntervencionCE;
						return true;
					}
					
				}
				else
				{
					return false;
				}
			} 
			catch (Exception ex)
			{
				await DisplayAlert("Error", "No se ha podido conectar con el servidor.", "Volver");
				return false;
			}			
		}
	}
}