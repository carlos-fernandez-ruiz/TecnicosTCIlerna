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
    public partial class IntervencionDescripcionPage : ContentPage
    {
        public IntervencionDescripcionPage()
        {
            InitializeComponent();
            cargarDatosIntervencion();
        }

        private void cargarDatosIntervencion()
        {
            lblTienda.Text = "Tienda: " + App.oIntervencion.nombreTienda;
            lblCliente.Text = "Cliente: " + App.oIntervencion.cliente;
            lblDireccion.Text = "Dirección: " + App.oIntervencion.direccion;
            lblPoblacion.Text = "Población: " + App.oIntervencion.poblacion;
            lblTipoIntervencion.Text = "Tipo de intervención: " + Enum.GetName(typeof(App.tipoIntervencion), App.oIntervencion.idTipoIntervencion);
            lblIndicaciones.Text = "Indicaciones: " + App.oIntervencion.comentarioIntervencion;
        }

        async private void btnIniciarIntervencion_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new XamarinAPP.Pages.Replanteo.ReplanteoMedidasPage(), this);
            await Navigation.PopAsync();
        }
    }
}