﻿using Cpnucleo.Domain.Common.Security.Interfaces;
using Cpnucleo.Shared.Requests.Auth;

namespace Cpnucleo.Application.Requests.Auth;

public sealed class AuthHandler : IRequestHandler<AuthRequest, AuthResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICryptographyManager _cryptographyManager;

    public AuthHandler(IUnitOfWork unitOfWork, IMapper mapper, ICryptographyManager cryptographyManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cryptographyManager = cryptographyManager;
    }

    public async Task<AuthResponse> Handle(AuthRequest request, CancellationToken cancellationToken)
    {
        AuthResponse result = new()
        {
            Status = OperationResult.Failed
        };

        Recurso recurso = await _unitOfWork.RecursoRepository.GetRecursoByLogin(request.Usuario)
            .FirstOrDefaultAsync(cancellationToken);

        if (recurso is null)
        {
            result.Status = OperationResult.NotFound;

            return result;
        }

        result.Recurso = _mapper.Map<RecursoDTO>(recurso);

        bool success = _cryptographyManager.VerifyPbkdf2(request.Senha, recurso.Senha, recurso.Salt);

        result.Status = success ? OperationResult.Success : OperationResult.Failed;

        return result;
    }
}
