# Architecture

This is a repository for trying out architectural ideas in C# and WPF.

## Principles

The architecture attempts to demonstrate a number of key objectives including, but not limited to:

1. Modularity - independent modules of functionality loaded using [MEF] (https://mef.codeplex.com/).
2. Shared components - re-usable models, UI components.
3. Cross cutting - logging (implementation independent, implemented using [NLog](http://nlog-project.org/)).
4. Localisation - support different languages, assisted using [Visual Locbaml](http://visuallocbaml.com/).
5. DDD - domain driven design.
6. TDD - test driven design, supported using [NSubstitute](http://nsubstitute.github.io/).

## Overview

### Application

[Application](./Application) is the main application. This is responsible for loading the compatible modules so that the user can access the functionality of different modules.

The `MainWindowViewModel` has a `CompositionHelper` from [ModuleLoader](./ModuleLoader) to load any modules that implement `IModuleContract`.

Find all modules that implement IModuleContract:
```c#
_compositionHelper = new CompositionHelper<IModuleContract>();
_compositionHelper.AssembleModuleComponents();
```
This loads all modules from the current build folder.

### Logger

[Logger](./Logger) is the logging library that wraps the NLog implementation. This enables the logger to be replaced without affecting any logging code spread throughout the application.

### Module 1 and Module 2

[Module1](./Module1) and [Module2](./Module2) are separate modules that provide functionality to demonstrate how modules can be used by the main application. Implementation is limited to displaying the name and version of each module.

### Feature 1

[Feature1](./Feature1) is a module that provides functionality to the application or other modules.

### Contract

[IModuleContract](./Contract) defines the contract that [Module1](./Module1) and [Module2](./Module2) must implement in order to be loaded by [Application](./Application).

[IFeature1Contract](./Contract) defines the contract that [Feature1](./Feature1) must implement in order to be loaded by [Application](./Application).

### ModuleLoader

[ModuleLoader](./ModuleLoader) provides the composition service to load modules used by [Application](./Application).

`CompositionHelper` is a generic class that can load modules that support a specified contract.

### Presentation.Core

[Presentation.Core](./Presentation.Core) provides supporting UI functionality and UI components for WPF.

User controls (`UserControl`) need to implement dependency properties (view models are not practical).

Note: to aid development, Visual Studio provides `propdp` as a shortcut to generate the boiler-plate code for dependency properties.

## Localisation

Localisation is done using .resx files.

In all relevant projects, add to AssemblyInfo (setting default language):
```c#
using System.Resources;
[assembly: NeutralResourcesLanguage("en", UltimateResourceFallbackLocation.Satellite)]
```

Create the default language .resx and then translate into language-specific .resx files (e.g. .de.resx, .fr.resx etc.).

## UI

### Main Menu / Module Commands

The main menu is constructed taking into account each loaded module and features language localisation.

Each command identified as a menu item (or module command) implements `IModuleCommand` and has the `ModuleLocationAttribute`.

```c#
[ModuleLocation("File")]
[Export(typeof(IModuleCommand))]
public class MyCommand : IModuleCommand
```

# Future Work

## Event Sourcing and CQRS

Consider using the [CQRSlite NuGet package](https://www.nuget.org/packages/cqrslite).

CQRSlite is documented by Sacha Barber on Code Project [CQRS : A Cross Examination Of How It Works](https://www.codeproject.com/articles/991648/cqrs-a-cross-examination-of-how-it-works).

### Event Store

A file-based event store could be used with Events serialised in JSON.

JSON serialisation could be done using `JavaScriptSerializer`. By using the `SimpleTypeResolver` the type information is serialised with the data.

```c#
_javaScriptSerializer = new JavaScriptSerializer(new SimpleTypeResolver());
```

This is of use during deserialisation where only the underlying interface `IEvent` is specified.

```c#
var @event = _javaScriptSerializer.Deserialize<IEvent>(json);
```

This currently means that the JSON is dominated by the type information.

```
{"__type":"Hdd.Module1.Domain.ReadModel.Events.ItemCreatedEvent, Hdd.Module1.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Name":"Item 1","Id":"a5788a21-393b-4e3c-bf55-e3e07ed74bb2","Version":1,"TimeStamp":"\/Date(1481406875929)\/"}
{"__type":"Hdd.Module1.Domain.ReadModel.Events.ItemCreatedEvent, Hdd.Module1.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Name":"Item 1","Id":"597696ba-20eb-4af7-a364-e0c67b72802f","Version":1,"TimeStamp":"\/Date(1481406908079)\/"}
```

# Alternatives

## UI Localisation

I opted to go for .resx resources. WPF also supports BAML.

[Visual Locbaml](http://visuallocbaml.com/) is an free open-source tool to assist WPF application localisation.

To localise a WPF application, follow the instructions [Prepare for Localisation](http://visuallocbaml.com/docs/prepare_for_localization.html).

To summarise:

Add `UICulture` all relevant `.csproj` files (set appropriately).
```c#
<PropertyGroup>
  <UICulture>en-US</UICulture>
</PropertyGroup>
```

In all relevant projects, add to AssemblyInfo (setting default language):
```c#
using System.Resources;
[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]
```

Rebuild the project. This will add resources to a en-US folder in the build folder. Use Visual Locbaml to create new resources.

## String Resources

Modules would then be built with their own string resources for localisation. `ResourceDictionary` resources are created for each supported language in the /resources/languages/\<language code>/StringResources.xaml.

A `ResourceDictionary` could then be loaded using the `ResourceDictionaryLoader` (in history of repository - see [Module1Module](./Module1/Module1Module.cs) for an example of use).
