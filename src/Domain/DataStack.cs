using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Domain;

public class DataStack
{
    private Stack<Image<Rgba32>?> _images = new Stack<Image<Rgba32>?>();
    private Stack<double> _numbers = new Stack<double>();

    public bool IsEmpty => _images.Count == 0 && _numbers.Count == 0;

    public void Push(Image<Rgba32>? image)
    {
        _images.Push(image);
    }
    
    public void Push(double number)
    {
        _numbers.Push(number);
    }

    public bool GetImage(out Image<Rgba32>? image)
    {
        return _images.TryPop(out image);
    }

    public bool GetNumber(out double number)
    {
        return _numbers.TryPop(out number);
    }
}