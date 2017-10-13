Test

# Wpf Resource Issue

A sample project to demonstrate an issue with WPF resources. The issue has been reported on [Microsoft Connect](https://connect.microsoft.com/VisualStudio/feedback/details/2993889/wpf-fails-to-load-resources-if-two-versions-of-the-same-assembly-are-loded)

This is a sample of a very simple modular application. 
It's only used to demonstrate an issue with WPF resources, so the code is kept very simple and does not follow any architectural patterns. 

The application simply collects visuals from modules dynamically, and displays them in the main window.

There are two sample modules, and each has just one simple visual. 
Both utilize resources from a common `StyleLibrary`, however module1 is referencing V1.0.0 of the `StyleLibrary`, while module2 is referencing V1.0.1 of the `StyleLibrary`.

To have transparent access to the resources without having to mangle with resource dictionaries, theme resources are used. 
Resources that use a type as the key, as it's the default for controls, can be loaded from the correct assembly, 
however when using a `ComponentResourceKey`, loading the resources fails if both versions of the style library are loaded into memory.

When both modules are loaded, module1 correctly loads `Control1` and it's style, as well as the color referenced by a type as it's key ("Background with type key").
However it fails to load the `CorporateColor` or `CorporateStyle` resources, and the primary text is formatted wrong. 
If module2 is removed, module1 works fine. If module1 upgrades to the same version of the `StyleLibrary`, both work fine.

Even worse if resources from the `StyleLibrary` are referenced by `StaticResource` instead of `DynamicResource`, the application crashes!

This scenario happens if customer buys application and module1, and later also buys module2. 
After installing module2, module1 stops working properly.

A real live example is Visual Studio, with two extensions installed, that both reference the same library.

### Module dependencies:

![sample](https://github.com/tom-englert/WpfResourceIssue/blob/master/sample.png)

### Loading only module 1:

![Module1Only](https://github.com/tom-englert/WpfResourceIssue/blob/master/Module1Only.png)

### Loading both modules:
Control1 (the background) can still find it's style, as well as the background of the lower right text block, that uses a type as it's key.
However the text block "Module 1" has lost it's style.

![Module1Module2](https://github.com/tom-englert/WpfResourceIssue/blob/master/Module1Module2.png)

###  Modules in reverse order:
Now the text block "Module 2" has lost it's style.

![Module2Module1](https://github.com/tom-englert/WpfResourceIssue/blob/master/Module2Module1.png)

