using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CapaEntidades;

namespace XamarinAPP
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public static UsuariosCE oUsuarioLogged;
        public static TecnicoCE oTecnico;
        public static IntervencionCE oIntervencion { get; set; }
        public App()
        {
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        public enum tipoIntervencion
        {
            Instalación = 1,
            Replanteo = 2,
            Incidencia = 3,
            Desinstalación = 4
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
