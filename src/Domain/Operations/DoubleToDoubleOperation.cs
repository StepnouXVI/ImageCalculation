namespace Domain.Operations;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public class DoubleToDoubleOperation(
    Func<double, double> function,
    Priority priority,
    Associativity associativity)
    : IOperation
{
    private readonly  Func<double, double> _function = function;
    
    public Priority Priority { get; } = priority;


    public Associativity Associativity { get; } = associativity;
    
    public void Execute(DataStack dataStack)
    {
        if (!dataStack.TryPopNumber(out var argument))
            throw new ImageProcessingException("Not enough arguments");

        dataStack.Push(_function(argument));
    }
}