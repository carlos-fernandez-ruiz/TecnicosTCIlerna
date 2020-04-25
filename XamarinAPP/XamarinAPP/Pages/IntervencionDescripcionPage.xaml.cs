using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAPP
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntervencionDescripcionPage : basePage
    {
        public IntervencionDescripcionPage()
        {
            InitializeComponent();
            cargarDatosIntervencion();
        }

        private void cargarDatosIntervencion()
        {
            lblIdTienda.Text = "IdTienda: " + App.oIntervencion.idTienda;
            lblIdCliente.Text = "IdCliente: " + App.oIntervencion.idCliente;
            lblTienda.Text = "Tienda: " + App.oIntervencion.nombreTienda;
            lblCliente.Text = "Cliente: " + App.oIntervencion.cliente;
            lblDireccion.Text = "Dirección: " + App.oIntervencion.direccion;
            lblPoblacion.Text = "Población: " + App.oIntervencion.poblacion;
            lblTipoIntervencion.Text = "Tipo de intervención: " + Enum.GetName(typeof(App.tipoIntervencion), App.oIntervencion.idTipoIntervencion);
            lblIndicaciones.Text = "Indicaciones: " + App.oIntervencion.comentarioIntervencion;
        }

        private void btnIniciarIntervencion_Clicked(object sender, EventArgs e)
        {
            switch (App.oIntervencion.idTipoIntervencion)
            {
                case (int)App.tipoIntervencion.Replanteo:
                    //Navigation.InsertPageBefore(new XamarinAPP.Pages.Replanteo.ReplanteoMedidasPage(), this);
                    //Navigation.PushAsync(new XamarinAPP.Pages.Replanteo.ReplanteoMedidasPage(), true);        
                    //Navigation.PushModalAsync(new NavigationPage(new XamarinAPP.Pages.Replanteo.ReplanteoMedidasPage()));
                    Navigation.PushAsync(new XamarinAPP.Pages.Replanteo.ReplanteoMedidasPage());
                    break;
            }          
           
            
        }
    }
}