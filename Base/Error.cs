using Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base
{
    /// <summary>
    /// Clase usada para pasar almacenar información de procesos.
    /// </summary>
    public class Error: IError
    {
        /// <summary>
        /// Indica si ha habido un error
        /// </summary>
        public bool IsError
        {
            get
            {
                return errorMessages.Count > 0;
            }
        }
        /// <summary>
        /// Muestra todos los mensajes almacenados, los de información y los de error.
        /// </summary>
        public string Messages => GetMessages();
        /// <summary>
        /// Muestra los mensaje de información almacenados.
        /// </summary>
        public string Information => GetInformation();
        /// <summary>
        /// Muestra los mensajes de error almacenados.
        /// </summary>
        public string Errors => GetErrors();

        private List<string> errorMessages = new List<string>();
        private List<string> informationMessages = new List<string>();

        private string GetMessages()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (errorMessages.Count > 0)
            {
                stringBuilder.AppendLine("ERRORES");
                foreach (string item in errorMessages)
                {
                    stringBuilder.AppendLine(item);
                }
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.AppendLine();
            }

            if (informationMessages.Count > 0)
            {
                stringBuilder.AppendLine("AVISOS");
                foreach (string item in informationMessages)
                {
                    stringBuilder.AppendLine(item);
                }
            }
            return stringBuilder.ToString();
        }
        private string GetErrors()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (this.errorMessages.Count > 0)
            {
                foreach (string item in errorMessages)
                {
                    stringBuilder.AppendLine(item);
                }
            }
            return stringBuilder.ToString();
        }

        private string GetInformation()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (informationMessages.Count > 0)
            {
                stringBuilder.AppendLine("AVISOS");
                foreach (string item in informationMessages)
                {
                    stringBuilder.AppendLine(item);
                }
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Añade un error.
        /// </summary>
        /// <param name="message">Mensaje del error</param>
        public void AddError(string message)
        {
            errorMessages.Add(message);
        }
        /// <summary>
        /// Añade una información.
        /// </summary>
        /// <param name="message">Contenido de dicha información</param>
        public void AddInformation(string message)
        {
            informationMessages.Add(message);
        }
        public void Reset()
        {
            errorMessages.Clear();
            informationMessages.Clear();
        }
    }
}
