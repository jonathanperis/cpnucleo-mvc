﻿namespace Cpnucleo.Infra.Data.Repositories;

internal class ImpedimentoTarefaRepository : GenericRepository<ImpedimentoTarefa>, IImpedimentoTarefaRepository
{
    public ImpedimentoTarefaRepository(CpnucleoContext context)
        : base(context)
    {

    }

    public async Task<IEnumerable<ImpedimentoTarefa>> GetByTarefaAsync(Guid idTarefa)
    {
        IEnumerable<ImpedimentoTarefa> result = await AllAsync(true);

        return result
            .Where(x => x.IdTarefa == idTarefa)
            .ToList();
    }
}
