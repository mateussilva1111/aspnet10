using Xunit.Abstractions;
using Xunit.Sdk;

namespace aspnet10Test.IntegrationTests.Tools
{
    public class PriorityOrder : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
            IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            return testCases.OrderBy(
                tc => tc.TestMethod.Method
                    .GetCustomAttributes(typeof(TestPriorityAttribute).AssemblyQualifiedName)
                    .FirstOrDefault()
                    ?.GetNamedArgument<int>("Priority") ?? 0
            );
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class TestPriorityAttribute : Attribute
    {
        public int Priority { get; }

        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}