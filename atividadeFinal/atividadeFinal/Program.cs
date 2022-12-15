using atividadeFinal.classes;
using System;
using System.Collections.Generic;

namespace atividadeFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            Locacao locacao = new Locacao();
            int opcao = 1;

            do
            {
                Console.WriteLine("0 - Finalizar"); 
                Console.WriteLine("1 - Incluir tipos de equipamentos"); 
                Console.WriteLine("2 - Consultar tipo do equipamento"); 
                Console.WriteLine("3 - Incluir equipamento"); 
                Console.WriteLine("4 - Registrar contrato"); 
                Console.WriteLine("5 - Consultar contrato"); 
                Console.WriteLine("6 - Liberar contrato"); 
                Console.WriteLine("7 - Consultar contratos liberados"); 
                Console.WriteLine("8 - Devolver equipamento"); 

                Console.Write("\nDigite a opção: ");
                opcao = int.Parse(Console.ReadLine());

                try
                {
                    switch (opcao)
                    {
                        case 0:
                            Console.WriteLine("Fechando..");
                            Console.ReadKey();
                            break;
                        case 1:
                            TipoEquipamento tipoEquipamento = new TipoEquipamento();

                            Console.Write("Digite o Id do tipo de equipamento: ");
                            tipoEquipamento.Id = int.Parse(Console.ReadLine());

                            Console.Write("Digite a descrição do tipo de equipamento: ");
                            tipoEquipamento.Descricao = Console.ReadLine();

                            Console.Write("Digite o valor do tipo de equipamento: ");
                            tipoEquipamento.ValorLocacaoDiaria = float.Parse(Console.ReadLine());

                            locacao.Incluir(tipoEquipamento);

                            Console.WriteLine("\n\nTipo de equipamento incluido!\n");
                            break;
                        case 2:
                            Console.Write("Digite o Id do Tipo de equipamento: ");
                            int id = int.Parse(Console.ReadLine());

                            var tipoEquipamentoPesquisa = locacao.ConsultarTiposEquipamento(id);

                            if (tipoEquipamentoPesquisa == null)
                            {
                                Console.WriteLine("\n\nNão foi possivel encontrar o tipo equipamento \n");
                            }
                            else
                            {
                                Console.WriteLine($"\n\n{tipoEquipamentoPesquisa.ToString()}\n");
                            }
                            break;
                        case 3:
                            Console.Write("Digite o Id do Tipo de equipamento: ");
                            int idEquip = int.Parse(Console.ReadLine());

                            var tipoEquipamentoCadastro = locacao.ConsultarTiposEquipamento(idEquip);

                            if (tipoEquipamentoCadastro == null)
                            {
                                Console.WriteLine("\n\nNão foi possivel encontrar o tipo do equipamento\n");
                                return;
                            }

                            Equipamento equipamento = new Equipamento();

                            Console.Write("Digite o Id do equipamento: ");
                            equipamento.IdEquipamento = int.Parse(Console.ReadLine());

                            Console.Write("Digite o nome equipamento: ");
                            equipamento.NomeEquipamento = Console.ReadLine();

                            tipoEquipamentoCadastro.incluirEquip(equipamento);

                            Console.WriteLine("\n\nEquipamento incluido!\n");
                            break;
                        case 4:
                            Contrato contrato = new Contrato();

                            Console.Write("Digite o Id do contrato: ");
                            contrato.Id = int.Parse(Console.ReadLine());

                            Console.WriteLine("\n");
                            contrato.dtInicio = dateFormat("do inicio de vigencia do contrato");

                            Console.WriteLine("\n");
                            contrato.dtTermino = dateFormat("do termino de vigencia do contrato");

                            contrato.Solicitacoes = inputSolicitacao();

                            locacao.Incluir(contrato);

                            Console.WriteLine("\n\nContrato incluido!\n");
                            break;
                        case 5:
                            var contratos = locacao.ConsultarContratosLocacao();

                            if (contratos.Count <= 0)
                            {
                                Console.WriteLine("\n\nNão existe contratos cadastrados.\n");
                                return;
                            }

                            Console.WriteLine("\n\nContratos solicitados\n");
                            foreach (var contratoLocacao in contratos)
                            {
                                Console.WriteLine(contratoLocacao.ToString());
                            }
                            break;
                        case 6:
                            Console.Write("Digite o Id do contrato: ");
                            int idContrato = int.Parse(Console.ReadLine());

                            locacao.Liberar(idContrato);

                            Console.WriteLine("\n\nContrato liberado!\n");
                            break;
                        case 7:
                            var contratosLiberados = locacao.ConsultarContratosLocacao(liberados: true);

                            if (contratosLiberados.Count <= 0)
                            {
                                Console.WriteLine("\n\nNão existe contratos liberados.\n");
                                return;
                            }

                            Console.WriteLine("\n\nContratos solicitados\n");
                            foreach (var contratoLiberado in contratosLiberados)
                            {
                                Console.WriteLine(contratoLiberado.ToString(mostrarEquipamentos: true));
                            }
                            break;
                        case 8:
                            Console.Write("Digite o Id do contrato: ");
                            int idCont = int.Parse(Console.ReadLine());

                            locacao.Devolver(idCont);

                            Console.WriteLine("\n\nContrato devolvido!\n");
                            break;
                        default:
                            Console.WriteLine("\n\nopção invalida\n\n");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n\n{e.Message}\n");
                }

            } while (opcao != 0);

            DateTime dateFormat(string label = "")
            {
                DateTime data;
                int dia, mes, ano;

                Console.Write($"Digite o dia {label}: ");
                dia = int.Parse(Console.ReadLine());

                Console.Write($"Digite o mes {label}: ");
                mes = int.Parse(Console.ReadLine());

                Console.Write($"Digite o ano {label}: ");
                ano = int.Parse(Console.ReadLine());

                data = new DateTime(ano, mes, dia);

                return data;
            }

            Dictionary<TipoEquipamento, int> inputSolicitacao()
            {
                var solicitacoes = new Dictionary<TipoEquipamento, int>();
                char sair = ' ';

                while (sair != '0')
                {
                    Console.Write("\n\nDigite o  Id do tipo de equipamento solicitado: ");
                    int idTipoEquipamento = int.Parse(Console.ReadLine());

                    var tipoEquipamento = locacao.ConsultarTiposEquipamento(idTipoEquipamento);

                    if (tipoEquipamento == null)
                    {
                        Console.WriteLine("\n\nNão foi possivel encontrar o tipo do equipamento.\n");
                        continue;
                    }

                    Console.Write("Digite a qtd de equipamentos: ");
                    int qtdTipoEquipamento = int.Parse(Console.ReadLine());

                    if (qtdTipoEquipamento <= 0)
                    {
                        Console.WriteLine("\n\nA quantidade deve ser valida.\n");
                        continue;
                    }

                    solicitacoes.Add(tipoEquipamento, qtdTipoEquipamento);

                    Console.Write("Deseja adicionar mais algum tipo de equipamento");
                    sair = Console.ReadLine()[0];
                }

                return solicitacoes;
            }

        }
    }
}
