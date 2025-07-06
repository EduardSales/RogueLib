# RogueLib
RogueLib is a lightweight library designed for small teams developing roguelikes games. It collects and displays information about each run, helping developers analyze gameplay data and improve the player experience.

How to use
---
For a more in-depth guide check [User manual](https://github.com/EduardSales/RogueLib/wiki/User-Manual).

RogueLib is designed to be simple to use, and is divided into two main parts: **data collection** and **data visualization**.
Let's start with data collection.
The data will be store in two parts, first everything that happens in a run will be stored in a class called *Run*, and the whole data of the session, like all the runs, or methods needed to store and show the information, will be stored in a class called **Vault**.

To begin to store data first we need to create a run, to achieve that we can do this

```c#
using RogueLib;

private void NewRun()
{
  //in this function you would implement all the code necesary to start a run in your game,
  //and in the end you'll add the function to store that in the library
  Vault.Instance.AddRun();

  //in case your game has a diferent characters to choose from, you can initialize the run slightly diferent
  //Vault.Instance.AddRun(character);
}
```

Now that a run has started we can add all the data we see necesary, let's see an example with items, a common thing to have in roguelikes.

```c#
using Roguelib;

private OnObjectPicked(Object _object)
{
  //your code...
  //this function is a bit diferent because we now need to acces the current run to add the item

  Vault.Instance.GetCurrentRun().AddItem(new Item(_object.name));
}
```

Once the session ends, and you have recolected all the information, now it's time to show that info.

You have 3 diferent options to get the report: **PDF**, **TXT**, **CSV**
Let's see them one by one

#### PDF
The first option, export the report in PDF

```c#
using RogueLib;

private void ExportPDF(string _path)
{
  //You'll have to put the path where you want the pdf to be sored localy on your PC
  //And specify the name of the file
  Vault.Instance.GeneratePDFReport(_path + "/runs.pdf");
}
```
#### TXT
In case you need to see the report, but maybe you want to add notes, or anything there's a way to export a TXT
```c#
using RogueLib;

private void ExportTXT(string _path)
{
  //You'll have to put the path where you want the pdf to be sored localy on your PC
  //And specify the name of the file
  Vault.Instance.GenerateTXTReport(_path + "/runs.txt");
}
```
#### CSV
Finally if you need to store all the data in excel, or a BBDD you might need a CSV, this function varies slightly
```c#
using RogueLib;

private void ExportCSV(string _path)
{
  //You'll have to put the path where you want the pdf to be sored localy on your PC
  //In this one you dont specify the name, because it generates diferent csv
  //Doing this you can choose what you wanna save and how
  Vault.Instance.GenerateCSVReport(_path);
}
```

Build and Installation
---
RogueLib releases are available at the [release page](https://github.com/EduardSales/RogueLib/releases).

To use the library first download from the release page the asset called RogueLib, inside you'll find 2 folders,  **Dependencies** and **library** inside there are the dll necesary.
To have the library set, open Unity and inside the Plugins folder, add the content of the 2 folders from before.


