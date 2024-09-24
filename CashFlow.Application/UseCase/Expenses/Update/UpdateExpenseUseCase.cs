using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories;
using CashFlow.Communication.Requests;
using Microsoft.Extensions.Options;
using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Exception.ExceptionBase;
using AutoMapper;
using CashFlow.Domain.Entities;
using System.Resources;
using CashFlow.Exception;

namespace CashFlow.Application.UseCase.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IMapper _mapper;
    private readonly IExpensesUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateExpenseUseCase(IExpensesUpdateOnlyRepository repository, IUnitOfWork unitOfWork,IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;    
    }
    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var expense = await _repository.GetById(id);

        if(expense is null)
        {
            throw new NotFoundException(ResourceErrorMenssages.EXPENSE_NOT_FOUND);
        }

        _mapper.Map(request, expense);

        _repository.Update(expense);

        await _unitOfWork.Commit();
    }

    public Task<Expense?> GetByID(long id)
    {
        throw new NotImplementedException();
    }

    private void Validate(RequestExpenseJson request)
    {
       var validator = new ExpenseValidator();
       var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);

        }
    }
}
