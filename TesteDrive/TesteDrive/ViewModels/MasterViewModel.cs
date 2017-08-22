using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using TesteDrive.Midia;
using TesteDrive.Models;
using Xamarin.Forms;

namespace TesteDrive.ViewModels
{
    public class MasterViewModel :BaseViewModel
    {

        public string Nome
        {
            get { return this.usuario.nome; }
            set { this.usuario.nome = value; }
        }

        public string Email
        {
            get { return this.usuario.email; }
            set { this.usuario.email = value; }
        }

        public string DataNascimento
        {
            get { return this.usuario.dataNascimento; }
            set { this.usuario.dataNascimento = value; }
        }        

        public string Telefone
        {
            get { return this.usuario.telefone; }
            set { this.usuario.telefone = value; }
        }
        private bool editando = false;

        public bool Editando
        {
            get { return editando; }
            private set
            {
                editando = value;
                OnPropertyChanged();
            }
        }

        private ImageSource imagemPerfil = "perfil.png";

        public ImageSource ImagemPerfil
        {
            get { return imagemPerfil; }
            private set
            {
                imagemPerfil = value;
                OnPropertyChanged();
            }
        }


        private readonly Usuario usuario;

        public ICommand EditarPerfilCommand { get; private set; }
        public ICommand SalvarCommand { get; private set; }
        public ICommand EditarCommand { get; private set; }
        public ICommand TirarFotoCommand { get; private set; }
        public ICommand AgendamentosCommand { get; private set; }
        public ICommand NovoAgendamento { get; private set; }
        public MasterViewModel(Usuario usuario)
        {
            this.usuario = usuario;
            DefinirComandos(usuario);
            AssinarMensagens();
        }

        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<byte[]>(this, "FotoTirada", (bytes) =>
            {
                ImagemPerfil = ImageSource.FromStream(
                    () => new MemoryStream(bytes));
            });
        }

        private void DefinirComandos(Usuario usuario)
        {
            EditarPerfilCommand = new Command(() =>
            {
                MessagingCenter.Send<Usuario>(usuario, "EditarPerfil");
            });

            SalvarCommand = new Command(() =>
            {
                this.Editando = false;
                MessagingCenter.Send<Usuario>(usuario, "SucessoSalvarUsuario");
            });

            EditarCommand = new Command(() =>
            {
                this.Editando = true;
            });

            TirarFotoCommand = new Command(() =>
            {

                DependencyService.Get<ICamera>().TirarFoto();
            });

            AgendamentosCommand = new Command(() =>
            {
                MessagingCenter.Send<Usuario>(usuario, "MeusAgendamentos");
            });

            NovoAgendamento = new Command(() =>
            {      
                MessagingCenter.Send<Usuario>(usuario, "NovoAgendamento");
            });
        }
    }
}
