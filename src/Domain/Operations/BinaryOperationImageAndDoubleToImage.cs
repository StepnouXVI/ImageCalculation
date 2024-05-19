namespace Domain.Operations;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public class BinaryOperationImageAndDoubleToImage(
    Func<Image<Rgba32>, double, Image<Rgba32>> function,
    Priority priority,
    Associativity associativity)
    : IOperation
{
    private readonly Func<Image<Rgba32>, double, Image<Rgba32>> _function = function;
    
    public Priority Priority { get; } = priority;


    public Associativity Associativity { get; } = associativity;
    
    public void Execute(DataStack dataStack)
    {
        if (!dataStack.TryPopImage(out var image) || !dataStack.TryPopNumber(out var number))
            throw new ImageProcessingException("Not enough arguments");

        dataStack.Push(_function(image, number));
    }
}
