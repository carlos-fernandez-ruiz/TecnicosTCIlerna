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


		async void OnLoginButtonClicked(object sender, EventArgs e)
		{			
			if (await AreCredentialsCorrect(usernameEntry.Text, intervencionEntry.Text))
			{
				Navigation.InsertPageBefore( new DatosTecnicoPage(), this);
				await Navigation.PopAsync();
			}
			else
			{
				messageLabel.Text = "El usuario o la intervención no coinciden";
				intervencionEntry.Text = string.Empty;
			}
		}


		async Task<bool> AreCredentialsCorrect(string usuario, string codigoIntervencion)
		{			
			(UsuariosCE oUsuarioCE, IntervencionCE oIntervencionCE) = await new UsuariosCRN_APP().getUsuarioIntervencionLogin(usuario, codigoIntervencion);
			if (oUsuarioCE != null && oIntervencionCE!= null)
			{
				App.IsUserLoggedIn = true;
				App.oUsuarioLogged = oUsuarioCE;
				App.oIntervencion= oIntervencionCE;
				return true;
			} 
			else
			{
				return false;
			}
		}
	}
}