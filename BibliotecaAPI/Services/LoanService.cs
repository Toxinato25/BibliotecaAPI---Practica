using BibliotecaAPI.Data;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Repository;
using BibliotecaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

namespace BibliotecaAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly IRepository<Loans> _LoansRepository;
        private readonly IRepository<Users> _UsersRepository;
        private readonly IRepository<Books> _BooksRepository;

        public LoanService(IRepository<Loans> LoansRepository,
            IRepository<Users> UsersRepository,
            IRepository<Books> BooksRepository)
        {
            _LoansRepository = LoansRepository;
            _BooksRepository = BooksRepository;
            _UsersRepository = UsersRepository;

        }

        public async Task<IEnumerable<LoanDto>> GetAllLoans()
        {
            var loans = await _LoansRepository.Get();

            var result = loans.Select(loan => new LoanDto
            {
                LoanId = loan.LoanId,
                UserId = loan.UserId,
                UserName = loan.User.Username,
                BookId = loan.BookId,
                BookTitle = loan.Book.Title,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate
            });

            return result;


        }

        public async Task<LoanDto> GetLoanById(int id)
        {
           var loan = await _LoansRepository.GetById(id);


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
            var user = await _UsersRepository.GetById(loanDto.UserId);
                if (user == null)
                    return null;

            var book = await _BooksRepository.GetById(loanDto.BookId);
                if (book == null)
                return null;

            var loanInsert = new Loans // lo que el cliente ingresa
            {
                UserId = loanDto.UserId,
                BookId = loanDto.BookId,
                ReturnDate = null,
            };

            loanInsert.LoanDate = DateTime.Now;

            await _LoansRepository.Create(loanInsert);
            await _LoansRepository.Save();

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
            var loanToReturn = await _LoansRepository.GetById(id);

            if (loanToReturn == null || loanToReturn.ReturnDate != null)
            {
                return null;
            }

            loanToReturn.ReturnDate = DateTime.Now;

            await _LoansRepository.Save();

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
