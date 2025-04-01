[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-yamldotnet-variablereplacement.svg)](https://ci.appveyor.com/project/alunacjones/lsl-yamldotnet-variablereplacement)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.YamlDotNet.VariableReplacement)](https://coveralls.io/github/alunacjones/LSL.YamlDotNet.VariableReplacement)
[![NuGet](https://img.shields.io/nuget/v/LSL.YamlDotNet.VariableReplacement.svg)](https://www.nuget.org/packages/LSL.YamlDotNet.VariableReplacement/)

# LSL.YamlDotNet.VariableReplacement

A YamlDotNet parser to replace variables in all `Scalar` values of a `YAML` document as it is being parsed.

> **NOTE**: This library uses the variable replacement package found [here](https://www.nuget.org/packages/LSL.VariableReplacer/#readme-body-tab).

## Quick Start

The following example shows variable replacement in action on a `YAML` file:

```csharp
// Necessary usings:
using LSL.VariableReplacement;
using LSL.YamlDotNet.VariableReplacement;

...

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

var result = deserialiser.Deserialize<MyTest>(
    new VariableReplacerParser(replacer, new Parser(new StringReader(
    // Note the use of variables in keys as well as values
    """
    $(FieldName): $(FullName)
    Actual$(FieldName): $(FullName)
    Other: $(FullName)
    """
    )))
);

// result will have:
// result.Name == "Al Jones"
// result.ActualName == "Al Jones"
// result.Other == "Al Jones"
```
