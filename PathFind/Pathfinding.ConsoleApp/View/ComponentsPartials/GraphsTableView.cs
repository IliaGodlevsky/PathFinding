using System.Collections.Generic;
using System.Data;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class GraphsTableView : TableView
    {
        private readonly DataTable table = new DataTable();

        private void Initialize()
        {
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(int)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Width", typeof(int)),
                new DataColumn("Length", typeof(int)),
                new DataColumn("Obstacles", typeof(int))
            });
            table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };
            var columnStyles = new Dictionary<DataColumn, ColumnStyle>()
            {
                { table.Columns["Id"], new ColumnStyle() { Visible = false } },
                { table.Columns["Name"], new ColumnStyle() { MinWidth = 28 } },
                { table.Columns["Width"], new ColumnStyle() { Alignment = TextAlignment.Centered } },
                { table.Columns["Length"], new ColumnStyle() { Alignment = TextAlignment.Centered } },
                { table.Columns["Obstacles"], new ColumnStyle() { Alignment = TextAlignment.Centered } }
            };
            Style = new TableStyle()
            {
                ShowVerticalCellLines = false,
                AlwaysShowHeaders = true,
                SmoothHorizontalScrolling = true,
                ShowHorizontalHeaderOverline = true,
                ShowVerticalHeaderLines = false,
                ColumnStyles = columnStyles
            };
            FullRowSelect = true;
            X = 0;
            Y = Pos.Percent(0);
            Width = Dim.Fill();
            Height = Dim.Percent(87);
            Table = table;
        }
    }
}
