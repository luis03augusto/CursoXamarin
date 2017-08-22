using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TesteDrive.Data;
using TesteDrive.Models;
using Xamarin.Forms;

namespace TesteDrive.Service
{
    class AgendamentoService
    {
        const string URL_POPSAGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";

        public async System.Threading.Tasks.Task EnviarAgendamento(Agendamento agendamento)
        {
            HttpClient cliente = new HttpClient();

            var dataHotaAgendamento = new DateTime(
                agendamento.DataAgendamento.Year, agendamento.DataAgendamento.Month, agendamento.DataAgendamento.Day,
                agendamento.HoraAgendamento.Hours, agendamento.HoraAgendamento.Minutes, agendamento.HoraAgendamento.Seconds
                );
            var Json = JsonConvert.SerializeObject(new
            {
                nome = agendamento.Nome,
                fone = agendamento.Fone,
                email = agendamento.Email,
                carro = agendamento.Modelo,
                preco = agendamento.Preco,
                dataAgendamento = dataHotaAgendamento
            });

            var conteudo = new StringContent(Json, Encoding.UTF8, "application/json");

            var resposta = await cliente.PostAsync(URL_POPSAGENDAMENTO, conteudo);

            agendamento.Confirmado = resposta.IsSuccessStatusCode;

            SalvarAgendamentoDB(agendamento);

            if (agendamento.Confirmado)
            {
                MessagingCenter.Send<Agendamento>(agendamento, "SucessoAgendamento");
            }
            else
            {
                MessagingCenter.Send<ArgumentException>(new ArgumentException(), "FalhaAgendamento");
            }
        }

        private void SalvarAgendamentoDB(Agendamento agendamento)
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                AgendamentoDAO dao = new AgendamentoDAO(conexao);

                dao.Salvar(agendamento);
            }
        }
    }
}
