﻿namespace StyleCop.Analyzers.Test.ReadabilityRules
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.Diagnostics;
    using StyleCop.Analyzers.ReadabilityRules;
    using TestHelper;
    using Xunit;

    /// <summary>
    /// This class contains unit tests for <see cref="SA1125UseShorthandForNullableTypes"/>.
    /// </summary>
    public class SA1125UnitTests : DiagnosticVerifier
    {
        /// <summary>
        /// This is a regression test for DotNetAnalyzers/StyleCopAnalyzers#385.
        /// <see href="https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/385">SA1125
        /// UseShorthandForNullableTypes incorrectly reported in XML comment</see>
        /// </summary>
        [Theory]
        [InlineData("Nullable{T}")]
        [InlineData("System.Nullable{T}")]
        [InlineData("global::System.Nullable{T}")]
        public async Task TestSeeAlsoNullable(string form)
        {
            string template = @"
namespace System
{{
    /// <seealso cref=""{0}""/>
    class ClassName
    {{
    }}
}}
";
            string testCode = string.Format(template, form);
            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        /// <summary>
        /// This is a regression test for DotNetAnalyzers/StyleCopAnalyzers#638.
        /// <see href="https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/638">SA1125
        /// UseShorthandForNullableTypes incorrectly reported for member in XML comment</see>
        /// </summary>
        [Theory]
        [InlineData("Nullable{T}")]
        [InlineData("System.Nullable{T}")]
        [InlineData("global::System.Nullable{T}")]
        public async Task TestSeeAlsoNullableValue(string form)
        {
            string template = @"
namespace System
{{
    /// <seealso cref=""{0}.Value""/>
    class ClassName
    {{
    }}
}}
";
            string testCode = string.Format(template, form);
            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        /// <summary>
        /// This is a regression test for DotNetAnalyzers/StyleCopAnalyzers#385.
        /// <see href="https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/385">SA1125
        /// UseShorthandForNullableTypes incorrectly reported in XML comment</see>
        /// </summary>
        [Theory]
        [InlineData("Nullable{int}", "int?")]
        [InlineData("System.Nullable{int}", "int?")]
        [InlineData("global::System.Nullable{int}", "int?")]
        public async Task TestSeeAlsoNullableShorthand(string longForm, string shortForm)
        {
            string template = @"
using System.Collections.Generic;
using System.Linq;
namespace System
{{
    /// <seealso cref=""Enumerable.Average(IEnumerable{{{0}}})""/>
    class ClassName
    {{
    }}
}}
";
            string testCode = string.Format(template, longForm);
            string fixedCode = string.Format(template, shortForm);

            DiagnosticResult expected = this.CSharpDiagnostic().WithLocation(6, 55);
            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        /// <summary>
        /// This is a regression test for DotNetAnalyzers/StyleCopAnalyzers#386.
        /// <see href="https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/386">SA1125
        /// UseShorthandForNullableTypes incorrectly reported in typeof()</see>
        /// </summary>
        [Theory]

        [InlineData("Nullable<int>", "int?")]
        [InlineData("System.Nullable<int>", "int?")]
        [InlineData("global::System.Nullable<int>", "int?")]

        [InlineData("Nullable<T>", "T?")]
        [InlineData("System.Nullable<T>", "T?")]
        [InlineData("global::System.Nullable<T>", "T?")]

        [InlineData("Nullable<>", "Nullable<>")]
        [InlineData("System.Nullable<>", "System.Nullable<>")]
        [InlineData("global::System.Nullable<>", "global::System.Nullable<>")]
        public async Task TestTypeOfNullable(string longForm, string shortForm)
        {
            string template = @"
namespace System
{{
    class ClassName<T>
        where T : struct
    {{
        Type nullableType = typeof({0});
    }}
}}
";
            string testCode = string.Format(template, longForm);
            string fixedCode = string.Format(template, shortForm);

            if (testCode != fixedCode)
            {
                DiagnosticResult expected = this.CSharpDiagnostic().WithLocation(7, 36);
                await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
            }

            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [Theory]

        [InlineData("Nullable<int>", "int?")]
        [InlineData("System.Nullable<int>", "int?")]
        [InlineData("global::System.Nullable<int>", "int?")]

        [InlineData("Nullable<T>", "T?")]
        [InlineData("System.Nullable<T>", "T?")]
        [InlineData("global::System.Nullable<T>", "T?")]
        public async Task TestNullableField(string longForm, string shortForm)
        {
            string template = @"
namespace System
{{
    class ClassName<T>
        where T : struct
    {{
        {0} nullableField;
    }}
}}
";
            string testCode = string.Format(template, longForm);
            string fixedCode = string.Format(template, shortForm);

            DiagnosticResult expected = this.CSharpDiagnostic().WithLocation(7, 9);
            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [Theory]

        [InlineData("Nullable<int>", "int?")]
        [InlineData("System.Nullable<int>", "int?")]
        [InlineData("global::System.Nullable<int>", "int?")]

        [InlineData("Nullable<T>", "T?")]
        [InlineData("System.Nullable<T>", "T?")]
        [InlineData("global::System.Nullable<T>", "T?")]
        public async Task TestDefaultNullableValue(string longForm, string shortForm)
        {
            string template = @"
namespace System
{{
    class ClassName<T>
        where T : struct
    {{
        void MethodName()
        {{
            var nullableValue = default({0});
        }}
    }}
}}
";
            string testCode = string.Format(template, longForm);
            string fixedCode = string.Format(template, shortForm);

            DiagnosticResult expected = this.CSharpDiagnostic().WithLocation(9, 41);
            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        /// <summary>
        /// This is a regression test for DotNetAnalyzers/StyleCopAnalyzers#637.
        /// <see href="https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/637">SA1125
        /// UseShorthandForNullableTypes incorrectly reported in <c>nameof</c> expression</see>
        /// </summary>
        [Theory]
        [InlineData("Nullable<int>")]
        [InlineData("System.Nullable<int>")]
        [InlineData("global::System.Nullable<int>")]
        public async Task TestNameOfNullable(string form)
        {
            string template = @"
namespace System
{{
    class ClassName<T>
        where T : struct
    {{
        string nullableName = nameof({0});
    }}
}}
";
            string testCode = string.Format(template, form);
            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        /// <summary>
        /// This is a regression test for DotNetAnalyzers/StyleCopAnalyzers#636.
        /// <see href="https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/636">SA1125
        /// UseShorthandForNullableTypes incorrectly reported for static access through Nullable&lt;int&gt;</see>
        /// </summary>
        /// <remarks>
        /// <para>This special case of instance access through <c>Nullable&lt;int&gt;</c> was mentioned in a
        /// comment.</para>
        /// </remarks>
        [Theory]
        [InlineData("Nullable<int>")]
        [InlineData("System.Nullable<int>")]
        [InlineData("global::System.Nullable<int>")]
        public async Task TestNameOfNullableValue(string form)
        {
            string template = @"
namespace System
{{
    class ClassName<T>
        where T : struct
    {{
        string nullableName = nameof({0}.Value);
    }}
}}
";
            string testCode = string.Format(template, form);
            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        /// <summary>
        /// This is a regression test for DotNetAnalyzers/StyleCopAnalyzers#636.
        /// <see href="https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/636">SA1125
        /// UseShorthandForNullableTypes incorrectly reported for static access through Nullable&lt;int&gt;</see>
        /// </summary>
        [Theory]
        [InlineData("Nullable<int>")]
        [InlineData("System.Nullable<int>")]
        [InlineData("global::System.Nullable<int>")]
        public async Task TestAccessObjectEqualThroughNullable(string form)
        {
            string template = @"
namespace System
{{
    class ClassName<T>
        where T : struct
    {{
        bool equal = {0}.Equals(null, null);
    }}
}}
";
            string testCode = string.Format(template, form);
            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [Theory]

        [InlineData("Nullable<int>", "int?")]
        [InlineData("System.Nullable<int>", "int?")]
        [InlineData("global::System.Nullable<int>", "int?")]

        [InlineData("Nullable<T>", "T?")]
        [InlineData("System.Nullable<T>", "T?")]
        [InlineData("global::System.Nullable<T>", "T?")]
        public async Task TestNameOfListOfNullable(string longForm, string shortForm)
        {
            string template = @"
using System.Collections.Generic;
namespace System
{{
    class ClassName<T>
        where T : struct
    {{
        string nullableName = nameof(List<{0}>);
    }}
}}
";
            string testCode = string.Format(template, longForm);
            string fixedCode = string.Format(template, shortForm);

            DiagnosticResult expected = this.CSharpDiagnostic().WithLocation(8, 43);
            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);
            await this.VerifyCSharpDiagnosticAsync(fixedCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new SA1125UseShorthandForNullableTypes();
        }
    }
}
