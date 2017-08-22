using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using TesteDrive.Models;
using Xamarin.Forms;

namespace TesteDrive.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set { usuario = value;
                ((Command)EentrarCommand).ChangeCanExecute();
            }
        }
        private string senha;

        public string Senha
        {
            get { return senha; }
            set { senha = value;
                ((Command)EentrarCommand).ChangeCanExecute();
                }
        }

        public ICommand EentrarCommand { get; private set; }

        public  LoginViewModel()
        {
            EentrarCommand = new Command(async() =>
            {
                var loginService = new LoginService();
                await loginService.FazerLogin(new Login(usuario, senha));
            }, ()=>
            {
                return !string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(senha);
            });
        }

       
    }
}
