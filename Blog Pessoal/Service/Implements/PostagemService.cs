using Blog_Pessoal.Data;
using Blog_Pessoal.Model;
using Microsoft.EntityFrameworkCore;

namespace Blog_Pessoal.Service.Implements
{
    public class PostagemService : IPostagemService
    {
        private readonly AppDbContext _context; // usa o underline pois é readonly

        public async Task<IEnumerable<Postagem>> GetAll()
        {
            return await _context.Postagens.ToListAsync();
        }

        public async Task<Postagem?> GetById(long id)
        {
            try
            {

                var Postagem = await _context.Postagens.FirstAsync(i => i.Id == id);

                return Postagem;

            }

            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Postagem>> GetByTitulo(string titulo)
        {
            var Postagem = await _context.Postagens
                                         .Where(p => p.Titulo.Contains(titulo))
                                         .ToListAsync();
            return Postagem;
        }
        public PostagemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Postagem?> Create(Postagem postagem)
        {
            await _context.Postagens.AddAsync(postagem);
            await _context.SaveChangesAsync();

            return postagem;
        }
                    
        public async Task<Postagem?> Update(Postagem postagem)
        {
            var PostagemUpdate = await _context.Postagens.FindAsync(postagem.Id); //primeiro busca a postagem para conseguir atualizar

            if(PostagemUpdate is null) 
                return null;

            _context.Entry(PostagemUpdate).State = EntityState.Detached; // não quer persistir o objeto na busca
            _context.Entry(postagem).State = EntityState.Modified; //objeto atualizado = postagem ao inves de PostagemUpdate
            await _context.SaveChangesAsync();

            return postagem;
        }
        public async Task Delete(Postagem postagem)
        {
            _context.Remove(postagem);
            await _context.SaveChangesAsync();
        }

    }
}
