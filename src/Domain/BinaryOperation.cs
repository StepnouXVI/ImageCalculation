namespace Domain;

public class BinaryOperation<TResult, T1, T2>(
    Func<TResult, T1, T2> function,
    Priority priority,
    Associativity associativity)
    : IOperation
{
    private readonly Func<TResult, T1, T2> _function = function;

    public Priority Priority { get; } = priority;

    public Associativity Associativity { get; } = associativity;

    public void Execute(DataStack dataStack)
    {
        
    }
}