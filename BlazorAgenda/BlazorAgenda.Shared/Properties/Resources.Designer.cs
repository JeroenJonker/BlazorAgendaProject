﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlazorAgenda.Shared.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BlazorAgenda.Shared.Properties.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to api/.
        /// </summary>
        public static string ControllerApi {
            get {
                return ResourceManager.GetString("ControllerApi", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to api/Event/GetUserEvents/.
        /// </summary>
        public static string EventApi_GetUserEvents {
            get {
                return ResourceManager.GetString("EventApi_GetUserEvents", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /Add.
        /// </summary>
        public static string ObjectApi_Add {
            get {
                return ResourceManager.GetString("ObjectApi_Add", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /Delete.
        /// </summary>
        public static string ObjectApi_Delete {
            get {
                return ResourceManager.GetString("ObjectApi_Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /Edit.
        /// </summary>
        public static string ObjectApi_Edit {
            get {
                return ResourceManager.GetString("ObjectApi_Edit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to api/User/GetAllUsers.
        /// </summary>
        public static string UserApi_GetAllUsers {
            get {
                return ResourceManager.GetString("UserApi_GetAllUsers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to api/User/IsUserInUse.
        /// </summary>
        public static string UserApi_IsUserInUse {
            get {
                return ResourceManager.GetString("UserApi_IsUserInUse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to api/User/IsValidUser.
        /// </summary>
        public static string UserApi_IsValidUser {
            get {
                return ResourceManager.GetString("UserApi_IsValidUser", resourceCulture);
            }
        }
    }
}