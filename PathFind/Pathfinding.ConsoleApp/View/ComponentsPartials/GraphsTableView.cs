using System.Collections.Generic;
using System.Data;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphsTableView : TableView
    {
        private const string IdCol = "Id";
        private const string NameCol = "Name";
        private const string WidthCol = "Width";
        private const string LengthCol = "Length";
        private const string NeighborsCol = "Neighbors";
        private const string SmoothCol = "Smooth";
        private const string ObstaclesCol = "Obstacles";
        private const string Status = "Status";

        private readonly DataTable table = new DataTable();

        private void Initialize()
        {
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(IdCol, typeof(int)),
                new DataColumn(NameCol, typeof(string)),
                new DataColumn(WidthCol, typeof(int)),
                new DataColumn(LengthCol, typeof(int)),
                new DataColumn(NeighborsCol, typeof(string)),
                new DataColumn(SmoothCol, typeof(string)),
                new DataColumn(ObstaclesCol, typeof(int)),
                new DataColumn(Status, typeof(string)),
            });
            table.PrimaryKey = new DataColumn[] { table.Columns[IdCol] };
            var columnStyles = new Dictionary<DataColumn, ColumnStyle>()
            {
                { table.Columns[IdCol], new() { Visible = false } },
                { table.Columns[NameCol], new() { MinWidth = 24, MaxWidth = 24, Alignment = TextAlignment.Left } },
                { table.Columns[WidthCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[LengthCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[NeighborsCol], new() { Alignment = TextAlignment.Left } },
                { table.Columns[SmoothCol], new() { Alignment = TextAlignment.Left } },
                { table.Columns[ObstaclesCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[Status], new () { Alignment = TextAlignment.Centered } },
            };
            Style = new TableStyle()
            {
                ExpandLastColumn = false,
                ShowVerticalCellLines = false,
                AlwaysShowHeaders = true,
                ShowVerticalHeaderLines = false,
                ColumnStyles = columnStyles
            };
            MultiSelect = true;
            FullRowSelect = true;
            X = 0;
            Y = Pos.Percent(0);
            Width = Dim.Fill();
            Height = Dim.Percent(90);
            Table = table;
        }
    }
}
