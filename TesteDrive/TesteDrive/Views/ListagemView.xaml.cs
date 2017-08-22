using System;
using System.Collections.Generic;
using TesteDrive.Models;
using TesteDrive.ViewModels;
using Xamarin.Forms;

namespace TesteDrive.Views
{

    public partial class ListagemView : ContentPage
	{
        public ListagemViewModel ViewModel { get; set; }
        readonly Usuario usuario;

        public ListagemView(Usuario usuario)
		{
			InitializeComponent();
            this.ViewModel = new ListagemViewModel();
            this.usuario = usuario;
            this.BindingContext = this.ViewModel;
           
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            AssinarMenssagens();

            await this.ViewModel.GetVeiculos();
        }

        private void AssinarMenssagens()
        {
            MessagingCenter.Subscribe<Veiculo>(this, "VeiculoSelecionado",
                            (veiculo) =>
                            {
                                Navigation.PushAsync(new DetalheView(veiculo, usuario));
                            });

            MessagingCenter.Subscribe<Exception>(this, "FalahListagem",
                (msg) =>
                {
                    DisplayAlert("Erro", "Ocorreu um eroo ao obter a listagem de veículos", "Ok");
                });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CancelarAssinatura();
        }

        private void CancelarAssinatura()
        {
            MessagingCenter.Unsubscribe<Veiculo>(this, "VeiculoSelecionado");
            MessagingCenter.Unsubscribe<Exception>(this, "FalahListagem");
        }
    }
}
