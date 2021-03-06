﻿namespace StyleCop.Analyzers.DocumentationRules
{
    using System.Collections.Immutable;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;

    /// <summary>
    /// The file tag within the file header at the top of a C# code file does not contain the name of the file.
    /// </summary>
    /// <remarks>
    /// <para>A violation of this rule occurs when the file tag within the file header at the top of a C# file does not
    /// contain the name of the file. For example, consider a C# source file named File1.cs, with the following
    /// header:</para>
    ///
    /// <code language="csharp">
    /// //-----------------------------------------------------------------------
    /// // &lt;copyright file="File2.cs" company="My Company"&gt;
    /// //     Custom company copyright tag.
    /// // &lt;/copyright&gt;
    /// //-----------------------------------------------------------------------
    /// </code>
    ///
    /// <para>A violation of this rule would occur, since the file tag does not contain the correct name of the file.
    /// The header should be written as:</para>
    ///
    /// <code language="csharp">
    /// //-----------------------------------------------------------------------
    /// // &lt;copyright file="File1.cs" company="My Company"&gt;
    /// //     Custom company copyright tag.
    /// // &lt;/copyright&gt;
    /// //-----------------------------------------------------------------------
    /// </code>
    /// </remarks>
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SA1638FileHeaderFileNameDocumentationMustMatchFileName : DiagnosticAnalyzer
    {
        /// <summary>
        /// The ID for diagnostics produced by the <see cref="SA1638FileHeaderFileNameDocumentationMustMatchFileName"/>
        /// analyzer.
        /// </summary>
        public const string DiagnosticId = "SA1638";
        private const string Title = "File header file name documentation must match file name";
        private const string MessageFormat = "TODO: Message format";
        private const string Category = "StyleCop.CSharp.DocumentationRules";
        private const string Description = "The file tag within the file header at the top of a C# code file does not contain the name of the file.";
        private const string HelpLink = "http://www.stylecop.com/docs/SA1638.html";

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
