using Common.Helper;
using System;
using System.Runtime.Serialization;

namespace DAL.Helper 
{
    /// <summary>
    /// Clase que desiga fallos declarados de la capa DAL. 
    /// </summary>
    [Serializable]
    public class DALException : GenericException 
    {
         /// <summary>
        /// Constructor sin parametros. 
        /// </summary>
        public DALException() { }

        /// <summary>
        /// Constructor con base en un mensaje.
        /// </summary>
        /// <param name="message">Mensaje contenido en la excepcion.</param>
        public DALException(string message) : base(message) { }

        /// <summary>
        /// Constructor con base en un mensaje y una excepcion base.
        /// </summary>
        /// <param name="message">Mensaje contenido en la excepcion.</param>
        /// <param name="baseException">Excepcion que origina la excepcion creada.</param>
        public DALException(string message, Exception baseException) : base(message, baseException) { }

         /// <summary>
        /// Constructor Regular 
        /// </summary>
        /// <param name="info">Contiene la informacion asociada a la excepcion</param>
        /// <param name="context">Informacion contextual de origen y destino de la excepcion</param>
        protected DALException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}