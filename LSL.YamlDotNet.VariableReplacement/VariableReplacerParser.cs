using LSL.VariableReplacer;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace LSL.YamlDotNet.VariableReplacement;

/// <summary>
/// A variable replacing parser for <see href="https://github.com/aaubry/YamlDotNet"><c>YamlDotNet</c></see>
/// </summary>
/// <param name="variableReplacer"></param>
/// <param name="parser"></param>
public class VariableReplacerParser(IVariableReplacer variableReplacer, IParser parser) : IParser
{
    /// <inheritdoc/>
    public ParsingEvent Current => parser.Current is Scalar scalar 
        ? new Scalar(variableReplacer.ReplaceVariables(scalar.Value))
        : parser.Current;

    /// <inheritdoc/>
    public bool MoveNext() => parser.MoveNext();
}