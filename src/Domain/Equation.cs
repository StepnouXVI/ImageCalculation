using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Domain.Operations;

namespace Domain;

public class Equation(Stack<IOperation> operations)
{
    public (Image<Rgba32>?, double) Execute()
    {
        var dataStack = new DataStack();

        while (operations.TryPop( out var operation))
        {
            operation.Execute(dataStack);
        }

        if (dataStack.Count != 1)
        {
            throw new ImageProcessingException("Invalid Equation");
        }

        dataStack.TryPopNumber(out var number);
        dataStack.TryPopImage(out var image);
        
        return (image, number);
    }
}