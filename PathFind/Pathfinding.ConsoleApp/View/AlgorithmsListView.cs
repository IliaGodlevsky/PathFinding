using Autofac.Features.AttributeFilters;
using Pathfinding.ConsoleApp.Injection;
using ReactiveMarbles.ObservableEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Terminal.Gui;

namespace Pathfinding.ConsoleApp.View
{
    internal sealed partial class AlgorithmsListView : FrameView
    {
        public AlgorithmsListView([KeyFilter(KeyFilters.AlgorithmsListView)] IEnumerable<Terminal.Gui.View> children)
        {
            Initialize();
            var names = children.ToDictionary(x => x.Text, x => x);
            algorithms.RadioLabels = names.Keys.ToArray();
            algorithms.Events().SelectedItemChanged
                .Where(x => x.SelectedItem > -1)
                .Do(x =>
                {
                    var key = algorithms.RadioLabels[x.SelectedItem];
                    var element = names[key];
                    element.OnMouseEvent(new Terminal.Gui.MouseEvent() { Flags = MouseFlags.Button1Clicked });
                })
                .Subscribe();
            //algorithms.Add(children.ToArray());
        }
    }
}
