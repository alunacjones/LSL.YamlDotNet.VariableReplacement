using System;
using LSL.VariableReplacer;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace LSL.YamlDotNet.VariableReplacement;

/// <summary>
/// A variable replacing parser for <see href="https://github.com/aaubry/YamlDotNet"><c>YamlDotNet</c></see>
/// </summary>
/// <param name="variableReplacer">
/// A function that takes a source string
/// and returns a string with any variables replaced
/// </param>
/// <param name="parser">
/// An inner parser to delegate work to such as <see cref="Parser"/>
/// </param>
public class VariableReplacerParser(Func<string, string> variableReplacer, IParser parser) : IParser
{
    /// <summary>
    /// Constructor to use to pass in an <see cref="IVariableReplacer"/>
    /// </summary>
    /// <param name="variableReplacer">
    /// A <see cref="IVariableReplacer"/> to perform the variable replacement
    /// </param>
    /// <param name="parser">
    /// An inner parser to delegate work to such as <see cref="Parser"/>
    /// </param>
    public VariableReplacerParser(IVariableReplacer variableReplacer, IParser parser) : this(variableReplacer.ReplaceVariables, parser) {}

    /// <inheritdoc/>
    public ParsingEvent Current => parser.Current is Scalar scalar 
        ? new Scalar(variableReplacer(scalar.Value))
        : parser.Current;

    /// <inheritdoc/>
    public bool MoveNext() => parser.MoveNext();
}