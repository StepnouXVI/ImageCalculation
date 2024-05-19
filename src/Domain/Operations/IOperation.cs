namespace Domain.Operations;


public interface IOperation
{
    Priority Priority { get; }
    Associativity Associativity { get; }
    void Execute(DataStack dataStack);
}