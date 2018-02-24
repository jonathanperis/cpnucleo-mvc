﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using dotnet_cpnucleo_pages.Repository.Sistema;
using Microsoft.AspNetCore.Authorization;
using dotnet_cpnucleo_pages.Repository;

namespace dotnet_cpnucleo_pages.Pages.Sistema
{
    [Authorize]
    public class AlterarModel : PageModel
    {
        private readonly IRepository<SistemaItem> _sistemaRepository;

        public AlterarModel(IRepository<SistemaItem> sistemaRepository)
        {
            _sistemaRepository = sistemaRepository;
        }

        [BindProperty]
        public SistemaItem Sistema { get; set; }

        public async Task<IActionResult> OnGetAsync(int idSistema)
        {
            Sistema = await _sistemaRepository.Consultar(idSistema);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(SistemaItem sistema)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _sistemaRepository.Alterar(sistema);

            return RedirectToPage("Listar");
        }
    }
}