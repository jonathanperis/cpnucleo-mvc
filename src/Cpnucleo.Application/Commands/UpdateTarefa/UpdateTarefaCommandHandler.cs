﻿using Cpnucleo.Application.Common.Context;
using Cpnucleo.Shared.Commands.UpdateTarefa;

namespace Cpnucleo.Application.Commands.UpdateTarefa;

public sealed class UpdateTarefaCommandHandler : IRequestHandler<UpdateTarefaCommand, OperationResult>
{
    private readonly IApplicationDbContext _context;

    public UpdateTarefaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult> Handle(UpdateTarefaCommand request, CancellationToken cancellationToken)
    {
        var tarefa = await _context.Tarefas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (tarefa is null)
        {
            return OperationResult.NotFound;
        }

        tarefa = Domain.Entities.Tarefa.Update(tarefa,
                                                   request.Nome,
                                                   request.DataInicio,
                                                   request.DataTermino,
                                                   request.QtdHoras,
                                                   request.Detalhe,
                                                   request.IdProjeto,
                                                   request.IdWorkflow,
                                                   request.IdRecurso,
                                                   request.IdTipoTarefa);
        _context.Tarefas.Update(tarefa);

        bool success = await _context.SaveChangesAsync(cancellationToken);

        OperationResult result = success ? OperationResult.Success : OperationResult.Failed;

        return result;
    }
}