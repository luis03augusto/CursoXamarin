using System.Collections.Generic;

namespace TesteDrive.Models
{
    public class ListagemVeiculosModel
    {
        public List<Veiculo> Veiculos { get; set; }
        public ListagemVeiculosModel()
        {
            this.Veiculos = new List<Veiculo>
            {
                new Veiculo {Nome = "Azera", Preco = 60000},
                new Veiculo {Nome = "Fiesta 2.0", Preco = 50000},
                new Veiculo {Nome = "HB20 S", Preco = 40000},
                new Veiculo {Nome = "Hilux 4x4", Preco = 90000}
            };
        }
    }
}
