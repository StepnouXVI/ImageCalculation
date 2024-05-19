using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Domain.Operations;


/// <summary>
/// Represents an operation that converts an input image to a double value using a specified function.
/// </summary>
public class ImageToDoubleOperation : IOperation
{
    private readonly Func<Image<Rgba32>, double> _function;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageToDoubleOperation"/> class.
    /// </summary>
    /// <param name="function">The function that converts the image to a double value.</param>
    /// <param name="priority">The priority of the operation.</param>
    /// <param name="associativity">The associativity of the operation.</param>
    public ImageToDoubleOperation(
        Func<Image<Rgba32>, double> function,
        Priority priority,
        Associativity associativity)
    {
        _function = function;
        Priority = priority;
        Associativity = associativity;
    }

    /// <summary>
    /// Gets the priority of the operation.
    /// </summary>
    public Priority Priority { get; }

    /// <summary>
    /// Gets the associativity of the operation.
    /// </summary>
    public Associativity Associativity { get; }

    /// <summary>
    /// Executes the operation by converting the input image to a double value using the specified function.
    /// </summary>
    /// <param name="dataStack">The data stack containing the input image.</param>
    /// <exception cref="ImageProcessingException">Thrown when there are not enough arguments in the data stack.</exception>
    public void Execute(DataStack dataStack)
    {
        if (!dataStack.TryPopImage(out var inputImage))
            throw new ImageProcessingException("Not enough arguments");

        var result = _function(inputImage);
        dataStack.Push(result);
    }
}