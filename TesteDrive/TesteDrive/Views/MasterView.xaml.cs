using TesteDrive.Models;
using TesteDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterView : TabbedPage
    {
      
        public MasterView (Usuario usuario)
		{
            InitializeComponent();
            this.BindingContext = new MasterViewModel(usuario);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            AssinarMenssagem();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CancelarMenssagem();
        }

        private void AssinarMenssagem()
        {
            MessagingCenter.Subscribe<Usuario>(this, "EditarPerfil", (usuario) =>
            {
                this.CurrentPage = this.Children[1];
            });

            MessagingCenter.Subscribe<Usuario>(this, "SucessoSalvarUsuario", (usuario) =>
            {
                this.CurrentPage = this.Children[0];
            });
           
        }
        private void CancelarMenssagem()
        {
            MessagingCenter.Unsubscribe<Usuario>(this, "EditarPerfil");
            MessagingCenter.Unsubscribe<Usuario>(this, "SucessoSalvarUsuario");}
    }
}