﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace moleQule.Library.Hipatia.Resources {
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
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("moleQule.Library.Hipatia.Resources.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to El agente no ha sido dado de alta en el Gestor Documental.
        /// </summary>
        public static string AGENTE_NOT_FOUND {
            get {
                return ResourceManager.GetString("AGENTE_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Para insertar o actualizar objetos hijo debe utilizar las funciones Insert o Update..
        /// </summary>
        public static string CHILD_SAVE_NOT_ALLOWED {
            get {
                return ResourceManager.GetString("CHILD_SAVE_NOT_ALLOWED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El documento ya se encuentra asociado al agente..
        /// </summary>
        public static string DOCUMENT_REPEATED {
            get {
                return ResourceManager.GetString("DOCUMENT_REPEATED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se ha encontrado el tipo de entidad .
        /// </summary>
        public static string ENTIDAD_NOT_FOUND {
            get {
                return ResourceManager.GetString("ENTIDAD_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error en los datos del formulario.
        ///
        ///Los campos marcados con * necesitan un valor.
        ///Los campos marcados con ! tienen valores inadecuados..
        /// </summary>
        public static string GENERIC_VALIDATION_ERROR {
            get {
                return ResourceManager.GetString("GENERIC_VALIDATION_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Imposible bloquear el elemento seleccionado.
        ///Otro usuario está modificándolo en este momento. .
        /// </summary>
        public static string LOCK_ERROR {
            get {
                return ResourceManager.GetString("LOCK_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No ha seleccionado ningún tipo de Carpeta..
        /// </summary>
        public static string NO_ENTIDAD_SELECTED {
            get {
                return ResourceManager.GetString("NO_ENTIDAD_SELECTED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No ha sido posible realizar la operación.
        ///.
        /// </summary>
        public static string OPERATION_ERROR {
            get {
                return ResourceManager.GetString("OPERATION_ERROR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se ha encontrado una clave en el registro de Windows..
        /// </summary>
        public static string REGISTRYKEY_NOT_FOUND {
            get {
                return ResourceManager.GetString("REGISTRYKEY_NOT_FOUND", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuario no autorizado a realizar esta operación.
        /// </summary>
        public static string USER_NOT_ALLOWED {
            get {
                return ResourceManager.GetString("USER_NOT_ALLOWED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuario no autentificado.
        ///
        ///Compruebe su nombre de usuario y contraseña..
        /// </summary>
        public static string USER_NOT_AUTHENTIFICATED {
            get {
                return ResourceManager.GetString("USER_NOT_AUTHENTIFICATED", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Comprobando permisos....
        /// </summary>
        public static string USER_VALIDATION_MSG {
            get {
                return ResourceManager.GetString("USER_VALIDATION_MSG", resourceCulture);
            }
        }
    }
}