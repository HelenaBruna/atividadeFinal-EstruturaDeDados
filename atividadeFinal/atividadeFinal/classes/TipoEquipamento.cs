using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atividadeFinal.classes
{
    public class TipoEquipamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public float ValorLocacaoDiaria { get; set; }
        public Queue<Equipamento> Equipamentos { get; }

        public TipoEquipamento()
            : this(0)
        {
        }

        public TipoEquipamento(int id)
        {
            Id = id;
            Equipamentos = new Queue<Equipamento>();
        }

        public void incluirEquip(Equipamento equipamento)
        {
            Equipamentos.Enqueue(equipamento);
        }

        public Equipamento retornoEquipamento()
        {
            return Equipamentos.Dequeue();
        }

        public override string ToString()
        {
            string retorno = $"Id: {Id} - Descricao: {Descricao} - Valor Diária da locação: {ValorLocacaoDiaria.ToString("C")}\nEquipamentos: ";

            if (Equipamentos.Count <= 0)
            { 
                retorno += "Sem equipamentos";
            }
            else
            { 
                foreach (var equipamento in Equipamentos)
                {
                    retorno += $"{equipamento.ToString()}";
                }
            }

            return retorno;
        }
    }
}
