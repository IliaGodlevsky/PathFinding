using System;
using System.Collections.Generic;
using System.Data;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class RunsTableView : TableView
    {
        private const string IdCol = "Id";
        private const string AlgorithmCol = "Algorithm";
        private const string VisitedCol = "Visited";
        private const string StepsCol = "Steps";
        private const string CostCol = "Cost";
        private const string ElapsedCol = "Elapsed";
        private const string StepCol = "Step";
        private const string LogicCol = "Logic";
        private const string WeightCol = "Weight";
        private const string StatusCol = "Status";
        private const string TimeFormat = @"ss\.fff";

        private readonly DataTable table = new();
        private readonly int headerLinesConsumed;

        public RunsTableView()
        {
            table.Columns.AddRange(new DataColumn[]
            {
                new (IdCol, typeof(int)),
                new (AlgorithmCol, typeof(string)),
                new (VisitedCol, typeof(int)),
                new (StepsCol, typeof(int)),
                new (CostCol, typeof(double)),
                new (ElapsedCol, typeof(TimeSpan)),
                new (StepCol, typeof(string)),
                new (LogicCol, typeof(string)),
                new (WeightCol, typeof(string)),
                new (StatusCol, typeof(string))
            });
            table.PrimaryKey = new[] { table.Columns[IdCol] };
            var columnStyles = new Dictionary<DataColumn, ColumnStyle>()
            {
                { table.Columns[IdCol], new() { Visible = false } },
                { table.Columns[AlgorithmCol], new() { MinWidth = 12, MaxWidth = 12, Alignment = TextAlignment.Left } },
                { table.Columns[VisitedCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[StepsCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[CostCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[ElapsedCol], new() { Format = TimeFormat, Alignment = TextAlignment.Centered } },
                { table.Columns[StepCol], new() { MinWidth = 9, MaxWidth = 9, Alignment = TextAlignment.Centered } },
                { table.Columns[LogicCol], new() { MinWidth = 9, MaxWidth = 9, Alignment = TextAlignment.Centered } },
                { table.Columns[WeightCol], new() { Alignment = TextAlignment.Left } },
                { table.Columns[StatusCol], new() { Alignment = TextAlignment.Centered } }
            };
            Style = new()
            {
                ExpandLastColumn = true,
                ShowVerticalCellLines = false,
                AlwaysShowHeaders = true,
                SmoothHorizontalScrolling = true,
                ShowHorizontalHeaderOverline = true,
                ShowVerticalHeaderLines = false,
                ColumnStyles = columnStyles,
                ShowHorizontalScrollIndicators = true
            };
            int line = 1;
            if (Style.ShowHorizontalHeaderOverline)
            {
                line++;
            }
            if (Style.ShowHorizontalHeaderUnderline)
            {
                line++;
            }
            headerLinesConsumed = line;
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
