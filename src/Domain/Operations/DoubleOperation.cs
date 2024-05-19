namespace Domain.Operations;


public class DoubleOperation :IOperation
{
    private readonly double _number;
    public Priority Priority { get; }
    public Associativity Associativity { get; }

    public DoubleOperation(double number, Priority priority, Associativity associativity)
    {
        _number = number;
        Priority = priority;
        Associativity = associativity;
    }
    public void Execute(DataStack dataStack)
    {
        dataStack.Push(_number);
    }
}