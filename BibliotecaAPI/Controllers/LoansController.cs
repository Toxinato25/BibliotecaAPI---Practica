using BibliotecaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using BibliotecaAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using BibliotecaAPI.Validators;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanservice;
        private IValidator<LoanDto> _validator;

        public LoansController(ILoanService loanservice,
            IValidator<LoanDto> _validator)
        {
            _loanservice = loanservice;
            this._validator = _validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoans() => Ok(await _loanservice.GetAllLoans());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            var loanresult = await _loanservice.GetLoanById(id);
            if (loanresult == null)
                return NotFound("Prestamo no encontrado");

            return Ok(loanresult);
        }

        [HttpPost]
        public async Task<IActionResult> AddLoan([FromBody] LoanDto dto)
        {
            var validator = await _validator.ValidateAsync(dto);

            if(!validator.IsValid)
                return BadRequest(validator.Errors);

            var result = await _loanservice.AddLoan(dto);

            if (result == null)
                return BadRequest("No se pudo agregar el prestamo");

            return CreatedAtAction(nameof(GetLoanById), new { id = result.LoanId }, result);
        }

        [HttpPatch("return/{id}")]
        public async Task<IActionResult> ReturnLoan(int id)
        {
            var result = await _loanservice.ReturnLoan(id);

            if (result == null)
                return NotFound("Prestamo no encontrado o ya devuelto");

            return Ok(result);
        }
    }
}
