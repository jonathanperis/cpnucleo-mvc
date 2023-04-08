﻿using Cpnucleo.Shared.Commands.UpdateProjeto;

namespace Cpnucleo.Application.Commands.UpdateProjeto;

public sealed class UpdateProjetoCommandValidator : AbstractValidator<UpdateProjetoCommand>
{
    public UpdateProjetoCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Nome).MaximumLength(50);
        RuleFor(x => x.IdSistema).NotEmpty();
    }
}