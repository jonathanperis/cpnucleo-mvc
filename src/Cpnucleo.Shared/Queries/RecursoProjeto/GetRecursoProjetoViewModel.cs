﻿namespace Cpnucleo.Shared.Queries.RecursoProjeto;

public record GetRecursoProjetoViewModel : BaseQuery
{
    public RecursoProjetoDTO RecursoProjeto { get; set; }
    public OperationResult OperationResult { get; set; }
}