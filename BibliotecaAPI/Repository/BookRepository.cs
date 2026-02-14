using BibliotecaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Repository
{
    public class BookRepository : IRepository<Books>
    {
        private readonly BibliotecaContext _context;

        public BookRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Books>> Get() =>
            await _context.Books.ToListAsync();



        public async Task<Books> GetById(int id) =>
            await _context.Books.FindAsync(id);

        public async Task Create(Books entity) =>
            await _context.Books.AddAsync(entity);


        public void Update(Books entity)
        {
            _context.Books.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
        }


        public void Delete(Books entity) =>
            _context.Books.Remove(entity);

        public async Task Save() =>
            await _context.SaveChangesAsync();

    }
}
