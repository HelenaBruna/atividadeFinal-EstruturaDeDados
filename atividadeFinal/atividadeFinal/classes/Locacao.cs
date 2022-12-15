using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atividadeFinal.classes
{
    public class Locacao
    {
        public List<TipoEquipamento> Estoque { get; private set; }
        public List<Contrato> ContratosLocacao { get; private set; }

        public Locacao()
        {
            Estoque = new List<TipoEquipamento>();
            ContratosLocacao = new List<Contrato>();
        }

        public void Incluir(TipoEquipamento tipoEquipamento)
        {
            Estoque.Add(tipoEquipamento);
        }

        public void Incluir(int idTipo, Equipamento equipamento)
        {
            var tipoEquipamento = Estoque.FirstOrDefault(te => te.Id == idTipo);

            if (tipoEquipamento == null)
            { 
                throw new Exception($"Não existe um equipamento com este id: ${idTipo}");
            }

            tipoEquipamento.Equipamentos.Enqueue(equipamento);
        }

        public void Incluir(Contrato contratoLocacao)
        {
            ContratosLocacao.Add(contratoLocacao);
        }

        public void Liberar(int idContrato)
        {
            var contrato = ContratosLocacao.FirstOrDefault(c => c.Id == idContrato && !c.Liberado);

            if (contrato == null)
            { 
                throw new Exception("Nenhum contrato encontrado");
            }

            foreach (var solicitacao in contrato.Solicitacoes)
            {
                var tipoEquipamento = solicitacao.Key;
                var qtde = solicitacao.Value;

                var contratoEquipamento = new TipoEquipamento(tipoEquipamento.Id);
                for (int i = 0; i < qtde && tipoEquipamento.Equipamentos.Count > 0; i++)
                {
                    var item = tipoEquipamento.retornoEquipamento();

                    if (item.EquipamentoAvariado)
                    {
                        tipoEquipamento.incluirEquip(item);
                    }
                    else
                    {
                        contratoEquipamento.incluirEquip(item);
                    }
                }

                contrato.incluirEquip(contratoEquipamento);
            }

            contrato.Liberado = true;
        }

        public void Devolver(int idContrato)
        {
            var contrato = ContratosLocacao.FirstOrDefault(c => c.Id == idContrato && c.Liberado);

            if (contrato == null)
                throw new Exception("Contrato não encontrato");

            foreach (var tipoEquipamento in contrato.Equipamentos)
            {
                var currentTipoEquipamento = Estoque.FirstOrDefault(e => e.Id == tipoEquipamento.Id);

                while (tipoEquipamento.Equipamentos.Count > 0)
                {
                    currentTipoEquipamento.incluirEquip(tipoEquipamento.retornoEquipamento());
                }
            }
        }
        public TipoEquipamento ConsultarTiposEquipamento(int idTipoEquipamento)
        {
            return Estoque.FirstOrDefault(e => e.Id == idTipoEquipamento);
        }
        public List<Contrato> ConsultarContratosLocacao(bool liberados = false)
        {
            if (liberados)
            { 
                return ContratosLocacao.Where(c => c.Liberado).ToList();
            }

            return ContratosLocacao.ToList();
        }
    }
}
