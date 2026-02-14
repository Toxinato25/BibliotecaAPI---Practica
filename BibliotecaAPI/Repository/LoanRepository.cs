using BibliotecaAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BibliotecaAPI.Repository
{
    public class LoanRepository : IRepository<Loans>
    {
        private readonly BibliotecaContext _context;

        public LoanRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loans>> Get() =>
            await _context.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
            .ToListAsync();



        public async Task<Loans> GetById(int id) =>
            await _context.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.LoanId == id);

        public async Task Create(Loans entity) =>
            await _context.Loans.AddAsync(entity);


        public void Update(Loans entity)
        {
            _context.Loans.Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
        }


        public void Delete(Loans entity) =>
            _context.Loans.Remove(entity);

        public async Task Save() =>
            await _context.SaveChangesAsync();


    }
}
