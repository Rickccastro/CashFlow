using Cashflow.Api.Middleware;
using System.Collections;

namespace WebApi.Test.InlineData;

public class CultureInlineDataTest : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
       yield return new object[] {"pt-BR"};
       yield return new object[] {"eng"};
       yield return new object[] {"pt-PT"};
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
