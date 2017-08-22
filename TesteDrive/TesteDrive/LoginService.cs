using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TesteDrive.Models;
using Xamarin.Forms;

namespace TesteDrive
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {
            using (var cliente = new HttpClient())
            {
                var campoFormulario = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("email", login.email),
                        new KeyValuePair<string, string>("senha", login.senha),
                    });
                cliente.BaseAddress = new Uri("https://aluracar.herokuapp.com");

                HttpResponseMessage resultado = null;
                try
                {
                    resultado = await cliente.PostAsync("/login", campoFormulario);

                }
                catch
                {

                    MessagingCenter.Send<LoginException>(new LoginException(@"Ocerreu um erro de comunicação com o servidor.
Por favor verifique sua conexão e tente novamento mais tarde."), "FalhaLogin");
                }

                if (resultado.IsSuccessStatusCode)
                {
                    var conteudoResultado = await resultado.Content.ReadAsStringAsync();
                    var resultadoLogin = JsonConvert.DeserializeObject<ResultadoLogin>(conteudoResultado);
                    MessagingCenter.Send<Usuario>(resultadoLogin.usuario, "SucessoLogin");

                }
                else
                {
                    MessagingCenter.Send<LoginException>(new LoginException("Usuário o senha inválida!"), "FalhaLogin");
                }
            }
        }
    }
    public class LoginException : Exception
    {
        public LoginException(string message) : base(message)
        {
        }
    }
}
