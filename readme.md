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

### ModuleContract

[ModuleContract](./ModuleContract) defines the contract that [Module1](./Module1) and [Module2](./Module2) must implement in order to be loaded by [Application](./Application).

### ModuleLoader

[ModuleLoader](./ModuleLoader) provides the composition service to load modules used by [Application](./Application).

`CompositionHelper` is a generic class that can load modules that support a specified contract.

### Presentation.Core

[Presentation.Core](./Presentation.Core) provides supporting UI functionality and UI components for WPF.

User controls (`UserControl`) need to implement dependency properties (view models are not practical).

Note: to aid development, Visual Studio provides `propdp` as a shortcut to generate the boiler-plate code for dependency properties.

## Localisation

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