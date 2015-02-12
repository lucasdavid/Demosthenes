# Demosthenes

## Introduction

Demosthenes is a web application written in .net c# created to support academies, universities and teaching institutions. It contains essencial management resources such as faculty, student, courses, classes, schedules and so on.

## Contributing

There is still lots to be done. Please, fell free to go over the list of opened issues at the [issues page](https://github.com/lucasdavid/Demosthenes/issues), fork the project and improve something!

## Compiling and Running

* Open the project on Visual Studio.
* **Clean the solution**, going in the Solution Explorer, at the right upper corner, right click on the solution and select `Clean Solution`.
Alternatively, you can simply press: `Ctrl+Alt+L` > `Home` > `Context menu` > `C`.
* **Update all dependencies**, in the Solution Explorer, click with the right button on the solution and `Manage NuGet Packages for Solution...`,
select `All` under the `Update` tab and `Update All`, in the center of the screen.
* **Build the project** with `Ctrl+Shift+B`. You shouldn't be getting any errors by now.
* **Update the database**, going to the `Package Manage Console` (`Ctrl+Q` and type `Package Manage Console`) and type `Update-Database -ProjectName Demosthenes.Data -Force`.
* Run the project with `Ctrl+F5`.
