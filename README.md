# Wpf Resource Issue
A sample project to demonstrate an issue with WPF resources

This is a sample of a very simple modular application. 
It's only used to demonstrate an issue with WPF resources, so the code is kept very simple and does not follow any architectural patterns. 

The application simply collects visuals from modules dynamically, and displays them in the main window.

There are two sample modules, and each has just one simple visual. 
Both utilize resources from a common `StyleLibrary`, however module1 is referencing V1.0.0 of the `StyleLibrary`, while module2 is referencing V1.0.1 of the `StyleLibrary`.

When both modules are loaded, module1 fails to load the `CorporateColor` resource, and the primary text is black. 
If module2 is removed, module1 works fine. If module1 upgrades to the same version of the `StyleLibrary`, both work fine.

This scenario happens if customer buys application and module1, and later also buys module2. 
After installing module2, module1 stops working properly.
