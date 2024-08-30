using NStack;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.Service.Interface.Models.Read;
using System.Collections.Generic;
using System.Data;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainApp.Run(args);
            //Application.Init();
            //Colors.Base.Normal = Application.Driver.MakeAttribute(Color.White, Color.Black);
            //var mainWindow = new Window()
            //{
            //    X = 0,
            //    Y = 0,
            //    Height = Dim.Fill(),
            //    Width = Dim.Fill()
            //};
            //var statusBar = new StatusBar(new StatusItem[] {
            //    new StatusItem(Key.F1, "~F1~ Help", () => MessageBox.Query(50, 7, "Help", "Help not implemented", "Ok")),
            //    new StatusItem(Key.CtrlMask | Key.Q, "~^Q~ Quit", ()=>{})
            //});

            //var top = Application.Top;
           
            //top.Add(mainWindow, statusBar);

            //var rightPanel = new FrameView()
            //{
            //    X = Pos.Percent(75),
            //    Y = 0,
            //    Width = Dim.Percent(25),
            //    Height = Dim.Fill(),
            //    Border = new Border() { BorderStyle = BorderStyle.Rounded, DrawMarginFrame = false, Padding = new Thickness(0)}
            //};

            //var table = new DataTable();
            //var idColumn = new DataColumn("Id", typeof(int));
            //var nameColumn = new DataColumn("Name", typeof(string));
            //var widthColumn = new DataColumn("Width", typeof(int));
            //var heightColumn = new DataColumn("Length", typeof(int));
            //var obstaclesColumn = new DataColumn("Obstacles", typeof(int));
            //table.Columns.AddRange(new DataColumn[]
            //{
            //    idColumn, nameColumn, widthColumn, heightColumn, obstaclesColumn
            //});
            //for (int i = 0; i < 25; i++)
            //{
            //    table.Rows.Add(i+1, $"Algorithm{i + 1 }", 15 + i, 25 + i, i);
            //}
            //var frameView = new FrameView()
            //{
            //    X = 0,
            //    Y = Pos.Percent(0),
            //    Width = Dim.Fill(),
            //    Height = Dim.Percent(50),
            //    Border = new Border()
            //    {
            //        BorderStyle = BorderStyle.None,
            //        Padding = new Thickness(0),
            //        DrawMarginFrame = false
            //    }
            //};
            //var tableView = new TableView()
            //{
            //    MultiSelect = true,
            //    Style = new TableView.TableStyle() 
            //    {
            //        ShowVerticalCellLines = false,
            //        AlwaysShowHeaders = true, 
            //        SmoothHorizontalScrolling = true, 
            //        ShowHorizontalHeaderOverline = true,
            //        ShowVerticalHeaderLines = false,
            //        ColumnStyles = new Dictionary<DataColumn, TableView.ColumnStyle>()
            //        {
            //            { idColumn, new TableView.ColumnStyle() { Visible = false } },
            //            { nameColumn, new TableView.ColumnStyle() { MinWidth = 30 } },
            //            { widthColumn, new TableView.ColumnStyle() { Alignment = TextAlignment.Centered } },
            //            { heightColumn, new TableView.ColumnStyle() { Alignment = TextAlignment.Centered } },
            //            { obstaclesColumn, new TableView.ColumnStyle() { Alignment = TextAlignment.Centered } }
            //        }
            //    },
            //    FullRowSelect = true,
            //    X = 0,
            //    Y = Pos.Percent(0),
            //    Width = Dim.Fill(),
            //    Height = Dim.Percent(87),
            //    Table = table
            //};
            //tableView.SelectedCellChanged += e =>
            //{
            //    int id = (int)tableView.Table.Rows[e.NewRow]["Id"];
            //    string name = (string)tableView.Table.Rows[e.NewRow]["Name"];
            //    int width = (int)tableView.Table.Rows[e.NewRow]["Width"];
            //    int length = (int)tableView.Table.Rows[e.NewRow]["Length"];
            //    int obstacles = (int)tableView.Table.Rows[e.NewRow]["Obstacles"];
            //};
            //var buttonsFramwView = new FrameView()
            //{
            //    Border = new Border() 
            //    { 
            //        BorderStyle = BorderStyle.Rounded, 
            //        DrawMarginFrame = false, 
            //        Padding = new Thickness(0) 
            //    },
            //    X = 0,
            //    Y = Pos.Percent(87),
            //    Width = Dim.Fill(),
            //    Height = Dim.Percent(15)
            //};
            //var newButton = new Button("New")
            //{
            //    X = 0,
            //    Y = 0,
            //    Width = Dim.Percent(25)
            //};

            //var centered = new FrameView()
            //{
            //    X = Pos.Center(),
            //    Y = Pos.Center(),
            //    Width = Dim.Fill(),
            //    Height = Dim.Fill(),
            //    Visible = false,
            //    ClearOnVisibleFalse = true,
            //    Border = new Border()
            //    {
            //        BorderStyle = BorderStyle.None,
            //        BorderThickness = new Thickness(0)
            //    }
            //};

            //var nameFrame = new FrameView()
            //{
            //    X = 1,
            //    Y = 1,
            //    Height = Dim.Percent(15, true),
            //    Width = Dim.Fill(3),
            //    Border = new Border() 
            //    { 
            //        BorderStyle = BorderStyle.None, 
            //        Padding = new Thickness(0) 
            //    }
            //};

            //var graphNameLabel = new Label("Name")
            //{
            //    X = 1,
            //    Y = 1,
            //    Width = Dim.Percent(15),
            //};
            //var graphNameInput = new TextField()
            //{
            //    X = Pos.Percent(15),
            //    Y = 1,
            //    Width = Dim.Fill()
            //};
            //nameFrame.Add(graphNameLabel, graphNameInput);
            //var parametresFrame = new FrameView()
            //{
            //    X = 1,
            //    Y = Pos.Percent(15) + 1,
            //    Width = Dim.Percent(35),
            //    Height = Dim.Percent(40),
            //    Border = new Border()
            //    {
            //        BorderStyle = BorderStyle.Rounded,
            //        Padding = new Thickness(0),
            //        Title = "Parametres"
            //    }
            //};
            //var graphWidthLabel = new Label("Width")
            //{
            //    X = 1,
            //    Y = 1,
            //    Width = Dim.Percent(50, true)
            //};
            //var graphWidthInput = new TextField()
            //{
            //    X = Pos.Right(graphWidthLabel) + 1,
            //    Y = 1,
            //    Width = Dim.Fill(1),
            //};
            //var graphLengthLabel = new Label("Length")
            //{
            //    X = 1,
            //    Y = Pos.Bottom(graphWidthLabel) + 1,
            //    Width = Dim.Percent(50, true)
            //};
            //var graphLengthInput = new TextField()
            //{
            //    X = Pos.Right(graphLengthLabel) + 1,
            //    Y = Pos.Bottom(graphWidthInput) + 1,
            //    Width = Dim.Fill(1),
            //};
            //var obstaclesLabel = new Label("Obstacles")
            //{
            //    X = 1,
            //    Y = Pos.Bottom(graphLengthLabel) + 1,
            //    Width = Dim.Percent(50, true)
            //};
            //var obstaclesInput = new TextField()
            //{
            //    X = Pos.Right(obstaclesLabel) + 1,
            //    Y = Pos.Bottom(graphLengthInput) + 1,
            //    Width = Dim.Fill(1),
            //};
            //var neighborFrame = new FrameView()
            //{
            //    X = Pos.Percent(35) + 1,
            //    Y = Pos.Percent(15) + 1,
            //    Width = Dim.Percent(35),
            //    Height = Dim.Percent(40),
            //    Border = new Border()
            //    {
            //        BorderStyle = BorderStyle.Rounded,
            //        Padding = new Thickness(0),
            //        Title = "Neighbors"
            //    }
            //};
            //var neighborhoods = new RadioGroup(new ustring[] {"Moore", "Von Neimann"})
            //{
            //    X = 1,
            //    Y = 1,
            //};
            //neighborFrame.Add(neighborhoods);
            //var smoothFrame = new FrameView()
            //{
            //    X = Pos.Percent(70) + 1,
            //    Y = Pos.Percent(15) + 1,
            //    Width = Dim.Fill(1),
            //    Height = Dim.Percent(40),
            //    Border = new Border()
            //    {
            //        BorderStyle = BorderStyle.Rounded,
            //        Padding = new Thickness(0),
            //        Title = "Smooth"
            //    }
            //};
            //var smoothLevels = new RadioGroup(new ustring[] { "No", "Low", "Medium", "High" })
            //{
            //    X = 1,
            //    Y = 1
            //};
            //smoothFrame.Add(smoothLevels);
            //parametresFrame.Add(graphWidthLabel, graphWidthInput,
            //    graphLengthLabel, graphLengthInput,
            //    obstaclesInput, obstaclesLabel);
            //var creationErrorsFrame = new FrameView()
            //{
            //    X = 1,
            //    Y = Pos.Percent(55) + 1,
            //    Width = Dim.Fill(1),
            //    Height = Dim.Percent(25),
            //    Border = new Border()
            //    {
            //        BorderStyle = BorderStyle.Rounded,
            //        BorderBrush = Color.BrightRed,
            //        Padding = new Thickness(0),
            //        Title = "Errors"
            //    }
            //};

            //var createButton = new Button("Create")
            //{
            //    X = Pos.Percent(27),
            //    Y = Pos.Percent(85) + 1,
            //};
            //var cancelButton = new Button("Cancel")
            //{
            //    X = Pos.Right(createButton) + 2,
            //    Y = Pos.Percent(85) + 1,
            //};
            //centered.Add(nameFrame, parametresFrame, neighborFrame, 
            //    smoothFrame, creationErrorsFrame,
            //    createButton, cancelButton);
            
            //newButton.MouseClick += e =>
            //{
            //    if (!centered.Visible)
            //    {
            //        centered.Visible = true;
            //    }
            //};
            //cancelButton.MouseClick += e =>
            //{
            //    centered.Visible = false;
            //};
            //var saveButton = new Button("Save")
            //{
            //    X = Pos.Percent(25),
            //    Y = 0,
            //    Width = Dim.Percent(25)
            //};
            //var loadButton = new Button("Load")
            //{
            //    X = Pos.Percent(50),
            //    Y = 0,
            //    Width = Dim.Percent(25)
            //};
            //var deleteButton = new Button("Delete")
            //{
            //    X = Pos.Percent(75),
            //    Y = 0,
            //    Width = Dim.Percent(25)
            //};
            //buttonsFramwView.Add(newButton, saveButton, loadButton, deleteButton);
            //frameView.Add(tableView, buttonsFramwView, centered);

            //var graphFieldFrame = new FrameView("Graph field")
            //{
            //    X= 0,
            //    Y = 0,
            //    Width = Dim.Percent(75),
            //    Height = Dim.Fill()
            //};
            //int labelWidth = 2;
            //for (int i = 0; i < 25; i++)
            //{
            //    for (int j = 0; j < 35; j++)
            //    {
            //        var label = new Label()
            //        {
            //            X = j * (labelWidth),
            //            Y = i,
            //            Width = labelWidth,
            //            Text = "3"
            //        };
            //        graphFieldFrame.Add(label);
            //    }
            //}

            //rightPanel.Add(frameView);
            //mainWindow.Add(rightPanel, graphFieldFrame);

            //Application.Run();
        }
    }
}
