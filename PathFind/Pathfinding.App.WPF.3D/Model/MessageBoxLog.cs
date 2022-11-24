﻿using Pathfinding.Logging.Interface;
using System;
using System.Windows;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class MessageBoxLog : ILog
    {
        public void Debug(string message)
        {

        }

        public void Error(Exception ex, string message = null)
        {
            MessageBox.Show(ex.Message + "\n" + message);
        }

        public void Error(string message)
        {
            MessageBox.Show(message);
        }

        public void Fatal(Exception ex, string message = null)
        {
            MessageBox.Show(ex.Message + "\n" + message);
        }

        public void Fatal(string message)
        {
            MessageBox.Show(message);
        }

        public void Info(string message)
        {

        }

        public void Trace(string message)
        {

        }

        public void Warn(Exception ex, string message = null)
        {

        }

        public void Warn(string message)
        {

        }
    }
}
