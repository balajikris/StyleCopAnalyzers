﻿namespace StyleCop.Analyzers.ReadabilityRules
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// A parameter to a C# method or indexer, other than the first parameter, spans across multiple lines.
    /// </summary>
    /// <remarks>
    /// <para>To prevent method calls from becoming excessively complicated and unreadable, only the first parameter to
    /// a method or indexer call is allowed to span across multiple lines. The exception is an anonymous method passed
    /// as a parameter, which is always allowed to span multiple lines. A violation of this rule occurs whenever a
    /// parameter other than the first parameter spans across multiple lines, and the parameter does not contain an
    /// anonymous method.</para>
    ///
    /// <para>For example, the following code would violate this rule, since the second parameter spans across multiple
    /// lines:</para>
    ///
    /// <code language="csharp">
    /// return JoinStrings(
    ///     "John",
    ///     "Smith" + 
    ///     " Doe");
    /// </code>
    ///
    /// <para>When parameters other than the first parameter span across multiple lines, it can be difficult to tell how
    /// many parameters are passed to the method. In general, the code becomes difficult to read.</para>
    ///
    /// <para>To fix the example above, ensure that the parameters after the first parameter do not span across multiple
    /// lines. If this will cause a parameter to be excessively long, store the value of the parameter within a
    /// temporary variable. For example:</para>
    ///
    /// <code language="csharp">
    /// string last = "Smith" +
    ///     " Doe";
    ///
    /// return JoinStrings(
    ///     "John",
    ///     last);
    /// </code>
    ///
    /// <para>In some cases, this will allow the method to be written even more concisely, such as:</para>
    ///
    /// <code language="csharp">
    /// return JoinStrings("John", last);
    /// </code>
    /// </remarks>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SA1118ParameterMustNotSpanMultipleLines : DiagnosticAnalyzer
    {
        /// <summary>
        /// The ID for diagnostics produced by the <see cref="SA1118ParameterMustNotSpanMultipleLines"/> analyzer.
        /// </summary>
        public const string DiagnosticId = "SA1118";
        private const string Title = "Parameter must not span multiple lines";
        private const string MessageFormat = "TODO: Message format";
        private const string Category = "StyleCop.CSharp.ReadabilityRules";
        private const string Description = "A parameter to a C# method or indexer, other than the first parameter, spans across multiple lines.";
        private const string HelpLink = "http://www.stylecop.com/docs/SA1118.html";

        private static readonly DiagnosticDescriptor Descriptor =
            new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, AnalyzerConstants.DisabledNoTests, Description, HelpLink);

        private static readonly ImmutableArray<DiagnosticDescriptor> SupportedDiagnosticsValue =
            ImmutableArray.Create(Descriptor);

        /// <inheritdoc/>
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return SupportedDiagnosticsValue;
            }
        }

        /// <inheritdoc/>
        public override void Initialize(AnalysisContext context)
        {
            // TODO: Implement analysis
        }
    }
}
