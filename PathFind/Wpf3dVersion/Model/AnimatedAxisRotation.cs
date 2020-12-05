﻿using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Wpf3dVersion.Enums;

namespace Wpf3dVersion.Model
{
    internal class AnimatedAxisRotation
    {
        public static double StartAngle => 0;
        public static double EndAngle => 360;

        public AnimatedAxisRotation(AxisAngleRotation3D axis, RotateDirection direction)
        {
            this.axis = axis;
            this.direction = direction;
        }

        public void RotateAxisAnimated()
        {
            DoubleAnimation animation;

            if (direction == RotateDirection.Forward)
                animation = CreateAnimation(axis.Angle, EndAngle, FillBehavior.HoldEnd);
            else
                animation = CreateAnimation(axis.Angle, StartAngle, FillBehavior.Stop);

            axis.BeginAnimation(AxisAngleRotation3D.AngleProperty, animation);
        }

        private DoubleAnimation CreateAnimation(double from,
            double to, FillBehavior fillBehavior)
        {
            return new DoubleAnimation
            {
                To = to,
                From = from,
                FillBehavior = fillBehavior,
                Duration = GetAnimationDuration()
            };
        }

        private Duration GetAnimationDuration()
        {
            var duration = InitialDuration;

            if (direction == RotateDirection.Forward)
                duration *= (AngleAmplitude - axis.Angle) / AngleAmplitude;
            else
                duration *= axis.Angle / AngleAmplitude;

            return new Duration(TimeSpan.FromMilliseconds(duration));
        }

        private double AngleAmplitude => EndAngle - StartAngle;

        private double InitialDuration => 3000;

        private readonly AxisAngleRotation3D axis;
        private readonly RotateDirection direction;
    }
}