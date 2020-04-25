using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using CapaEntidades;
using CapaNegocioAPP;

namespace XamarinAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class basePage : ContentPage
    {
        public basePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            getLocalizacion();
        }

        protected void getLocalizacion()
        {
            try
            {
                if (App.ultimaLocalizacion != null)
                {
                    //Comprobamos que hayan pasado al menos 5 minutos desde la ultima vez que guardamos la localizacion
                    if ((DateTime.Now - App.ultimaLocalizacion).TotalMinutes >= 5)
                    {
                       actualizarLocalizacion();
                    }
                } 
                else
                {
                    actualizarLocalizacion();
                }                
                  
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async Task actualizarLocalizacion()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            LocalizacionCE oLocalizacionCE = null;

            //la localizacion debe ser mas reciente de 5 minutos
            if (location != null && location.Timestamp > DateTime.Now.AddMinutes(-5))
            {
                oLocalizacionCE = getLocalizacionCE(location);
            }
            else
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var currentLocation = await Geolocation.GetLocationAsync(request);
                if (currentLocation != null)
                {
                    oLocalizacionCE = getLocalizacionCE(currentLocation);
                }
            }

            if (oLocalizacionCE != null)
            {
                new IntervencionCRN_APP().insertarLocalizacion(oLocalizacionCE);
                App.ultimaLocalizacion = DateTime.Now;
            }
        }

        private LocalizacionCE getLocalizacionCE(Location localizacion)
        {
            LocalizacionCE oLocalizacion = new LocalizacionCE();
            oLocalizacion.accuracy = localizacion.Accuracy == null  ? 0 : (double)localizacion.Accuracy;
            oLocalizacion.latitude = localizacion.Latitude;
            oLocalizacion.fecha = localizacion.Timestamp.UtcDateTime.ToLocalTime();
            oLocalizacion.longitude = localizacion.Longitude;
            oLocalizacion.idIntervencion = App.oIntervencion.idIntervencion;
            oLocalizacion.tecnico = App.oTecnico.tecnico;
            oLocalizacion.telefonoTecnico = App.oTecnico.telefonoTecnico;
            return oLocalizacion;

        }
        
    }
}