﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34014
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alternative.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>num;System.Int64;unique</string>
  <string>pin;System.String;</string>
  <string>nom;System.Int32;</string>
  <string>date_s;System.DateTime;</string>
  <string>date_e;System.DateTime;</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection CardColumns {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["CardColumns"]));
            }
            set {
                this["CardColumns"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1123/omGRVKvV5iQb7nkh2CNpgoAmMQWTzgdoWt4xHerx0axUCg2g72uDTTE2GuLi5qr9UkRdOCGTWWo9" +
            "TsNeYPR/laWksjeSkgA3SlDylkKQnU=")]
        public string key {
            get {
                return ((string)(this["key"]));
            }
            set {
                this["key"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int edt {
            get {
                return ((int)(this["edt"]));
            }
            set {
                this["edt"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("3")]
        public int CountRepeatGetState {
            get {
                return ((int)(this["CountRepeatGetState"]));
            }
            set {
                this["CountRepeatGetState"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2000")]
        public int IntervalGetState {
            get {
                return ((int)(this["IntervalGetState"]));
            }
            set {
                this["IntervalGetState"] = value;
            }
        }
    }
}
