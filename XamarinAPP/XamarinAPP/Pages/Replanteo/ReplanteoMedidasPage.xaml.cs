using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CapaEntidades;
using System.Collections.ObjectModel;
using CapaNegocioAPP;

namespace XamarinAPP.Pages.Replanteo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReplanteoMedidasPage : basePage
    {
        ObservableCollection<MedidaCE> lstMedidas;

        public ReplanteoMedidasPage()
        {
            InitializeComponent();
        }

        private async void cargarMedidasList()
        {
            lstMedidas = new ObservableCollection<MedidaCE>(await new ReplanteoCRN_APP().getMedidasByIntervencion(App.oIntervencion.idIntervencion));
            lstView.ItemsSource = lstMedidas;
        }

        private async void cargarComboTipomedidas()
        {
            List<MedidaTiposCE> lstMedidasTipos = await new ReplanteoCRN_APP().getMedidasTipos();
            pckTipoMedidas.ItemDisplayBinding = new Binding("descripcionTipoMedida");
            pckTipoMedidas.ItemsSource = lstMedidasTipos;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargarComboTipomedidas();
            cargarMedidasList();
        }

        private void btnAgregarmedida_Clicked(object sender, EventArgs e)
        {
            MedidaCE oMedidaCE = getMedidaIntroducida();
            try
            {
                new ReplanteoCRN_APP().insertarReplanteoMedida(oMedidaCE);
                lstMedidas.Add(oMedidaCE);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Volver");
            }

        }

        private MedidaCE getMedidaIntroducida()
        {
            MedidaCE oMedidaCE = new MedidaCE();
            oMedidaCE.idTipoMedida = ((MedidaTiposCE)pckTipoMedidas.SelectedItem).idTipoMedida;
            oMedidaCE.descripcion = ((MedidaTiposCE)pckTipoMedidas.SelectedItem).descripcionTipoMedida;
            oMedidaCE.comentario = txtComentario.Text;
            oMedidaCE.valor = float.Parse(txtValor.Text);
            oMedidaCE.idIntervencion = App.oIntervencion.idIntervencion;
            oMedidaCE.idUsuario = App.oUsuarioLogged.idUsuario;
            oMedidaCE.tecnico = App.oTecnico.tecnico;
            oMedidaCE.telefonoTecnico = App.oTecnico.telefonoTecnico;

            return oMedidaCE;
        }

        private void btnEliminar_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var oMedidaCE = item.CommandParameter as MedidaCE;
            try
            {
                new ReplanteoCRN_APP().eliminarReplanteoMedida(oMedidaCE.idReplanteoMedida);
                lstMedidas.Remove(oMedidaCE);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Volver");
            }
        }

        private void btnSiguiente_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReplanteoImagenesPage());
        }

    }


}