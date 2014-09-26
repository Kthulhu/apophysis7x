﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Xyrus.Apophysis.Strings {
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
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Xyrus.Apophysis.Strings.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Can&apos;t change thread count while renderer is busy.
        /// </summary>
        internal static string AttemptedThreadCountChangeWhileRendererBusyErrorMessage {
            get {
                return ResourceManager.GetString("AttemptedThreadCountChangeWhileRendererBusyErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The plugin file &quot;{0}&quot; caused errors when Apophysis attempted to load it. As long as this file exists, it will not be loaded again. Delete the file if you re-enable the plugin..
        /// </summary>
        internal static string BadPluginMessageFileContent {
            get {
                return ResourceManager.GetString("BadPluginMessageFileContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select the folder to save the result images in.
        /// </summary>
        internal static string BatchModeSelectPathHintText {
            get {
                return ResourceManager.GetString("BatchModeSelectPathHintText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure that you want to cancel the render?.
        /// </summary>
        internal static string CancelRenderConfirmMessage {
            get {
                return ResourceManager.GetString("CancelRenderConfirmMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Directory must exist.
        /// </summary>
        internal static string DirectoryDoesntExistError {
            get {
                return ResourceManager.GetString("DirectoryDoesntExistError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The directory must exist..
        /// </summary>
        internal static string DirectoryDoesntExistUiError {
            get {
                return ResourceManager.GetString("DirectoryDoesntExistUiError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you want to save the current batch before loading a new one?.
        /// </summary>
        internal static string DiscardBatchConfirmMessage {
            get {
                return ResourceManager.GetString("DiscardBatchConfirmMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Batch can&apos;t be empty.
        /// </summary>
        internal static string EmptyBatchError {
            get {
                return ResourceManager.GetString("EmptyBatchError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t remove last flame from batch.
        /// </summary>
        internal static string EmptyFlameCollectionError {
            get {
                return ResourceManager.GetString("EmptyFlameCollectionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source enumeration of palettes can&apos;t be empty.
        /// </summary>
        internal static string EmptyPaletteCollectionError {
            get {
                return ResourceManager.GetString("EmptyPaletteCollectionError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your current batch has unsaved changes. Do you want to save them before exiting Apophysis?.
        /// </summary>
        internal static string ExitConfirmMessage {
            get {
                return ResourceManager.GetString("ExitConfirmMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brightness must be greater than zero.
        /// </summary>
        internal static string FlameBrightnessRangeError {
            get {
                return ResourceManager.GetString("FlameBrightnessRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Depth of field must be greater than or equal to zero.
        /// </summary>
        internal static string FlameCameraDofRangeError {
            get {
                return ResourceManager.GetString("FlameCameraDofRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No flames in batch.
        /// </summary>
        internal static string FlameCollectionNoChildTagsError {
            get {
                return ResourceManager.GetString("FlameCollectionNoChildTagsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error: {0}.
        /// </summary>
        internal static string FlameErrorMessage {
            get {
                return ResourceManager.GetString("FlameErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Flame &quot;{0}&quot;: {1}.
        /// </summary>
        internal static string FlameErrorWrapper {
            get {
                return ResourceManager.GetString("FlameErrorWrapper", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gamma must be greater than or equal to one.
        /// </summary>
        internal static string FlameGammaRangeError {
            get {
                return ResourceManager.GetString("FlameGammaRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gamma threshold must be greater than or equal to zero.
        /// </summary>
        internal static string FlameGammaThresholdRangeError {
            get {
                return ResourceManager.GetString("FlameGammaThresholdRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No descendant node &quot;palette&quot; found.
        /// </summary>
        internal static string FlameMissingPaletteTagError {
            get {
                return ResourceManager.GetString("FlameMissingPaletteTagError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Flame &quot;{0}&quot;.
        /// </summary>
        internal static string FlameNameMessageHeader {
            get {
                return ResourceManager.GetString("FlameNameMessageHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Scale must be greater than zero.
        /// </summary>
        internal static string FlamePixelsPerUnitRangeError {
            get {
                return ResourceManager.GetString("FlamePixelsPerUnitRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Size must be greater than zero in both dimensions.
        /// </summary>
        internal static string FlameSizeRangeError {
            get {
                return ResourceManager.GetString("FlameSizeRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Done!.
        /// </summary>
        internal static string FlameSuccessMessage {
            get {
                return ResourceManager.GetString("FlameSuccessMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The flame was created with a different version of Apophysis or a different software and might not render correctly..
        /// </summary>
        internal static string FlameVersionMismatch {
            get {
                return ResourceManager.GetString("FlameVersionMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vibrancy must be greater than or equal to zero.
        /// </summary>
        internal static string FlameVibrancyRangeError {
            get {
                return ResourceManager.GetString("FlameVibrancyRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error loading library &quot;{0}&quot;: {1}.
        /// </summary>
        internal static string GenericPluginError {
            get {
                return ResourceManager.GetString("GenericPluginError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A full list of problems can be found in the message window (F8).
        /// </summary>
        internal static string GenericProblemListExceedsMaxSizeMessage {
            get {
                return ResourceManager.GetString("GenericProblemListExceedsMaxSizeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading GUI.
        /// </summary>
        internal static string InitializationLoadingGuiMessage {
            get {
                return ResourceManager.GetString("InitializationLoadingGuiMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading variations.
        /// </summary>
        internal static string InitializationLoadingVariationsMessage {
            get {
                return ResourceManager.GetString("InitializationLoadingVariationsMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Initializing.
        /// </summary>
        internal static string InitializationMessage {
            get {
                return ResourceManager.GetString("InitializationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A polygon must contain at least three vertices.
        /// </summary>
        internal static string InvalidPolygonError {
            get {
                return ResourceManager.GetString("InvalidPolygonError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No transforms in flame.
        /// </summary>
        internal static string IteratorCollectionNoChildTagsError {
            get {
                return ResourceManager.GetString("IteratorCollectionNoChildTagsError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t remove last primary iterator of flame.
        /// </summary>
        internal static string IteratorCollectionRemovingLastPrimaryIteratorError {
            get {
                return ResourceManager.GetString("IteratorCollectionRemovingLastPrimaryIteratorError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Color must be be in the range 0 - 1.
        /// </summary>
        internal static string IteratorColorRangeError {
            get {
                return ResourceManager.GetString("IteratorColorRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Color speed must be be in the range -1 - 1.
        /// </summary>
        internal static string IteratorColorSpeedRangeError {
            get {
                return ResourceManager.GetString("IteratorColorSpeedRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DirectColor must be be in the range 0 - 1.
        /// </summary>
        internal static string IteratorDirectColorRangeError {
            get {
                return ResourceManager.GetString("IteratorDirectColorRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Transform #{0}: {1}.
        /// </summary>
        internal static string IteratorErrorWrapper {
            get {
                return ResourceManager.GetString("IteratorErrorWrapper", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Opacity must be be in the range 0 - 1.
        /// </summary>
        internal static string IteratorOpacityRangeError {
            get {
                return ResourceManager.GetString("IteratorOpacityRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Potentially missing variation or variable: {0}.
        /// </summary>
        internal static string IteratorUnknownAttributeError {
            get {
                return ResourceManager.GetString("IteratorUnknownAttributeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Weight must not be less or equal to 0.
        /// </summary>
        internal static string IteratorWeightRangeError {
            get {
                return ResourceManager.GetString("IteratorWeightRangeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error loading &quot;{0}&quot;: {1}.
        /// </summary>
        internal static string LoadingPluginErrorLogMessage {
            get {
                return ResourceManager.GetString("LoadingPluginErrorLogMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mismatching palette arrays.
        /// </summary>
        internal static string MismatchingPaletteArraysError {
            get {
                return ResourceManager.GetString("MismatchingPaletteArraysError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The target file already exists. Do you want to overwrite it?.
        /// </summary>
        internal static string OverwriteRenderTargetConfirmMessage {
            get {
                return ResourceManager.GetString("OverwriteRenderTargetConfirmMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The plugin &quot;{0}&quot; seems to be loaded already..
        /// </summary>
        internal static string PluginAlreadyLoadedError {
            get {
                return ResourceManager.GetString("PluginAlreadyLoadedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Incompatible Apophysis plugin.
        /// </summary>
        internal static string PluginMethodNotFoundError {
            get {
                return ResourceManager.GetString("PluginMethodNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Module not found.
        /// </summary>
        internal static string PluginModuleNotFoundError {
            get {
                return ResourceManager.GetString("PluginModuleNotFoundError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid DLL (unknown machine type).
        /// </summary>
        internal static string PluginUnknownArchitectureError {
            get {
                return ResourceManager.GetString("PluginUnknownArchitectureError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid DLL (can&apos;t find PE header).
        /// </summary>
        internal static string PluginUnknownPeHeaderError {
            get {
                return ResourceManager.GetString("PluginUnknownPeHeaderError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unsupported plugin architecture ({0}).
        /// </summary>
        internal static string PluginUnsupportedArchitectureError {
            get {
                return ResourceManager.GetString("PluginUnsupportedArchitectureError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Allocating {0:###,###,###,##0.00} MB of memory....
        /// </summary>
        internal static string RenderAllocatingMessage {
            get {
                return ResourceManager.GetString("RenderAllocatingMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Average speed: {0:###,###,###,##0.00} iterations per second.
        /// </summary>
        internal static string RenderAverageSpeedMessage {
            get {
                return ResourceManager.GetString("RenderAverageSpeedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Density:    {0}.
        /// </summary>
        internal static string RenderDensityMessage {
            get {
                return ResourceManager.GetString("RenderDensityMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oversample: {0}, Filter: {1:0.0##}.
        /// </summary>
        internal static string RenderFilterMessage {
            get {
                return ResourceManager.GetString("RenderFilterMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rendering....
        /// </summary>
        internal static string RenderInProgressMessage {
            get {
                return ResourceManager.GetString("RenderInProgressMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rendering &quot;{0}&quot;.
        /// </summary>
        internal static string RenderMessageHeader {
            get {
                return ResourceManager.GetString("RenderMessageHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pure rendering time: {0}.
        /// </summary>
        internal static string RenderPureTimeMessage {
            get {
                return ResourceManager.GetString("RenderPureTimeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creating image with density: {0:###,###,###,##0.00}.
        /// </summary>
        internal static string RenderSamplingMessage {
            get {
                return ResourceManager.GetString("RenderSamplingMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Saving image....
        /// </summary>
        internal static string RenderSavingImageMessage {
            get {
                return ResourceManager.GetString("RenderSavingImageMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Size:       {0}x{1}.
        /// </summary>
        internal static string RenderSizeMessage {
            get {
                return ResourceManager.GetString("RenderSizeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rendering terminated!.
        /// </summary>
        internal static string RenderTerminatedMessage {
            get {
                return ResourceManager.GetString("RenderTerminatedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Terminating render....
        /// </summary>
        internal static string RenderTerminatingMessage {
            get {
                return ResourceManager.GetString("RenderTerminatingMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Total time: {0}.
        /// </summary>
        internal static string RenderTotalTimeMessage {
            get {
                return ResourceManager.GetString("RenderTotalTimeMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Some settings will be applied once Apophysis restarted..
        /// </summary>
        internal static string SettingsRequireRestartNotice {
            get {
                return ResourceManager.GetString("SettingsRequireRestartNotice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select target file....
        /// </summary>
        internal static string SingleModeSelectPathHintText {
            get {
                return ResourceManager.GetString("SingleModeSelectPathHintText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Palette size must be greater than one.
        /// </summary>
        internal static string TooSmallPaletteError {
            get {
                return ResourceManager.GetString("TooSmallPaletteError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected XML node &quot;{0}&quot; but received &quot;{1}&quot;.
        /// </summary>
        internal static string UnexpectedXmlTagError {
            get {
                return ResourceManager.GetString("UnexpectedXmlTagError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Apophysis encountered an unhandled exception. {0}
        ///
        ///Message: {1}
        ///Location: {2}.
        /// </summary>
        internal static string UnhandledExceptionMessage {
            get {
                return ResourceManager.GetString("UnhandledExceptionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sadly, the details could not be written to &quot;{0}&quot; because of the following error: {1}.
        /// </summary>
        internal static string UnhandledExceptionSaveFailureMessage {
            get {
                return ResourceManager.GetString("UnhandledExceptionSaveFailureMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The details have been written to &quot;{0}&quot;.
        /// </summary>
        internal static string UnhandledExceptionSaveMessage {
            get {
                return ResourceManager.GetString("UnhandledExceptionSaveMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The following plugins could not be loaded:
        ///
        ///{0}.
        /// </summary>
        internal static string VariationInitializationCollectiveError {
            get {
                return ResourceManager.GetString("VariationInitializationCollectiveError", resourceCulture);
            }
        }
    }
}
