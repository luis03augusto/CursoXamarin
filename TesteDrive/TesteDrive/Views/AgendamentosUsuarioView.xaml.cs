using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDrive.Models;
using TesteDrive.Service;
using TesteDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TesteDrive.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AgendamentosUsuarioView : ContentPage
	{
        readonly AgendamentosUsuarioViewModel _viewModel;
        public AgendamentosUsuarioView ()
		{
			InitializeComponent ();
            this._viewModel = new AgendamentosUsuarioViewModel();
            this.BindingContext =  this._viewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AssinarMenssagens();
        }

        private void AssinarMenssagens()
        {
            MessagingCenter.Subscribe<Agendamento>(this, "AgendamentoSelecionado",
                            async (agendamento) =>
                            {
                                if (!agendamento.Confirmado)
                                {
                                    var reeviar = await DisplayAlert("Reenviar", "Deseja reeinviar o agendamento?", "Sim", "Não");
                                    if (reeviar)
                                    {
                                        AgendamentoService agendamentoService = new AgendamentoService();
                                        await agendamentoService.EnviarAgendamento(agendamento);
                                        this._viewModel.AtualizarLista();
                                    }

                                }
                            });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento", async (agendamento) =>
            {
                await DisplayAlert("Reeinviar", "Reenvio com sucesso!", "Ok");
            });

            MessagingCenter.Subscribe<Agendamento>(this, "FalhaoAgendamento", async (agendamento) =>
            {
                await DisplayAlert("Falha", "Falha ao reenviar!", "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CancelarAssinatura();
        }

        private void CancelarAssinatura()
        {
            MessagingCenter.Unsubscribe<Agendamento>(this, "AgendamentoSelecionado");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "FalhaoAgendamento");
        }
    }
}