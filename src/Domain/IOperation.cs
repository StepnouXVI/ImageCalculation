namespace Domain;

public interface IOperation
{
    Priority Priority { get; }
    Associativity Associativity { get; }
    void Execute(DataStack dataStack);
}