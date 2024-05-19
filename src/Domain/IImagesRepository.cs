using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Domain;
using Domain.Operations;

public interface IImagesRepository
{
    bool TryGetImage(string lexeme, out Image<Rgba32>? image); 
}