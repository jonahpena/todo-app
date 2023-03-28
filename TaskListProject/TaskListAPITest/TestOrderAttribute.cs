namespace TaskListAPITest;

public class TestOrderAttribute : Attribute
{
    public int Order { get; private set; }

    public TestOrderAttribute(int order)
    {
        Order = order;
    }
}
