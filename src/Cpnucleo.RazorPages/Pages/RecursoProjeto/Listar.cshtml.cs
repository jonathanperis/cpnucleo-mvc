﻿using Cpnucleo.Infra.CrossCutting.Communication.API.Interfaces;
using Cpnucleo.Infra.CrossCutting.Identity.Interfaces;
using Cpnucleo.Infra.CrossCutting.Util.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cpnucleo.RazorPages.Pages.RecursoProjeto
{
    [Authorize]
    public class ListarModel : PageBase
    {
        private readonly IRecursoProjetoApiService _recursoProjetoApiService;

        public ListarModel(IClaimsManager claimsManager,
                                    IRecursoProjetoApiService recursoProjetoApiService)
            : base(claimsManager)
        {
            _recursoProjetoApiService = recursoProjetoApiService;
        }

        [BindProperty]
        public RecursoProjetoViewModel RecursoProjeto { get; set; }

        public IEnumerable<RecursoProjetoViewModel> Lista { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid idProjeto)
        {
            Lista = await _recursoProjetoApiService.ListarPorProjetoAsync(Token, idProjeto);

            ViewData["idProjeto"] = idProjeto;

            return Page();
        }
    }
}