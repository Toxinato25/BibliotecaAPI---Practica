using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

namespace BibliotecaAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly BibliotecaContext _context;

        public LoanService(BibliotecaContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<LoanDto>> GetAllLoans() =>
            await _context.Loans
            .Include(l => l.User)
            .Include(l => l.Book)
            .Select(l => new LoanDto
            {
                LoanId = l.LoanId,
                UserId = l.UserId,
                UserName = l.User.Username,
                BookId = l.BookId,
                BookTitle = l.Book.Title,
                LoanDate = l.LoanDate,
                ReturnDate = l.ReturnDate
            }).ToListAsync();

        public async Task<LoanDto> GetLoanById(int id)
        {
            var loan = await _context.Loans
                .Include(l => l.User)
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.LoanId == id);


            if (loan == null)
                return null;

            var loandto = new LoanDto
            {
                LoanId = loan.LoanId,
                UserId = loan.UserId,
                UserName = loan.User.Username,
                BookId = loan.BookId,
                BookTitle = loan.Book.Title,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate

            };

            return loandto;
        }

        public async Task<LoanDto> AddLoan(LoanDto loanDto)
        {
            var user = await _context.Users.FindAsync(loanDto.UserId);
                if (user == null)
                    return null;

            var book = await _context.Books.FindAsync(loanDto.BookId);
                if (book == null)
                return null;

            var loanInsert = new Loans // lo que el cliente ingresa
            {
                UserId = loanDto.UserId,
                BookId = loanDto.BookId,
                ReturnDate = null,
            };

            await _context.Loans.AddAsync(loanInsert);
            await _context.SaveChangesAsync();

            var loanResult = new LoanDto // lo que se retorna al header
            {
                LoanId = loanInsert.LoanId,
                UserId = loanInsert.UserId,
                UserName = loanInsert.User.Username,
                BookId = loanInsert.BookId,
                BookTitle = loanInsert.Book.Title,
                LoanDate = loanInsert.LoanDate
            };

            return loanResult;

        }

        public async Task<LoanDto> ReturnLoan(int id)
        {
            var loanToReturn = await _context.Loans
                .Include(l => l.User)
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.LoanId == id);

            if (loanToReturn == null || loanToReturn.ReturnDate != null)
            {
                return null;
            }

            loanToReturn.ReturnDate = DateTime.Now;

            await _context.SaveChangesAsync();

            var loanReturned = new LoanDto // lo que se retorna al header
            {
                LoanId = loanToReturn.LoanId,
                UserId = loanToReturn.UserId,
                UserName = loanToReturn.User.Username,
                BookId = loanToReturn.BookId,
                BookTitle = loanToReturn.Book.Title,
                LoanDate = loanToReturn.LoanDate,
                ReturnDate = loanToReturn.ReturnDate
            };

            return loanReturned;
        }
    }
}
