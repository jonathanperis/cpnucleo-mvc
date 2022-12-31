﻿namespace Cpnucleo.Application.Commands.Sistema;

public sealed class UpdateSistemaHandler : IRequestHandler<UpdateSistemaCommand, OperationResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSistemaHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OperationResult> Handle(UpdateSistemaCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Sistema sistema = await _unitOfWork.SistemaRepository.Get(request.Id).FirstOrDefaultAsync(cancellationToken);

        if (sistema is null)
        {
            return OperationResult.NotFound;
        }

        sistema.Nome = request.Nome;
        sistema.Descricao = request.Descricao;

        _unitOfWork.SistemaRepository.Update(sistema);

        bool success = await _unitOfWork.SaveChangesAsync(cancellationToken);

        OperationResult result = success ? OperationResult.Success : OperationResult.Failed;

        return result;
    }
}
