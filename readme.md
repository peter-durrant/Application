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

#### Menu

Each command identified as a menu item implemented as `IModuleCommand` and has the `MenuGroupItemAttribute` to identify where in the menu structure the command should appear. The commands implement `INotifyPropertyChanged`
to ensure that WPF containers are updated if the menu items are updated.

```c#
[MenuGroupItem("File")]
[Export(typeof(IModuleCommand))]
public class MyCommand : NotifyPropertyChanged, IModuleCommand
```

The menu is constructed from core menu items and items from MEF loaded modules.

The core items are implented in [MainMenuCommands.cs](./Application/MainMenuCommands.cs) and created by the `MainWindowViewModel` ([MainWindowViewModel.cs](./Application/MainWindowViewModel.cs)).

```c#
var coreMenu = new List<IMenuCommand> {new File(), new Help()};
MainMenuViewModel = new MainMenuViewModel(coreMenu, Modules);
```

Additionally, any modules that implement `IModuleContract` and have commands that implement `IModuleCommand` will be loaded by `MainWindowViewModel` ([MainWindowViewModel.cs](./Application/MainWindowViewModel.cs)) using MEF composition.

The `MenuGroupItem` attribute allows complex menus to be defined. The parameter is a string array, with each string represeting the syntax to define menu items. The components of the
array represent the menu path, so that a File/Exit menu item would be defined as:

```c#
[MenuGroupItem("File", "Exit")]
```

By default, the order of menus added to the parent is determined by the order menus are loaded by the MEF module loader - if Module2 is loaded before Module1 then the items would be added from Module2 first.

##### Menu Definition

In order to control the ordering of menu items, it is possible to define a precedence such that:

```c#
[MenuGroupItem("File", "Exit|999")]
public class ExitCommand : NotifyPropertyChanged, IModuleCommand
...

[MenuGroupItem("File", "Open|1")]
public class OpenCommand : NotifyPropertyChanged, IModuleCommand
...
```

would produce a menu:

1. File

  1. Open
  2. Exit

The depth of menus is not limited.

`MenuHelper` defines a parser for the text in each string item in the `MenuGroupItem` attribute.

Menu items can also be grouped where each group is delimited by a separator (horizontal line). See [MenuTests.cs](./Menu.Core.Test/MenuTests.cs) for examples.

##### Enabling and Disabling Menu Items

A menu item can be enabled/disabled by setting the `Active` parameter on the command class:

```c#
public Open()
{
    Active = true;
}

public bool Active
{
    get { return _active; }
    set
    {
        _active = value;
        OnPropertyChanged();
    }
}
```

Note, that if the `Active` value is updated (or any other property that may be required) then the `PropertyChanged` event fires.

#### Module Commands

The commands require an implementation of the `IModuleCommand` interface. This allows each module's command to call into the module code once the menu has been constructed.
Each menu item can be enabled or disabled, and the displayed menu name is defined (and is fully localised if a string resource is referenced appropriately).

## Controls

Visual Studio 2017 is a 32-bit application, or at least the WPF designer only supports 32-bit, so if 64-bit controls are used, they prevent the designer from working.

### 32-bit and 64-bit Controls

I present an option that has general controls built for "Any CPU" that can be wrapped in a container that can switch to a specific 64-bit control if that exists.

There are two `Renderer` classes in this architecture, one built for "Any CPU" and one built specifically for "x64". A `RendererContainer` in [RendererContainer.xaml.cs](./Framework/Presentation.Controls/RendererContainer.xaml.cs)
switches to the appropriate implementation at runtime. This concept of the container switching between different components could be extended to a specific 32-bit control, which is more
likely to reflect reality.

The constructor of `RendererContainer` loads the 32-bit (well "Any CPU" version) by default, unless a 64-bit environment is detected, when the 64-bit version is loaded through reflection
to avoid processor architecture conflicts on the project references.

```c#
public RendererContainer()
{
    InitializeComponent();

    if (Environment.Is64BitProcess)
    {
        const string controls64BitAssembly = "Hdd.Presentation.Controls.64bit.dll";
        var assembly = Assembly.LoadFrom(controls64BitAssembly);
        var type = assembly.GetType("Hdd.Presentation.Controls._64bit.Renderer");
        Renderer = (IRenderer) Activator.CreateInstance(type);
    }
    else
    {
        Renderer = new Renderer();
    }
    RendererContainerControl.Content = Renderer;
}
```

This allows the designer to keep working (since it is 32-bit it will request the "Any CPU" version and JIT that to 32-bit). If the application project settings have "Prefer 32-bit" set,
then the application will use the "Any CPU" version (what I would call the default option in the code above).

If the application runs on a 64-bit processor then the "Any CPU" application will need to prefer 64-bit over 32-bit for `RendererContainer` to correctly load the 64-bit control at runtime. In the
application's .csproj set:

```xml
<Prefer32Bit>false</Prefer32Bit>
```

### Dependency Properties

The `RendererContainer` must forward dependency properties to each of the `Renderer` classes, see the `Color` property on the container and each renderer.

In the WPF [MainWindow.xaml](./Application/Application/MainWindow.xaml) it is possible to include either the `Renderer` directly or via the `RendererContainer` with the dependency property
`Color` bound to the view model Color property.

```xml
<controls:Renderer Color="{Binding RendererViewModel.Color}" />
<controls:RendererContainer Color="{Binding RendererViewModel.Color}" />
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
