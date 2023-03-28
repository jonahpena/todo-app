using Xunit.Abstractions;
using Xunit.Sdk;

namespace TaskListAPITest;
public class TestMethodOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        var sortedMethods = new SortedList<int, TTestCase>();

        foreach (TTestCase testCase in testCases)
        {
            IAttributeInfo testOrderAttribute = testCase.TestMethod.Method.GetCustomAttributes(typeof(TestOrderAttribute)).SingleOrDefault();
            if (testOrderAttribute != null)
            {
                int order = testOrderAttribute.GetNamedArgument<int>("Order");
                sortedMethods.Add(order, testCase);
            }
        }

        return sortedMethods.Values;
    }
}
