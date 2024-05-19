using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Domain.Operations;

namespace Domain;

public class OperationsData(
    IOperationsRepository operations,
    IImagesRepository images,
    string openBracket,
    string closeBracket)
{
    public IOperationsRepository Operations { get; } = operations;
    public IImagesRepository Images { get; } = images;

    public string OpenBracket { get; } = openBracket;
    public string CloseBracket { get; } = closeBracket;
}