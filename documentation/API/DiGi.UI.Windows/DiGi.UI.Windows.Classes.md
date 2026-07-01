#### [DiGi\.UI\.Windows](index.md 'index')

## DiGi\.UI\.Windows\.Classes Namespace
### Classes

<a name='DiGi.UI.Windows.Classes.TrayApplicationContext_TWindow_'></a>

## TrayApplicationContext\<TWindow\> Class

Provides a base application context for applications that reside in the system tray and manage a specific window type\.

```csharp
public abstract class TrayApplicationContext<TWindow> : System.Windows.Forms.ApplicationContext
    where TWindow : System.Windows.Window
```
#### Type parameters

<a name='DiGi.UI.Windows.Classes.TrayApplicationContext_TWindow_.TWindow'></a>

`TWindow`

The type of the Window managed by this context, which must inherit from [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')\.

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Forms\.ApplicationContext](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.applicationcontext 'System\.Windows\.Forms\.ApplicationContext') → TrayApplicationContext\<TWindow\>
### Constructors

<a name='DiGi.UI.Windows.Classes.TrayApplicationContext_TWindow_.TrayApplicationContext(string)'></a>

## TrayApplicationContext\(string\) Constructor

Initializes a new instance of the [TrayApplicationContext&lt;TWindow&gt;](DiGi.UI.Windows.Classes.md#DiGi.UI.Windows.Classes.TrayApplicationContext_TWindow_ 'DiGi\.UI\.Windows\.Classes\.TrayApplicationContext\<TWindow\>') class\.

```csharp
public TrayApplicationContext(string text);
```
#### Parameters

<a name='DiGi.UI.Windows.Classes.TrayApplicationContext_TWindow_.TrayApplicationContext(string).text'></a>

`text` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The text to display as a tooltip when hovering over the tray icon\.
### Fields

<a name='DiGi.UI.Windows.Classes.TrayApplicationContext_TWindow_.NotifyIcon'></a>

## TrayApplicationContext\<TWindow\>\.NotifyIcon Field

The system tray icon used for application notifications and interaction\.

```csharp
protected readonly NotifyIcon? NotifyIcon;
```

#### Field Value
[System\.Windows\.Forms\.NotifyIcon](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.notifyicon 'System\.Windows\.Forms\.NotifyIcon')
### Methods

<a name='DiGi.UI.Windows.Classes.TrayApplicationContext_TWindow_.GetWindow()'></a>

## TrayApplicationContext\<TWindow\>\.GetWindow\(\) Method

When overridden in a derived class, returns the window instance associated with this application context\.

```csharp
protected abstract TWindow GetWindow();
```

#### Returns
[TWindow](DiGi.UI.Windows.Classes.md#DiGi.UI.Windows.Classes.TrayApplicationContext_TWindow_.TWindow 'DiGi\.UI\.Windows\.Classes\.TrayApplicationContext\<TWindow\>\.TWindow')  
The [TWindow](DiGi.UI.Windows.Classes.md#DiGi.UI.Windows.Classes.TrayApplicationContext_TWindow_.TWindow 'DiGi\.UI\.Windows\.Classes\.TrayApplicationContext\<TWindow\>\.TWindow') instance\.