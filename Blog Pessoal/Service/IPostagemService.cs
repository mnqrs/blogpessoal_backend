using Blog_Pessoal.Model;
using System.Threading.Tasks;

namespace Blog_Pessoal.Service
{
    public interface IPostagemService
    {
        Task<IEnumerable<Postagem>> GetAll();

        Task<Postagem?> GetById(long id);

        Task<IEnumerable<Postagem>> GetByTitulo(string titulo);

        Task<Postagem?> Create(Postagem postagem);

        Task<Postagem?> Update(Postagem postagem);

        Task Delete (Postagem postagem);

    }
}
