﻿namespace Cpnucleo.Shared.Queries.TipoTarefa;

public record GetTipoTarefaViewModel : BaseQuery
{
    public TipoTarefaDTO TipoTarefa { get; set; }
    public OperationResult OperationResult { get; set; }
}