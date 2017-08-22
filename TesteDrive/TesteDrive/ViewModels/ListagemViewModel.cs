using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TesteDrive.Models;
using Xamarin.Forms;

namespace TesteDrive.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        const string URL_GET_VEICULOS = "http://aluracar.herokuapp.com/";
        public ObservableCollection<Veiculo> Veiculos { get; set; }

        Veiculo veiculoSelecionado;
        public Veiculo VeiculoSelecionado
        {
            get
            {
                return VeiculoSelecionado;
            }
            set
            {
                veiculoSelecionado = value;
                if (value != null)
                {
                    MessagingCenter.Send(veiculoSelecionado, "VeiculoSelecionado");

                }
            }
        }
        public ListagemViewModel()
        {
            this.Veiculos = new ObservableCollection<Veiculo>();
        }

        private bool aguarde;

        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }

        public async Task GetVeiculos()
        {
            Aguarde = true;
            HttpClient cliete = new HttpClient();
            var resultado = await cliete.GetStringAsync(URL_GET_VEICULOS);

            var veiculosJson = JsonConvert.DeserializeObject<VeiculoJson[]>(resultado);

            foreach (var veiculo in veiculosJson)
            {
                this.Veiculos.Add(new Veiculo
                {
                    Nome = veiculo.nome,
                    Preco = veiculo.preco
                });
            }
            Aguarde = false;
        }
    }
}
    
    class VeiculoJson
    {
        public string nome { get; set; }
        public int preco { get; set; }
    }