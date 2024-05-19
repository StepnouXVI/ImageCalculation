using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Domain.Operations;


/// <summary>
/// Represents a binary operation that applies a specified function to two images and produces a new image.
/// </summary>
public class BinaryOperationImagesToImage(
    Func<Image<Rgba32>, Image<Rgba32>, Image<Rgba32>> function,
    Priority priority,
    Associativity associativity)
    : IOperation
{
    private readonly Func<Image<Rgba32>, Image<Rgba32>, Image<Rgba32>> _function = function;

    /// <summary>
    /// Gets the priority of the binary operation.
    /// </summary>
    public Priority Priority { get; } = priority;

    /// <summary>
    /// Gets the associativity of the binary operation.
    /// </summary>
    public Associativity Associativity { get; } = associativity;

    /// <summary>
    /// Executes the binary operation by applying the specified function to two images.
    /// </summary>
    /// <param name="dataStack">The data stack containing the images.</param>
    /// <exception cref="ImageProcessingException">Thrown when there are not enough arguments in the data stack.</exception>
    public void Execute(DataStack dataStack)
    {
        if (!dataStack.TryPopImage(out var image1) || !dataStack.TryPopImage(out var image2))
            throw new ImageProcessingException("Not enough arguments");

        dataStack.Push(_function(image1, image2));
    }
}
