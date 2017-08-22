using TesteDrive.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetailView : MasterDetailPage
	{
        private readonly Usuario usuario;
		public MasterDetailView (Usuario usuario )
		{
			InitializeComponent ();
            this.usuario = usuario;
            this.Master = new MasterView(usuario);
            this.Detail = new NavigationPage(new ListagemView(usuario));
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AssinarMensagens();
        }

        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<Usuario>(this, "MeusAgendamentos", (usuario) =>
            {
                this.Detail = new NavigationPage(
                new AgendamentosUsuarioView());
                this.IsPresented = false;
            });

            MessagingCenter.Subscribe<Usuario>(this, "NovoAgendamento", (usuario) =>
            {
                this.Detail = new NavigationPage(new ListagemView(usuario));
                this.IsPresented = false;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CancelarAssinaturas();
        }

        private void CancelarAssinaturas()
        {
            MessagingCenter.Unsubscribe<Usuario>(this, "MeusAgendamentos");
            MessagingCenter.Unsubscribe<Usuario>(this, "NovoAgendamento");
        }
    }
}