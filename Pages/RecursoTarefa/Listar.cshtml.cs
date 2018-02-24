﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using dotnet_cpnucleo_pages.Repository.RecursoTarefa;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace dotnet_cpnucleo_pages.Pages.RecursoTarefa
{
    [Authorize]
    public class ListarModel : PageModel
    {
        private readonly IRecursoTarefaRepository _RecursoTarefaRepository;

        public ListarModel(IRecursoTarefaRepository RecursoTarefaRepository)
        {
            _RecursoTarefaRepository = RecursoTarefaRepository;
        }

        [BindProperty]
        public RecursoTarefaItem RecursoTarefa { get; set; }

        [BindProperty]
        public IList<RecursoTarefaItem> Lista { get; set; }

        public async Task<IActionResult> OnGetAsync(int idTarefa)
        {
            Lista = await _RecursoTarefaRepository.ListarPoridTarefa(idTarefa);

            ViewData["idTarefa"] = idTarefa;

            return Page();
        }
    }
}