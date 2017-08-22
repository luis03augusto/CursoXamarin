using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TesteDrive.Models;
using Xamarin.Forms;

namespace TesteDrive.ViewModels
{
    public class DetalheViewModel : BaseViewModel
    {
        public Veiculo veiculo { get; set; }
        public string TextoABS
        {
            get
            {
                return string.Format("Freio ABS - R$ {0}", Veiculo.FREIO_ABS);
            }
        }
        public string TextoAr
        {
            get
            {
                return string.Format("Ar Condicionado - R$ {0}", Veiculo.AR_CONDICIONADO);
            }
        }
        public string TextoMP3
        {
            get
            {
                return string.Format("MP3 Player- R$ {0}", Veiculo.MP3_PLAYER);
            }
        }
        public bool TemABS
        {
            get
            {
                return veiculo.TemABS;
            }
            set
            {
                veiculo.TemABS = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }
        public bool TemAR
        {
            get
            {
                return veiculo.TemAr;
            }
            set
            {
                veiculo.TemAr = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }
        public bool TemMP3
        {
            get
            {
                return veiculo.TemMP3;
            }
            set
            {
                veiculo.TemMP3 = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }
        public string ValorTotal
        {
            get
            {
                return veiculo.PrecoTotalFormatado;
            }
        }

        public DetalheViewModel(Veiculo veiculo)
        {
            this.veiculo = veiculo;
            BotaoProximo = new Command(() =>
            {
                MessagingCenter.Send(veiculo, "Proximo");
            });
        }

     

        public ICommand BotaoProximo { get; set; }
    }
}
