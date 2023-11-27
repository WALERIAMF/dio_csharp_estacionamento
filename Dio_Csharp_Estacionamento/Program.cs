
using Dio_Csharp_Estacionamento.Repository;
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






