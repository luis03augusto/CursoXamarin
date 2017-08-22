
using SQLite;
using System.Collections.Generic;
using System.Linq;
using TesteDrive.Models;

namespace TesteDrive.Data
{
    public class AgendamentoDAO
    {
        readonly SQLiteConnection conexao;

        private List<Agendamento> lista;

        public List<Agendamento> Lista
        {
            get
            {
                if (lista == null)
                {
                    lista = new List<Agendamento>(conexao.Table<Agendamento>());
                }
                return lista;
            }
            private set
            {
                lista = value;
            }
        }


        public AgendamentoDAO(SQLiteConnection conexao)
        {
            this.conexao = conexao;
            this.conexao.CreateTable<Agendamento>();
        }

        public void Salvar(Agendamento agendamento)
        {
            if (conexao.Find<Agendamento>(agendamento.ID) == null)
            {
                conexao.Insert(agendamento);
            }
            else
            {
                conexao.Update(agendamento);
            }
        }
    }
}
