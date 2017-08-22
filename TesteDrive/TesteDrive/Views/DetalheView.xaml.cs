using System;
using TesteDrive.Models;
using TesteDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalheView : ContentPage
    {
        public Veiculo Veiculo { get; private set; }
        public Usuario Usuario { get; private set; }
        public DetalheView (Veiculo veiculo, Usuario usuario)
		{
            InitializeComponent ();
            this.Veiculo = veiculo;
            this.Usuario = usuario;
            this.BindingContext = new DetalheViewModel(veiculo);       
		}


        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Veiculo>(this, "Proximo", 
                (veiculo) =>
            {
                Navigation.PushAsync(new AgendamentoView(veiculo, this.Usuario));
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Veiculo>(this, "Proximo");
        }
    }
}