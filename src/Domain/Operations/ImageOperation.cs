using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Domain.Operations;


public class ImageOperation : IOperation
{
    private readonly Image<Rgba32> _image;
    public Priority Priority { get; }
    public Associativity Associativity { get; }

    public ImageOperation(Image<Rgba32> image, Priority priority, Associativity associativity)
    {
        _image = image;
        Priority = priority;
        Associativity = associativity;
    }
    public void Execute(DataStack dataStack)
    {
        dataStack.Push(_image);
    }
}