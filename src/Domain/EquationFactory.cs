using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Domain.Operations;

namespace Domain;

public class EquationFactory(OperationsData operationsData)
{
    private readonly OperationsData _operationsData = operationsData;
    private Stack<IOperation> _resultStack = new Stack<IOperation>();
    private Stack<IOperation> _tmpStack = new Stack<IOperation>();

    public Equation Create(Queue<string> lexems)
    {
        _resultStack = new Stack<IOperation>();
        _tmpStack.Clear();

        foreach (var lexeme in lexems)
        {
            if (TryNumberProcessor(lexeme))
                continue;
            if(TryImageProcessor(lexeme))
                continue;
            if(TryFunctionProcessor(lexeme))
                continue;
            if(TryOpenBracketProcessor(lexeme))
                continue;
            if(TryCloseBracketProcessor(lexeme))
                continue;
        }
        
        while (_tmpStack.Count > 0)
        {
            _resultStack.Push(_tmpStack.Pop());
        }

        return new Equation(_resultStack);
    }

    private bool TryNumberProcessor(string lexeme)
    {
        var result = double.TryParse(lexeme, out var number);

        if (result)
            NumberProcessor(number);
        return result;
    }

    private void NumberProcessor(double number)
    {
        _resultStack.Push(new DoubleOperation(number, Priority.Low, Associativity.Left));
    }


    private bool TryImageProcessor(string lexeme)
    {
        var result = _operationsData.Images.TryGetImage(lexeme, out var image);

        if (result && image is not null)
            ImageProcessor(image);

        return result;
    }

    private void ImageProcessor(Image<Rgba32> image)
    {
        _resultStack.Push(new ImageOperation(image, Priority.Low, Associativity.Left));
    }


    private bool TryCloseBracketProcessor(string lexeme)
    {
        var result = lexeme == _operationsData.CloseBracket;

        if (result)
        {
            CloseBracketProcessor();
        }
        
        return result;
    }
    
    private void CloseBracketProcessor()
    {
        while (_tmpStack.Count != 0 && _tmpStack.Peek().Priority != Priority.OpenBracket)
        {
            _resultStack.Push(_tmpStack.Pop());
        }

        if (_tmpStack.Count == 0)
            throw new ImageProcessingException("Invalid equation. Bracket error.");

        _tmpStack.Pop();
    }
    

    private bool TryOpenBracketProcessor(string lexeme)
    {
        var result = lexeme == _operationsData.OpenBracket;

        if (result)
        {
            CloseBracketProcessor();
        }
        
        return result;
    }

    private void OpenBracketProcessor()
    {
        _tmpStack.Push(new DoubleOperation(0, Priority.OpenBracket, Associativity.Left));
    }

    private bool TryFunctionProcessor(string lexeme)
    {
        var result = _operationsData.Operations.TryGetOperation(lexeme, out var operation);

        if (!result || operation is null)
            return false;


        switch (operation.Associativity)
        {
            case Associativity.Left:
                LeftAssociativityProcessor(operation);
                break;
            default:
                RightAssociativityProcessor(operation);
                break;
        }

        return true;
    }

    private void LeftAssociativityProcessor(IOperation operation)
    {
        while (_tmpStack.Count != 0 && operation.Priority <= _tmpStack.Peek().Priority)
        {
            _resultStack.Push(_tmpStack.Pop());
        }

        _tmpStack.Push(operation);
    }

    private void RightAssociativityProcessor(IOperation operation)
    {
        while (_tmpStack.Count != 0 && operation.Priority < _tmpStack.Peek().Priority)
        {
            _resultStack.Push(_tmpStack.Pop());
        }

        _tmpStack.Push(operation);
    }
}