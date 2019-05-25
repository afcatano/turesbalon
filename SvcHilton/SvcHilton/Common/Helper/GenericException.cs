using System;
using System.Runtime.Serialization;

namespace Common.Helper
{
    /// <summary>
    /// Clase base de la que deben extender todas las excepciones de la aplicación
    /// </summary>

    [Serializable]
    public class GenericException : Exception
    {
        /// <summary>
        /// Constructor sin parametros.
        /// </summary>
        public GenericException() { }

        /// <summary>
        /// Constructor basado en un mensaje informativo.
        /// </summary>
        /// <param name="message">Mensaje incluido en la excepcion</param>
        public GenericException(string message) : base(message) { }

        /// <summary>
        /// Constructor basado en un mensaje y una excepcion base 
        /// </summary>
        /// <param name="message">Mensaje incluido en la excepcion</param>
        /// <param name="innerException">Excepcion base asociada</param>
        public GenericException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        /// Constructor Regular 
        /// </summary>
        /// <param name="info">Contiene la informacion asociada a la excepcion</param>
        /// <param name="context">Informacion contextual de origen y destino de la excepcion</param>
        protected GenericException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
