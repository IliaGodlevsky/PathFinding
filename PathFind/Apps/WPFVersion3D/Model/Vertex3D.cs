﻿using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Serialization;
using GraphLib.Serialization.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using WPFVersion3D.Interface;

namespace WPFVersion3D.Model
{
    [DebuggerDisplay("{Position.ToString()}")]
    internal class Vertex3D : UIElement3D, IVertex, IVisualizable
    {
        public static readonly DependencyProperty SizeProperty;
        public static readonly DependencyProperty BrushProperty;

        private readonly IModel3DFactory modelFactory;
        private readonly IVisualization<Vertex3D> visualization;
        private readonly Lazy<IReadOnlyCollection<IVertex>> neighbours;
        private bool isObstacle;

        public bool IsObstacle
        {
            get => isObstacle;
            set
            {
                isObstacle = value;
                if (isObstacle)
                {
                    VisualizeAsObstacle();
                }
            }
        }

        public TranslateTransform3D FieldPosition => (TranslateTransform3D)Transform;

        public IGraph Graph { get; }

        public IVertexCost Cost { get; set; }

        public IReadOnlyCollection<IVertex> Neighbours => neighbours.Value;

        public ICoordinate Position { get; }

        public bool IsVisualizedAsPath => visualization.IsVisualizedAsPath(this);

        public bool IsVisualizedAsEndPoint => visualization.IsVisualizedAsEndPoint(this);

        public DiffuseMaterial Material { get; set; }

        public Brush Brush
        {
            get => (Brush)GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }

        private double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        public Vertex3D(INeighborhood neighborhood, ICoordinate coordinate, IModel3DFactory modelFactory,
            IVisualization<Vertex3D> visualization)
        {
            this.visualization = visualization;
            this.modelFactory = modelFactory;
            Position = coordinate;
            Material = new DiffuseMaterial();
            Transform = new TranslateTransform3D();
            Size = Constants.InitialVertexSize;
            this.Initialize();
            neighbours = new Lazy<IReadOnlyCollection<IVertex>>(() => neighborhood.GetNeighboursWithinGraph(this));
        }

        public Vertex3D(VertexSerializationInfo info, IModel3DFactory modelFactory, IVisualization<Vertex3D> visualization)
            : this(info.Neighbourhood, info.Position, modelFactory, visualization)
        {
            this.Initialize(info);
        }

        static Vertex3D()
        {
            SizeProperty = RegisterProperty(nameof(Size), typeof(double), SizePropertyChanged);
            BrushProperty = RegisterProperty(nameof(Brush), typeof(Brush), BrushPropertyChanged);
        }

        public bool Equals(IVertex other)
        {
            return other.IsEqual(this);
        }

        public void VisualizeAsTarget()
        {
            visualization.VisualizeAsTarget(this);
        }

        public void VisualizeAsObstacle()
        {
            visualization.VisualizeAsObstacle(this);
        }

        public void VisualizeAsPath()
        {
            visualization.VisualizeAsPath(this);
        }

        public void VisualizeAsRegular()
        {
            visualization.VisualizeAsRegular(this);
        }

        public void VisualizeAsVisited()
        {
            visualization.VisualizeAsVisited(this);
        }

        public void VisualizeAsEnqueued()
        {
            visualization.VisualizeAsEnqueued(this);
        }

        public void VisualizeAsSource()
        {
            visualization.VisualizeAsSource(this);
        }

        public void VisualizeAsIntermediate()
        {
            visualization.VisualizeAsIntermediate(this);
        }

        public void VisualizeAsMarkedToReplaceIntermediate()
        {
            visualization.VisualizeAsMarkedToReplaceIntermediate(this);
        }

        protected static void SizePropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            vert.Size = (double)prop.NewValue;
            vert.Visual3DModel = vert.modelFactory.CreateModel3D(vert.Size, vert.Material);
        }

        protected static void BrushPropertyChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs prop)
        {
            Vertex3D vert = (Vertex3D)depObj;
            vert.Brush = (Brush)prop.NewValue;
            vert.Material.Brush = vert.Brush;
        }

        private static DependencyProperty RegisterProperty(string propertyName, Type propertyType, PropertyChangedCallback callback)
        {
            return DependencyProperty.Register(propertyName, propertyType, typeof(Vertex3D), new PropertyMetadata(callback));
        }
    }
}