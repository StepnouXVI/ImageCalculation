namespace Domain.Operations;


using SixLabors.ImageSharp;

public class BinaryOperationDoublesToDouble(
    Func<double, double, double> function,
    Priority priority,
    Associativity associativity)
    : IOperation
{
    private readonly  Func<double, double, double> _function = function;
    
    public Priority Priority { get; } = priority;


    public Associativity Associativity { get; } = associativity;
    
    public void Execute(DataStack dataStack)
    {
        if (!dataStack.TryPopNumber(out var arg1) || !dataStack.TryPopNumber(out var arg2))
            throw new ImageProcessingException("Not enough arguments");

        dataStack.Push(_function(arg1, arg2));
    }
}