using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atividadeFinal.classes
{
    public class Contrato
    {
        public int Id { get; set; }
        public DateTime dtInicio { get; set; }
        public DateTime dtTermino { get; set; }
        public Dictionary<TipoEquipamento, int> Solicitacoes { get; set; }
        public Stack<TipoEquipamento> Equipamentos { get; set; }
        public bool Liberado { get; set; }
        public float Valor
        {
            get => Equipamentos.ToList().Sum(e => e.ValorLocacaoDiaria * e.Equipamentos.Count);
        }

        public Contrato()
        {
            Equipamentos = new Stack<TipoEquipamento>();
        }

        public void incluirEquip(TipoEquipamento tipoEquipamento)
        {
            Equipamentos.Push(tipoEquipamento);
        }

        public override string ToString()
        {
            string retorno = $"Id: {Id} - Inicio Vigencia: {dtInicio.ToString("dd/MM/yy")} - Termino Vigencia: {dtTermino.ToString("dd/MM/yy")} - Liberado: {Liberado}";

            retorno += "\nSolicitações: ";

            if (Solicitacoes.Count <= 0)
            { 
                retorno += "Nenhuma solicitação";
            }
            else
            { 
                foreach (var solicitacao in Solicitacoes)
                {
                    retorno += $"{solicitacao.Key.Descricao} - {solicitacao.Value}";
                }
            }

            return retorno;
        }

        public string ToString(bool mostrarEquipamentos = false)
        {
            string retorno = $"Id: {Id} - Inicio Vigencia: {dtInicio.ToString("dd/MM/yy")} - Termino Vigencia: {dtTermino.ToString("dd/MM/yy")} - Liberado: {Liberado}";

            retorno += "\nSolicitações: ";

            if (Solicitacoes.Count <= 0)
            { 
                retorno += "Nenhuma solicitação";
            }
            else
            { 
                foreach (var solicitacao in Solicitacoes)
                {
                    retorno += $" {solicitacao.Key.Descricao} - {solicitacao.Value}";
                }
            }

            if (mostrarEquipamentos)
            {
                retorno += "\nEquipamentos liberados: ";

                if (Equipamentos.Count <= 0)
                { 
                    retorno += "Nenhum equipamento";
                }
                else
                { 
                    foreach (var tipoEquipamento in Equipamentos)
                    {
                        foreach (var equipamento in tipoEquipamento.Equipamentos)
                        {
                            retorno += $"{equipamento.ToString()}";
                        }
                    }
                }
            }

            return retorno;
        }
    }
}
