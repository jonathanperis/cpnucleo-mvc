﻿using Cpnucleo.Pages.Models;
using Cpnucleo.Pages.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cpnucleo.Pages.Pages.ImpedimentoTarefa
{
    [Authorize]
    public class ListarModel : PageModel
    {
        private readonly IImpedimentoTarefaRepository _impedimentoTarefaRepository;

        public ListarModel(IImpedimentoTarefaRepository impedimentoTarefaRepository) => _impedimentoTarefaRepository = impedimentoTarefaRepository;

        [BindProperty]
        public ImpedimentoTarefaModel ImpedimentoTarefa { get; set; }

        public IEnumerable<ImpedimentoTarefaModel> Lista { get; set; }

        public async Task<IActionResult> OnGetAsync(int idTarefa)
        {
            Lista = await _impedimentoTarefaRepository.ListarPoridTarefaAsync(idTarefa);

            ViewData["idTarefa"] = idTarefa;

            return Page();
        }
    }
}