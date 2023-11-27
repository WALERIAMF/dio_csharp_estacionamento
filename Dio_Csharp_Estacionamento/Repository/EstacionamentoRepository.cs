using Dio_Csharp_Estacionamento.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dio_Csharp_Estacionamento.Repository
{
    public class Estacionamento
    {
        public List<Veiculo> veiculos = new List<Veiculo>();
        public ValorEstacionamento valorEstacionamento = new ValorEstacionamento();

        public void AdicionarVeiculo(string placa)
        {
            veiculos.Add(new Veiculo { Placa = placa, HoraEntrada = DateTime.Now });
        }

        public void AdicionarValorEstacionamentoPrimeiraHora(decimal valor)
        {
            valorEstacionamento.ValorPrimeiraHora = valor;
        }

        public void AdicionarValorEstacionamentoMeiaHora(decimal valor)
        {
            valorEstacionamento.ValorTrintaMinutos = valor;
        }

        public void AdicionarValorEstacionamentoMaisHoras(decimal valor)
        {
            valorEstacionamento.ValorPorHora = valor;
        }

        public void ListarValoresEstacionamento()
        {
            Console.WriteLine($"-->Valor da primeira hora: {valorEstacionamento.ValorPrimeiraHora}");
            Console.WriteLine($"-->Valor de meia hora: {valorEstacionamento.ValorTrintaMinutos}");
            Console.WriteLine($"-->Valor por hora adicional: {valorEstacionamento.ValorPorHora}");
        }


        public void FecharContaVeiculo(string placa)
        {
            var veiculo = veiculos.FirstOrDefault(v => v.Placa == placa);
            if (veiculo != null)
            {
                var duracao = DateTime.Now - veiculo.HoraEntrada;
                var valorCobrado = CalcularValor(duracao);
                Console.WriteLine($"-->Veículo removido. Valor cobrado: {valorCobrado}");
                veiculos.Remove(veiculo);
            }
        }

        public void ListarVeiculos()
        {
            foreach (var veiculo in veiculos)
            {
                Console.WriteLine($"-->Placa: {veiculo.Placa}, Hora de Entrada: {veiculo.HoraEntrada}");
            }
        }

        public decimal CalcularValor(TimeSpan duracao)
        {
            decimal valor = 0;
            if (duracao.TotalMinutes <= 30)
            {
                valor = valorEstacionamento.ValorTrintaMinutos;
            }
            else if (duracao.TotalMinutes > 30 && duracao.TotalHours <= 1)
            {
                valor = valorEstacionamento.ValorPrimeiraHora;
            }
            else if (duracao.TotalHours > 1)
            {
                valor = valorEstacionamento.ValorPrimeiraHora;
                duracao = duracao.Subtract(TimeSpan.FromHours(1));
                valor += (decimal)Math.Ceiling(duracao.TotalHours) * valorEstacionamento.ValorPorHora;
            }
            return valor;
        }
    }
}