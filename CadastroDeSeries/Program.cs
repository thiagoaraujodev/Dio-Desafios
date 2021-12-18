using System;

namespace DIO.Series
{
  class Program
  {
    static SerieRepositorio repositorio = new SerieRepositorio();
    static void Main(string[] args)
    {
      Console.WriteLine();
      Console.WriteLine("Olá, seja bem-vindo a DIO Séries!!!");

      string opcaoUsuario = ObterOpcaoUsuario();

      while (opcaoUsuario.ToUpper() != "X")
      {
        switch (opcaoUsuario)
        {
          case "1":
            ListarSeries();
            break;
          case "2":
            InserirSerie();
            break;
          case "3":
            AtualizarSerie();
            break;
          case "4":
            ExcluirSerie();
            break;
          case "5":
            VisualizarSerie();
            break;
          case "C":
            Console.Clear();
            break;
          default:
            Console.WriteLine("Desculpe, opção inválida!");
            break;
        }
        opcaoUsuario = ObterOpcaoUsuario();
      }

      Console.WriteLine("Obrigado por utilizar nossos serviços.");
      Console.ReadLine();
    }

    private static string ObterOpcaoUsuario()
    {
      Console.WriteLine();
      Console.WriteLine("Informe a opção desejada:");
      Console.WriteLine("1- Listar séries");
      Console.WriteLine("2- Inserir nova série");
      Console.WriteLine("3- Atualizar série");
      Console.WriteLine("4- Excluir série");
      Console.WriteLine("5- Visualizar série");
      Console.WriteLine("C- Limpar Tela");
      Console.WriteLine("X- Sair");
      Console.WriteLine();

      string opcaoUsuario = Console.ReadLine().ToUpper();
      Console.WriteLine();
      return opcaoUsuario;
    }

    private static void ListarSeries()
    {
      Console.WriteLine("Listar séries");

      var lista = repositorio.Lista();

      if (lista.Count == 0)
      {
        Console.WriteLine("Nenhuma série cadastrada.");
        return;
      }

      foreach (var serie in lista)
      {
        var excluido = serie.retornaExcluido();

        Console.WriteLine("#ID {0}: {1} - {2} {3}", serie.retornaId(), serie.retornaTitulo(), serie.retornaDescricao(), (excluido ? "- *Excluído não pode ser alterado*" : ""));
      }
    }

    private static void InserirSerie()
    {
      Console.WriteLine("Tipo da série:");
      int qtdTipoSerie = 0;
      foreach (int i in Enum.GetValues(typeof(Genero)))
      {
        Console.WriteLine("{0}- {1}", i, Enum.GetName(typeof(Genero), i));
        qtdTipoSerie++;
      }

      Console.WriteLine();
      Console.Write("Digite o gênero entre as opções acima: ");
      if (int.TryParse(Console.ReadLine(), out int entradaGenero)
          && entradaGenero > 0
          && entradaGenero <= qtdTipoSerie)
      {

        Console.Write("Informe o Título da Série: ");
        string entradaTitulo = Console.ReadLine();

        int entradaAno;
        while (true)
        {
          Console.Write("Digite o Ano de Início da Série, (Ex. 2021): ");
          if (int.TryParse(Console.ReadLine(), out entradaAno)
              && entradaAno.ToString().Length == 4)
          {
            break;
          }
          Console.WriteLine("Desculpe, opção inválida!");
          Console.WriteLine();
        }

        Console.Write("Digite a Descrição da Série: ");
        string entradaDescricao = Console.ReadLine();

        Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                    genero: (Genero)entradaGenero,
                                    titulo: entradaTitulo,
                                    ano: entradaAno,
                                    descricao: entradaDescricao);

        repositorio.Insere(novaSerie);

        Console.WriteLine();
        Console.Write("Dados inseridos com sucesso!");
        Console.WriteLine();

      }
      else
      {
        Console.WriteLine();
        Console.WriteLine("Desculpe, opção inválida!");
        Console.WriteLine();
      }
    }

    private static void AtualizarSerie()
    {
      Console.Write("Digite o id da série que deseja atualizar: ");
      if (int.TryParse(Console.ReadLine(), out int idSerie))
      {
        int continua = 0;
        foreach (var serie in repositorio.Lista())
        {
          if (idSerie == serie.retornaId() && !serie.retornaExcluido())
          {
            continua = 1;
          }
        }

        if (continua == 1)
        {
          int qtdTipoSerie = 0;
          Console.WriteLine();
          foreach (int i in Enum.GetValues(typeof(Genero)))
          {
            Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
            qtdTipoSerie++;
          }

          Console.WriteLine();
          Console.Write("Digite o gênero entre as opções acima: ");
          if (int.TryParse(Console.ReadLine(), out int entradaGenero)
            && entradaGenero > 0
            && entradaGenero <= qtdTipoSerie)
          {

            Console.Write("Informe o Novo Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            int entradaAno;
            while (true)
            {
              Console.Write("Digite o Ano de Início da Série, (Ex. 2021): ");
              if (int.TryParse(Console.ReadLine(), out entradaAno)
                  && entradaAno.ToString().Length == 4)
              {
                break;
              }
              Console.WriteLine("Desculpe, opção inválida!");
              Console.WriteLine();
            }

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: idSerie,
                            genero: (Genero)entradaGenero,
                            titulo: entradaTitulo,
                            ano: entradaAno,
                            descricao: entradaDescricao);

            repositorio.Atualiza(idSerie, atualizaSerie);

            Console.WriteLine();
            Console.Write("Dados inseridos com sucesso!");
            Console.WriteLine();
          }
          else
          {
            Console.WriteLine();
            Console.WriteLine("Desculpe, opção inválida!");
            Console.WriteLine();
          }
        }
        else
        {
          Console.Write("Série não encontrada!");
          Console.WriteLine();
        }
      }
      else
      {
        Console.WriteLine();
        Console.WriteLine("Desculpe, opção inválida!");
        Console.WriteLine();
      }
    }

    private static void ExcluirSerie()
    {
      Console.Write("Digite o id da série: ");
      if (int.TryParse(Console.ReadLine(), out int idSerie))
      {
        int continua = 0;
        foreach (var serie in repositorio.Lista())
        {
          if (idSerie == serie.retornaId() && !serie.retornaExcluido())
          {
            continua = 1;
          }
        }

        if (continua == 1)
        {
          repositorio.Exclui(idSerie);
          Console.Write("Série excluida com sucesso!");
          Console.WriteLine();
        }
        else
        {
          Console.Write("Série não encontrada!");
          Console.WriteLine();
        }
      }
      else
      {
        Console.WriteLine();
        Console.WriteLine("Desculpe, opção inválida!");
        Console.WriteLine();
      }
    }

    private static void VisualizarSerie()
    {
      Console.Write("Digite o id da série: ");
      int idSerie = int.Parse(Console.ReadLine());

      var serie = repositorio.RetornaPorId(idSerie);

      Console.WriteLine(serie);
    }
  }
}