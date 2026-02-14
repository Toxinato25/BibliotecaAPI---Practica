using System.Collections;

namespace BibliotecaAPI.Repository
{
    public interface IRepository<Tentity>
    {
        Task<IEnumerable<Tentity>> Get();

        Task<Tentity> GetById(int id);

        Task Create(Tentity entity);

        void Update(Tentity entity);

        void Delete(Tentity entity);

        Task Save();
    }
}
