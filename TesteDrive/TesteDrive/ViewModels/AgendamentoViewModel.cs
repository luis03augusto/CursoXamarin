﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using TesteDrive.Data;
using TesteDrive.Models;
using TesteDrive.Service;
using Xamarin.Forms;

namespace TesteDrive.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        
        public Agendamento Agendamento { get; set; }

        public string Modelo
        {
            get { return this.Agendamento.Modelo; }
            set { this.Agendamento.Modelo = value; }
        }

        public decimal Preco
        {
            get { return this.Agendamento.Preco; }
            set { this.Agendamento.Preco = value; }
        }


        public string Nome { get { return Agendamento.Nome; } set { Agendamento.Nome = value;  OnPropertyChanged(); ((Command)AgendarCommand).ChangeCanExecute(); } }
        public string Fone { get { return Agendamento.Fone; } set { Agendamento.Fone = value; OnPropertyChanged(); ((Command)AgendarCommand).ChangeCanExecute(); } }
        public string Email { get { return Agendamento.Email; } set { Agendamento.Email = value; OnPropertyChanged(); ((Command)AgendarCommand).ChangeCanExecute(); } }

        public DateTime DataAgendamento
        {
            get
            {
                return Agendamento.DataAgendamento;
            }
            set
            {
                Agendamento.DataAgendamento = value;
            }
        }

        public async void SalvarAgendamento()
        {
            AgendamentoService agendamentoService = new AgendamentoService();
            await agendamentoService.EnviarAgendamento(this.Agendamento);
        }

     

        public TimeSpan HoraAgendamento { get { return Agendamento.HoraAgendamento; } set { Agendamento.HoraAgendamento = value; } }

        public AgendamentoViewModel(Veiculo veiculo, Usuario usuario)
        {
            this.Agendamento = new Agendamento(usuario.nome, usuario.telefone, usuario.email, veiculo.Nome,veiculo.Preco);

            AgendarCommand = new Command(() =>
            {
                MessagingCenter.Send<Agendamento>(this.Agendamento, "Agendamento");
            }, ()=> 
            {
                return !string.IsNullOrEmpty(this.Nome)
                        && !string.IsNullOrEmpty(this.Fone)
                        && !string.IsNullOrEmpty(this.Email);
            });
        }

        public ICommand AgendarCommand { get; set; }
        public object SalvaAgendamento { get; internal set; }
    }

   
}
