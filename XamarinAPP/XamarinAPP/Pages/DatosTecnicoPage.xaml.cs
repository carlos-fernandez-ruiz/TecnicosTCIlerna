using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using CapaEntidades;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatosTecnicoPage : ContentPage
    {
        public DatosTecnicoPage()
        {
            InitializeComponent();
            cargarDatosTecnico();
        }

        private void cargarDatosTecnico()
        {
            string nombreTecnico = Preferences.Get("nombreTecnico", "");
            string telefono = Preferences.Get("telefonoTecnico", "");  
            if (nombreTecnico != "") nombreEntry.Text = nombreTecnico;
            if (telefono != "") telefonoEntry.Text = telefono;
        }

        private async void btnConfirmar_Clicked(object sender, EventArgs e)
        {
            string nombreTecnico = nombreEntry.Text;
            string telefono = telefonoEntry.Text;

            if (nombreTecnico!= null && nombreTecnico != "" && nombreTecnico != "Tu nombre" && telefono != null && telefono != "" && telefono != "Teléfono")
            {
                Preferences.Set("nombreTecnico", nombreTecnico);
                Preferences.Set("telefonoTecnico", telefono);
                guardarTecnicoApp();
                await Navigation.PushAsync(new IntervencionDescripcionPage());

            }
            else
            {
                await DisplayAlert("Error", "Debe introducir su nombre y teléfono de contacto", "Reintentar");
            }
        }

        private void guardarTecnicoApp()
        {
            TecnicoCE oTecnico = new TecnicoCE();
            oTecnico.tecnico = nombreEntry.Text;
            oTecnico.telefonoTecnico = telefonoEntry.Text;
            App.oTecnico = oTecnico;
        }

        protected override void OnAppearing()
        {
            cargarDatosTecnico();
            base.OnAppearing();
        }
    }
}