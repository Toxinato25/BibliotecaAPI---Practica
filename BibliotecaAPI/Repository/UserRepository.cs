using BibliotecaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Repository
{
    public class UserRepository : IRepository<Users>
    {
        private readonly BibliotecaContext _context;

        public UserRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> Get() =>
            await _context.Users.ToListAsync();
        
            
        
        public async Task<Users> GetById(int id) =>
            await _context.Users.FindAsync(id);

        public async Task Create(Users entity) =>
            await _context.Users.AddAsync(entity);


        public void Update(Users entity)
        {
            _context.Users.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
        }


        public void Delete(Users entity) =>
            _context.Users.Remove(entity);

        public async Task Save() =>
            await _context.SaveChangesAsync();
        
       
    }
}
