﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pathfinding.ConsoleApp {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.12.0.0")]
    internal sealed partial class AppSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static AppSettings defaultInstance = ((AppSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new AppSettings())));
        
        public static AppSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=pathfinding.sqlite;")]
        public string ConnectionString {
            get {
                return ((string)(this["ConnectionString"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Black")]
        public global::Terminal.Gui.Color BackgroundColor {
            get {
                return ((global::Terminal.Gui.Color)(this["BackgroundColor"]));
            }
            set {
                this["BackgroundColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Gray")]
        public global::Terminal.Gui.Color ForegroundColor {
            get {
                return ((global::Terminal.Gui.Color)(this["ForegroundColor"]));
            }
            set {
                this["ForegroundColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DarkGray")]
        public global::Terminal.Gui.Color RegularVertexColor {
            get {
                return ((global::Terminal.Gui.Color)(this["RegularVertexColor"]));
            }
            set {
                this["RegularVertexColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("BrightMagenta")]
        public global::Terminal.Gui.Color SourceVertexColor {
            get {
                return ((global::Terminal.Gui.Color)(this["SourceVertexColor"]));
            }
            set {
                this["SourceVertexColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("BrightRed")]
        public global::Terminal.Gui.Color TargetVertexColor {
            get {
                return ((global::Terminal.Gui.Color)(this["TargetVertexColor"]));
            }
            set {
                this["TargetVertexColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Gray")]
        public global::Terminal.Gui.Color VisitedVertexColor {
            get {
                return ((global::Terminal.Gui.Color)(this["VisitedVertexColor"]));
            }
            set {
                this["VisitedVertexColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("White")]
        public global::Terminal.Gui.Color EnqueuedVertexColor {
            get {
                return ((global::Terminal.Gui.Color)(this["EnqueuedVertexColor"]));
            }
            set {
                this["EnqueuedVertexColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("BrightGreen")]
        public global::Terminal.Gui.Color TranstiVertexColor {
            get {
                return ((global::Terminal.Gui.Color)(this["TranstiVertexColor"]));
            }
            set {
                this["TranstiVertexColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Brown")]
        public global::Terminal.Gui.Color PathVertexColor {
            get {
                return ((global::Terminal.Gui.Color)(this["PathVertexColor"]));
            }
            set {
                this["PathVertexColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Red")]
        public global::Terminal.Gui.Color CrossedPathColor {
            get {
                return ((global::Terminal.Gui.Color)(this["CrossedPathColor"]));
            }
            set {
                this["CrossedPathColor"] = value;
            }
        }
    }
}
