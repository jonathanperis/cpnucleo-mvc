using Cpnucleo.Pages.Data;
using Cpnucleo.Pages.Models;
using Cpnucleo.Pages.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cpnucleo.Pages.Repository
{
    class RecursoRepository : IRecursoRepository
    {
        private readonly Context _context;

        public RecursoRepository(Context context) => _context = context;

        public async Task IncluirAsync(RecursoModel recurso)
        {
            CryptographyManager.CryptPbkdf2(recurso.Senha, out string itemCriptografado, out string salt);

            recurso.SenhaCriptografada = itemCriptografado;
            recurso.Salt = salt;
            recurso.DataInclusao = DateTime.Now;
            
            _context.Recursos.Add(recurso);
            await _context.SaveChangesAsync();
        }

        public async Task AlterarAsync(RecursoModel recurso)
        {
            var recursoItem = await ConsultarAsync(recurso.IdRecurso);

            CryptographyManager.CryptPbkdf2(recurso.Senha, out string itemCriptografado, out string salt);

            recursoItem.SenhaCriptografada = itemCriptografado;
            recursoItem.Salt = salt;
            recursoItem.Nome = recurso.Nome;
            recursoItem.Ativo = recurso.Ativo;
            recursoItem.DataAlteracao = DateTime.Now;

            _context.Recursos.Update(recursoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<RecursoModel> ConsultarAsync(int idRecurso)
        {
            return await _context.Recursos
                .SingleOrDefaultAsync(x => x.IdRecurso == idRecurso);
        }

        public async Task<IEnumerable<RecursoModel>> ListarAsync()
        {
            return await _context.Recursos
                .AsNoTracking()
                .OrderBy(y => y.DataInclusao)
                .ToListAsync();
        }

        public async Task RemoverAsync(RecursoModel recurso)
        {    
            var recursoItem = await ConsultarAsync(recurso.IdRecurso);            

            _context.Recursos.Remove(recursoItem);
            await _context.SaveChangesAsync();
        }

        public RecursoModel ValidarRecurso(string login, string senha, out bool valido)
        {
            valido = false;

            var recursoItem = _context.Recursos.SingleOrDefault(x => x.Login == login);

            if (recursoItem == null) return null;

            valido = CryptographyManager.VerifyPbkdf2(senha, recursoItem.SenhaCriptografada, recursoItem.Salt);

            return recursoItem;
        }        
    }
}