﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Xyrus.Tools.TemplateExpander {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Xyrus.Tools.TemplateExpander.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ERROR: {0}.
        /// </summary>
        internal static string ErrorFormatString {
            get {
                return ResourceManager.GetString("ErrorFormatString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed..
        /// </summary>
        internal static string FailedString {
            get {
                return ResourceManager.GetString("FailedString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Enter the new project name: .
        /// </summary>
        internal static string PromptString {
            get {
                return ResourceManager.GetString("PromptString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reading &quot;{0}&quot;....
        /// </summary>
        internal static string ReadingString {
            get {
                return ResourceManager.GetString("ReadingString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Successful..
        /// </summary>
        internal static string SuccessfulString {
            get {
                return ResourceManager.GetString("SuccessfulString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Template directory &quot;{0}&quot;does not exist..
        /// </summary>
        internal static string TemplateDirError {
            get {
                return ResourceManager.GetString("TemplateDirError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Writing &quot;{0}&quot;....
        /// </summary>
        internal static string WritingString {
            get {
                return ResourceManager.GetString("WritingString", resourceCulture);
            }
        }
    }
}
