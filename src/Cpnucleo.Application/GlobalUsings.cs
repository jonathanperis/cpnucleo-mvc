﻿global using AutoMapper;
global using Cpnucleo.Application.Common.Repositories.UoW;
global using Cpnucleo.Domain.Entities;
global using Cpnucleo.Domain.Services.Interfaces;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.Apontamento;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.Impedimento;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.ImpedimentoTarefa;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.Projeto;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.Recurso;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.RecursoProjeto;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.RecursoTarefa;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.Sistema;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.Tarefa;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.TipoTarefa;
global using Cpnucleo.Infra.CrossCutting.Shared.Commands.Workflow;
global using Cpnucleo.Infra.CrossCutting.Shared.Common.DTOs;
global using Cpnucleo.Infra.CrossCutting.Shared.Common.Models;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.Apontamento;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.Impedimento;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.ImpedimentoTarefa;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.Projeto;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.Recurso;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.RecursoProjeto;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.RecursoTarefa;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.Sistema;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.Tarefa;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.TipoTarefa;
global using Cpnucleo.Infra.CrossCutting.Shared.Queries.Workflow;
global using FluentValidation;
global using MediatR;