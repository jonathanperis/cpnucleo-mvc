﻿using Cpnucleo.Application.Common.Context;
using Cpnucleo.Shared.Commands.CreateApontamento;

namespace Cpnucleo.Application.Commands.CreateApontamento;

public sealed class CreateApontamentoCommandHandler : IRequestHandler<CreateApontamentoCommand, OperationResult>
{
    private readonly IApplicationDbContext _context;

    public CreateApontamentoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult> Handle(CreateApontamentoCommand request, CancellationToken cancellationToken)
    {
        var apontamento = Domain.Entities.Apontamento.Create(request.Descricao, request.DataApontamento, request.QtdHoras, request.IdTarefa, request.IdRecurso);
        _context.Apontamentos.Add(apontamento);

        bool success = await _context.SaveChangesAsync(cancellationToken);

        OperationResult result = success ? OperationResult.Success : OperationResult.Failed;

        return result;
    }
}