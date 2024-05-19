namespace Domain;
using Domain.Operations;

public interface IOperationsRepository
{
    bool TryGetOperation(string lexeme, out IOperation? operation);
}