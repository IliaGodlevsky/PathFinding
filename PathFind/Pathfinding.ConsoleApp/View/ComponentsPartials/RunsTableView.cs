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

        private readonly DataTable table = new DataTable();

        private void Initialize()
        {
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn(IdCol, typeof(int)),
                new DataColumn(AlgorithmCol, typeof(string)),
                new DataColumn(VisitedCol, typeof(int)),
                new DataColumn(StepsCol, typeof(int)),
                new DataColumn(CostCol, typeof(double)),
                new DataColumn(ElapsedCol, typeof(TimeSpan)),
                new DataColumn(StepCol, typeof(string)),
                new DataColumn(LogicCol, typeof(string)),
                new DataColumn(WeightCol, typeof(string)),
                new DataColumn(StatusCol, typeof(string))
            });
            table.PrimaryKey = new DataColumn[] { table.Columns[IdCol] };
            var columnStyles = new Dictionary<DataColumn, ColumnStyle>()
            {
                { table.Columns[IdCol], new() { Visible = false } },
                { table.Columns[AlgorithmCol], new() { Alignment = TextAlignment.Left } },
                { table.Columns[VisitedCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[StepsCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[CostCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[ElapsedCol], new() { Format = TimeFormat, Alignment = TextAlignment.Centered } },
                { table.Columns[StepCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[LogicCol], new() { Alignment = TextAlignment.Centered } },
                { table.Columns[WeightCol], new() { Alignment = TextAlignment.Left } },
                { table.Columns[StatusCol], new() { Alignment = TextAlignment.Centered } }
            };
            Style = new TableStyle()
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
