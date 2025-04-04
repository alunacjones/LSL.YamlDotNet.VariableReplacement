using System.Collections.Generic;
using System.IO;
using LSL.VariableReplacer;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace LSL.YamlDotNet.VariableReplacement.Tests;

public class Tests
{
    [Test]
    public void Test()
    {
        // Arrange
        var replacer = new VariableReplacerFactory()
            .Build(c => c.AddVariables(new Dictionary<string, object>
            {
                ["FirstName"] = "Al",
                ["LastName"] = "Jones",
                ["FieldName"] = "Name",
                ["FullName"] = "$(FirstName) $(LastName)"
            }));

        var deserialiser = new DeserializerBuilder()
            .Build();

        // Act        
        var result = deserialiser.Deserialize<MyTest>(
            new VariableReplacerParser(replacer.ReplaceVariables, new Parser(new StringReader(
            // Note the use of variables in keys as well as values
            """
            $(FieldName): $(FullName)
            Actual$(FieldName): $(FullName)
            Other: $(FullName)
            """
            )))
        );

        // Assert
        using var assertionScope = new AssertionScope();

        result.ActualName.Should().Be("Al Jones");
        result.Name.Should().Be("Al Jones");
        result.Other.Should().Be("Al Jones");
    }

    internal class MyTest
    {
        public string Name { get; set; }
        public string ActualName { get; set; }
        public string Other { get; set; }
    }
}