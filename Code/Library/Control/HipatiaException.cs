using System;
using System.Collections.Generic;
using System.Text;

using moleQule.Library;

namespace moleQule.Library.Hipatia
{
    
    /// <summary>
    /// Códigos de error de moleQule
    /// </summary>
    public enum HipatiaCode
    {
        NO_CODE = 0,
        NO_ENTIDAD = 1,
        NO_AGENTE = 2
    }

    public class HipatiaException : moleQule.Library.iQException
    {
        private HipatiaCode _h_code;

        /// <summary>
        /// Codigo de error
        /// </summary>
        public new HipatiaCode Code
        {
            get { return _h_code; }
            set { _h_code = value; }
        }

        public HipatiaException(string msg) : this(msg, HipatiaCode.NO_CODE) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">Mensaje</param>
        /// <param name="code">Código del mensaje</param>
        public HipatiaException(string msg, HipatiaCode code) 
            : base(msg) 
        {
            _h_code = code;
        }
    }

    public class DocRepeatedHipatiaException : HipatiaException
    {
        public DocRepeatedHipatiaException()
            : base(Resources.Messages.DOCUMENT_REPEATED) { }
    }
}
