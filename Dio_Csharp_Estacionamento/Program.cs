
using System.Drawing;

Estacionamento estacionamento = new Estacionamento();

while (true)
{
    Console.WriteLine("\n\n1. Entrada veículo\n");
    Console.WriteLine("2. Saída veículo\n");
    Console.WriteLine("3. Listar veículos\n");
    Console.WriteLine("4. Valor demais horas do estacionamento\n");
    Console.WriteLine("5. Valor da primeira hora\n");
    Console.WriteLine("6. Valor de meia hora de estacionamento\n");
    Console.WriteLine("7. Listar valores para estacionar\n");
    Console.WriteLine("8. Sair\n");
    Console.Write("Escolha uma opção: \n");
    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.Write("Digite a placa do veículo: ");
            var placa = Console.ReadLine();
            estacionamento.AdicionarVeiculo(placa);
            break;
        case "2":
            Console.Write("Digite a placa do veículo: ");
            placa = Console.ReadLine();
            estacionamento.FecharContaVeiculo(placa);
            break;
        case "3":
            estacionamento.ListarVeiculos();
            break;
        case "4":
            Console.Write("Digite o valor por hora do estacionamento: ");
            decimal valor;
            if (Decimal.TryParse(Console.ReadLine(), out valor))
            {
                estacionamento.AdicionarValorEstacionamentoMaisHoras(valor);
            }
            else
            {
                Console.WriteLine("Valor inválido. Por favor, digite um número.");
            }
            break;

        case "5":
            Console.Write("Digite o valor da primeira hora: ");

            if (Decimal.TryParse(Console.ReadLine(), out valor))
            {
                estacionamento.AdicionarValorEstacionamentoPrimeiraHora(valor);
            }
            else
            {
                Console.WriteLine("Valor inválido. Por favor, digite um número.");
            }
            break;

        case "6":
            Console.Write("Digite o valor de meia hora de estacionamento: ");

            if (Decimal.TryParse(Console.ReadLine(), out valor))
            {
                estacionamento.AdicionarValorEstacionamentoMeiaHora(valor);
            }
            else
            {
                Console.WriteLine("Valor inválido. Por favor, digite um número.");
            }
            break;
        case "7":
            estacionamento.ListarValoresEstacionamento();
            break;
        case "8":
            return;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}


public class Veiculo
{
    public string Placa { get; set; }
    public DateTime HoraEntrada { get; set; }
}

public class ValorEstacionamento
{
    public decimal ValorPrimeiraHora { get; set; }
    public decimal ValorTrintaMinutos { get; set; }
    public decimal ValorPorHora { get; set; }
}

public class Estacionamento
{
    private List<Veiculo> veiculos = new List<Veiculo>();
    private ValorEstacionamento valorEstacionamento = new ValorEstacionamento();

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

    private decimal CalcularValor(TimeSpan duracao)
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