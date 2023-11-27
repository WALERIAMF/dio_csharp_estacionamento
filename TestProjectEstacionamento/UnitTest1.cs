using Dio_Csharp_Estacionamento.Repository;

namespace TestProjectEstacionamento
{
    [TestFixture]
    public class EstacionamentoTeste
    {
        private Estacionamento estacionamento;

        [SetUp]
        public void Setup()
        {
            estacionamento = new Estacionamento();
        }

        [Test]
        public void TesteAdicionarVeiculo()
        {
            estacionamento.AdicionarVeiculo("ABC1234");
            Assert.AreEqual(1, estacionamento.veiculos.Count);
            Assert.AreEqual("ABC1234", estacionamento.veiculos[0].Placa);
        }


        [Test]
        public void TesteCalcularValorAte30Minutos()
        {
            estacionamento.AdicionarValorEstacionamentoMeiaHora(5m);
            TimeSpan duracao = TimeSpan.FromMinutes(10);
            decimal valor = estacionamento.CalcularValor(duracao);
            Assert.AreEqual(5m, valor);
        }
        [Test]
        public void TesteCalcularValorMaiorQue30MenorQue60Minutos()
        {
            estacionamento.AdicionarValorEstacionamentoPrimeiraHora(10m);
            TimeSpan duracao = TimeSpan.FromMinutes(45);
            decimal valor = estacionamento.CalcularValor(duracao);
            Assert.AreEqual(10m, valor);
        }
        [Test]
        public void TesteCalcularValorUmaHora()
        {
            estacionamento.AdicionarValorEstacionamentoPrimeiraHora(10m);
            TimeSpan duracao = TimeSpan.FromHours(1);
            decimal valor = estacionamento.CalcularValor(duracao);
            Assert.AreEqual(10m, valor);
        }
        [Test]
        public void TesteCalcularValorMaisDeUmaHora()
        {
            estacionamento.AdicionarValorEstacionamentoPrimeiraHora(10m);
            estacionamento.AdicionarValorEstacionamentoMaisHoras(20m);
            TimeSpan duracao = TimeSpan.FromHours(1.5);
            decimal valor = estacionamento.CalcularValor(duracao);
            Assert.AreEqual(30m, valor);  
        }

 

        [Test]
        public void TesteAdicionarValorEstacionamentoPrimeiraHora()
        {
            decimal valorEsperado = 10m;
            estacionamento.AdicionarValorEstacionamentoPrimeiraHora(valorEsperado);
            Assert.AreEqual(valorEsperado, estacionamento.valorEstacionamento.ValorPrimeiraHora);
        }

        [Test]
        public void TesteAdicionarValorEstacionamentoMeiaHora()
        {
            decimal valorEsperado = 5m;
            estacionamento.AdicionarValorEstacionamentoMeiaHora(valorEsperado);
            Assert.AreEqual(valorEsperado, estacionamento.valorEstacionamento.ValorTrintaMinutos);
        }

        [Test]
        public void TesteAdicionarValorEstacionamentoMaisHoras()
        {
            decimal valorEsperado = 20m;
            estacionamento.AdicionarValorEstacionamentoMaisHoras(valorEsperado);
            Assert.AreEqual(valorEsperado, estacionamento.valorEstacionamento.ValorPorHora);
        }


        [Test]
        public void TesteFecharContaVeiculo()
        {
            // adicione um veículo
            estacionamento.AdicionarVeiculo("ABC1234");

            //  hora de entrada para ser 10 minutos atrás
            estacionamento.veiculos[0].HoraEntrada = DateTime.Now.AddMinutes(-10);

            // Debitar o veículo
            estacionamento.FecharContaVeiculo("ABC1234");

            // Verifique se o veículo foi debitado
            Assert.AreEqual(0, estacionamento.veiculos.Count);
        }

    }

}