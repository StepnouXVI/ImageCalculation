using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Domain;

public class DataStack
{
    private Stack<Image<Rgba32>?> _images = new();
    private Stack<double> _numbers = new Stack<double>();

    public bool IsEmpty => _images.Count == 0 && _numbers.Count == 0;
    
    public int Count => _images.Count + _numbers.Count;

    public void Push(Image<Rgba32>? image)
    {
        _images.Push(image);
    }
    
    public void Push(double number)
    {
        _numbers.Push(number);
    }

    public bool TryPopImage(out Image<Rgba32>? image)
    {
        return _images.TryPop(out image);
    }

    public bool TryPopNumber(out double number)
    {
        return _numbers.TryPop(out number);
    }
}