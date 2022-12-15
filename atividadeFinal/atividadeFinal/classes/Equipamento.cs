using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atividadeFinal.classes
{
    public class Equipamento
    {
        public int IdEquipamento { get; set; }
        public string NomeEquipamento { get; set; }
        public bool EquipamentoAvariado { get; set; }

        public Equipamento()
        {
            EquipamentoAvariado = false;
        }

        public override string ToString()
        {
            string avariado = EquipamentoAvariado ? "Sim" : "Não";
            return $"Id: {IdEquipamento} - Nome: {NomeEquipamento} - Está avariado: {avariado}";
        }
    }
}
