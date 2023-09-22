using Blog_Pessoal.Data;
using Blog_Pessoal.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog_Pessoal.Service.Implements
{
    public class PostagemService : IPostagemService
    {
        private readonly AppDbContext _context;

        public PostagemService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Postagem?> Create(Postagem postagem)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Postagem postagem)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Postagem>> GetAll()
        {
            return await _context.Postagens.ToListAsync();
        }

        public Task<Postagem?> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Postagem>> GetByTitulo(string titulo)
        {
            throw new NotImplementedException();
        }

        public Task<Postagem?> Update(Postagem postagem)
        {
            throw new NotImplementedException();
        }
    }
}
