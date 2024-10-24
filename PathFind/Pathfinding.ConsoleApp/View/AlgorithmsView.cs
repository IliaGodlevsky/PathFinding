﻿using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using Pathfinding.Shared.Extensions;
using ReactiveMarbles.ObservableEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmsView : FrameView
    {
        public AlgorithmsView([KeyFilter(KeyFilters.AlgorithmsListView)] IEnumerable<Terminal.Gui.View> children)
        {
            Initialize();
            var names = children.OrderByOrderAttribute()
                .ToDictionary(x => x.Text, x => x);
            algorithms.RadioLabels = names.Keys.ToArray();
            algorithms.Events().SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Do(x =>
                {
                    var key = algorithms.RadioLabels[x.SelectedItem];
                    var element = names[key];
                    var @event = new MouseEvent() { Flags = MouseFlags.Button1Clicked };
                    element.OnMouseEvent(@event);
                })
                .Subscribe();
        }
    }
}
