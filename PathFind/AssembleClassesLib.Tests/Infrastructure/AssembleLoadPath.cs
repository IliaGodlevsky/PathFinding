﻿using AssembleClassesLib.Attributes;
using AssembleClassesLib.Interface;
using System;
using System.IO;
using System.Reflection;

namespace AssembleClassesLib.Tests.Infrastructure
{
    [NotLoadable]
    internal sealed class AssempleLoadPath : IAssembleLoadPath
    {
        public string LoadPath
        {
            get
            {
                if (string.IsNullOrEmpty(loadPath))
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    string codeBase = assembly.CodeBase;
                    UriBuilder uri = new UriBuilder(codeBase);
                    string path = Uri.UnescapeDataString(uri.Path);
                    loadPath = Path.GetDirectoryName(path);
                }
                return loadPath;
            }
        }

        private string loadPath;
    }
}