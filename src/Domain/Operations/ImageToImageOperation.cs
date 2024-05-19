using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Domain.Operations;


/// <summary>
/// Represents an operation that transforms an input image into an output image using a specified function.
/// </summary>
public class ImageToImageOperation : IOperation
{
    private readonly Func<Image<Rgba32>, Image<Rgba32>> _function;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageToImageOperation"/> class.
    /// </summary>
    /// <param name="function">The function that performs the image transformation.</param>
    /// <param name="priority">The priority of the operation.</param>
    /// <param name="associativity">The associativity of the operation.</param>
    public ImageToImageOperation(
        Func<Image<Rgba32>, Image<Rgba32>> function,
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
    /// Executes the image transformation by applying the specified function to the input image.
    /// </summary>
    /// <param name="dataStack">The data stack containing the input image.</param>
    /// <exception cref="ImageProcessingException">Thrown when there are not enough arguments in the data stack.</exception>
    public void Execute(DataStack dataStack)
    {
        if (!dataStack.TryPopImage(out var inputImage))
            throw new ImageProcessingException("Not enough arguments");

        dataStack.Push(_function(inputImage));
    }
}