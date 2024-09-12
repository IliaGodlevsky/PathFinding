using System;
using System.Collections.Generic;
using System.Data;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class RunsTableView : TableView
    {
        private readonly DataTable table = new DataTable();

        private void Initialize()
        {
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(int)),
                new DataColumn("Algorithm", typeof(string)),
                new DataColumn("Visited", typeof(int)),
                new DataColumn("Steps", typeof(int)),
                new DataColumn("Cost", typeof(double)),
                new DataColumn("Elapsed", typeof(TimeSpan)),
                new DataColumn("Step", typeof(string)),
                new DataColumn("Logic", typeof(string)),
                new DataColumn("Spread", typeof(string)),
                new DataColumn("Status", typeof(string))
            });
            table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };
            var columnStyles = new Dictionary<DataColumn, ColumnStyle>()
            {
                { table.Columns["Id"], new ColumnStyle() { Visible = false } },
                { table.Columns["Algorithm"], new ColumnStyle() { Alignment = TextAlignment.Left } },
                { table.Columns["Visited"], new ColumnStyle() { Alignment = TextAlignment.Centered } },
                { table.Columns["Steps"], new ColumnStyle() { Alignment = TextAlignment.Centered } },
                { table.Columns["Cost"], new ColumnStyle() { Alignment = TextAlignment.Centered } },
                { table.Columns["Elapsed"], new ColumnStyle() { Format = @"ss\.fff", Alignment = TextAlignment.Centered } },
                { table.Columns["Step"], new ColumnStyle() { Alignment = TextAlignment.Centered } },
                { table.Columns["Logic"], new ColumnStyle() { Alignment = TextAlignment.Centered } },
                { table.Columns["Spread"], new ColumnStyle() { Alignment = TextAlignment.Centered } },
                { table.Columns["Status"], new ColumnStyle() { Alignment = TextAlignment.Centered } }
            };
            Style = new TableStyle()
            {
                ShowVerticalCellLines = false,
                AlwaysShowHeaders = true,
                SmoothHorizontalScrolling = true,
                ShowHorizontalHeaderOverline = true,
                ShowVerticalHeaderLines = false,
                ColumnStyles = columnStyles,
                ShowHorizontalScrollIndicators = true
            };
            MultiSelect = true;
            FullRowSelect = true;
            X = 0;
            Y = Pos.Percent(0);
            Width = Dim.Fill();
            Height = Dim.Percent(70);
            Table = table;
        }
    }
}
