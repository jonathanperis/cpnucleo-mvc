using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnet_cpnucleo_pages.Repository.Tarefa;
using dotnet_cpnucleo_pages.Extension;

namespace dotnet_cpnucleo_pages.Repository.Apontamento
{
    public class ApontamentoRepository : IApontamentoRepository
    {
        private readonly ApontamentoContext _context;

        private readonly ITarefaRepository _tarefaRepository;

        public ApontamentoRepository(ApontamentoContext context,
                                    ITarefaRepository tarefaRepository)
        {
            _context = context;
            _tarefaRepository = tarefaRepository;
        }        

        public async Task Incluir(ApontamentoItem apontamento)
        {           
            apontamento.DataInclusao = DateTime.Now;
            
            _context.Apontamentos.Add(apontamento);
            await _context.SaveChangesAsync();

            var TarefaItem = await _tarefaRepository.Consultar(apontamento.IdTarefa);
            TarefaItem.PercentualConcluido = apontamento.PercentualConcluido;

            await _tarefaRepository.Alterar(TarefaItem);            
        }

        public async Task Alterar(ApontamentoItem apontamento)
        {
            var ApontamentoItem = _context.Apontamentos.Find(apontamento.IdApontamento);
            ApontamentoItem.Descricao = apontamento.Descricao;
            ApontamentoItem.DataApontamento = apontamento.DataApontamento;
            ApontamentoItem.QtdHoras = apontamento.QtdHoras;
            ApontamentoItem.PercentualConcluido = apontamento.PercentualConcluido;

            ApontamentoItem.DataAlteracao = DateTime.Now;

            _context.Apontamentos.Update(ApontamentoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<ApontamentoItem> Consultar(int idApontamento)
        {
            return await _context.Apontamentos
                .AsNoTracking()
                .Include(x => x.Tarefa)
                .SingleOrDefaultAsync(x => x.IdApontamento == idApontamento);
        }

        public async Task<IList<ApontamentoItem>> Listar()
        {
            return await _context.Apontamentos
                .AsNoTracking()
                .Include(x => x.Tarefa)
                .OrderBy(x => x.DataInclusao)
                .ToListAsync();
        }  

        public async Task Remover(ApontamentoItem Apontamento)
        {    
            var ApontamentoItem = _context.Apontamentos.Find(Apontamento.IdApontamento);            

            _context.Apontamentos.Remove(ApontamentoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<int> ObterTotalHorasPoridRecurso(int idRecurso, int idTarefa)
        {
            return await _context.Apontamentos
                .AsNoTracking()
                .Include(x => x.Tarefa)
                .OrderBy(x => x.DataInclusao)
                .Where(x => x.IdRecurso == idRecurso && x.IdTarefa == idTarefa)
                .SumAsync(x => x.QtdHoras);
        }              

        public async Task<IList<ApontamentoItem>> ListarAnalitico(int mesReferencia)
        {
            var listaApontamentos = await _context.Apontamentos
                .AsNoTracking()
                .Include(x => x.Tarefa)
                .Include(x => x.Tarefa.Projeto)
                .Include(x => x.Recurso)
                .OrderBy(x => x.DataInclusao)
                .Where(x => x.DataApontamento.Value.Month == mesReferencia)
                .ToListAsync();

            foreach (var apontamento in listaApontamentos)
            {
                apontamento.DiaSemana = string.Format("Semana {0}", apontamento.DataApontamento.Value.GetWeekOfMonth());
            } 

            return listaApontamentos;
        }     

        public async Task<IList<ApontamentoItem>> ListarPoridRecurso(int idRecurso)
        {
            return await _context.Apontamentos
                .AsNoTracking()
                .Include(x => x.Tarefa)
                .OrderBy(x => x.DataInclusao)
                .Where(x => x.IdRecurso == idRecurso && x.DataApontamento.Value > DateTime.Now.AddDays(-30))
                .ToListAsync();
        }                    
    }    
}