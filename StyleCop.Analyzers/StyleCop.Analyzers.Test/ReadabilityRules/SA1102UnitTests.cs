﻿namespace StyleCop.Analyzers.Test.ReadabilityRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.Diagnostics;
    using StyleCop.Analyzers.ReadabilityRules;
    using TestHelper;
    using Xunit;

    public class SA1102UnitTests : CodeFixVerifier
    {
        [Fact]
        public async Task TestEmptySource()
        {
            var testCode = string.Empty;
            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [Fact]
        public async Task TestSelectOnSeparateLineWithAdditionalEmptyLine()
        {
            var testCode = @"
using System.Linq;
public class Foo4
{
    public void Bar()
    {
        var source = new int[0];

        var query = from m in source
                    where m > 0

                    select m;
    }
}";
            DiagnosticResult expected = this.CSharpDiagnostic().WithLocation(12, 21);

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
        }

        [Fact]
        public async Task TestWhereSelectOnSameLine()
        {
            var testCode = @"
using System.Linq;
public class Foo4
{
    public void Bar()
    {
        var source = new int[0];

        var query = from m in source
                    where m > 0 select m;
    }
}";
            DiagnosticResult expected = this.CSharpDiagnostic().WithLocation(10, 33);

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
        }

        [Fact]
        public async Task TestWhereOnTheSameLineAsFrom()
        {
            var testCode = @"
using System.Linq;
public class Foo4
{
    public void Bar()
    {
        var source = new int[0];
        var query = from m in source where m > 0
                    select m;
    }
}";
            DiagnosticResult expected = this.CSharpDiagnostic().WithLocation(8, 38);

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
        }

        [Fact]
        public async Task TestComplexQueryWithAdditionalEmptyLine()
        {
            var testCode = @"
using System.Linq;
public class Foo4
{
    public void Bar()
    {
        var source = new int[0];
        var source2 = new int[0];

        var query = from m in source

                    let z  = source.Take(10)

                    join f in source2  
                    on m equals f

                    where m > 0 && 
                    m < 1

                    group m by m into g

                    select new {g.Key, Sum = g.Sum()};
    }
}";
            DiagnosticResult[] expected =
                {
                    this.CSharpDiagnostic().WithLocation(12, 21),
                    this.CSharpDiagnostic().WithLocation(14, 21),
                    this.CSharpDiagnostic().WithLocation(17, 21),
                    this.CSharpDiagnostic().WithLocation(20, 21),
                    this.CSharpDiagnostic().WithLocation(22, 21),
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
        }

        [Fact]
        public async Task TestComplexQueryInOneLine()
        {
            var testCode = @"
using System.Linq;
public class Foo4
{
    public void Bar()
    {
        var source = new int[0];
        var source2 = new int[0];

        var query = from m in source let z  = source.Take(10) join f in source2 on m equals f where m > 0 && m < 1 group m by m into g select new {g.Key, Sum = g.Sum()};
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [Fact]
        public async Task TestQueryInsideQuery()
        {
            var testCode = @"
using System.Linq;
public class Foo4
{
    public void Bar()
    {
                var query = from m in (from s in Enumerable.Empty<int>()
                where s > 0 select s)

                where m > 0

                orderby m descending 
                select m;
    }
}";

            DiagnosticResult[] expected =
                {
                    this.CSharpDiagnostic().WithLocation(8, 29),
                    this.CSharpDiagnostic().WithLocation(10, 17),
                    this.CSharpDiagnostic().WithLocation(12, 17),
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
        }

        [Fact]
        public async Task QueryInsideQueryComplex()
        {
            var testCode = @"
using System.Linq;
public class Foo4
{
    public void Bar()
    {
                var query = from m in (from s in Enumerable.Empty<int>()
                where s > 0 select s)

                where m > 0 && (from zz in Enumerable.Empty<int>()


                    select zz).Max() > m

                orderby m descending
                select (from pp in new[] {m}

                    select pp);
    }
}";

            DiagnosticResult[] expected =
                {
                    this.CSharpDiagnostic().WithLocation(8, 29),
                    this.CSharpDiagnostic().WithLocation(10, 17),
                    this.CSharpDiagnostic().WithLocation(13, 21),
                    this.CSharpDiagnostic().WithLocation(15, 17),
                    this.CSharpDiagnostic().WithLocation(18, 21),
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new SA1102QueryClauseMustFollowPreviousClause();
        }
    }
}
