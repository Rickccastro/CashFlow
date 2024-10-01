using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCase.Expenses.Register;

public class RegisterExpensesUseCase : IRegisterExpensesUseCase
{
    private readonly IExpensesWriteOnlyRepository _repository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterExpensesUseCase(
        IExpensesWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task <ResponseRegisterExpenseJson> Execute(RequestExpenseJson request)
    {
        Validator(request);

        var loggedUser = await _loggedUser.GetLoggedUser();

        var expense = _mapper.Map<Expense>(request);

        expense.UserId = loggedUser.Id;

        await _repository.Add(expense);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpenseJson>(expense);
    }

    private void Validator(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMensages = result.Errors.Select(f => f.ErrorMessage).ToList();
            
            throw new ErrorOnValidationException(errorMensages);
                
        }
    }
}
