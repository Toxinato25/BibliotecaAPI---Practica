using BibliotecaAPI.DTOs;

namespace BibliotecaAPI.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDto>> GetAllLoans();

        Task<LoanDto> GetLoanById(int id);

        Task<LoanDto> AddLoan(LoanDto dto);

        Task<LoanDto> ReturnLoan(int id);
    }
}
