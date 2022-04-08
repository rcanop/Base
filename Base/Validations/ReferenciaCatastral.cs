using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Base.Validations
{
    public static class ReferenciaCatastral
    {
        private static string refCatastral = "";
        private static readonly int[] pesos = { 13, 15, 12, 5, 4, 17, 9, 21, 3, 7, 1 };
        private static Dictionary<char, int> letraValor = new Dictionary<char, int>();
        /// <summary>
        /// Devuelve si es errónea la referencia.
        /// </summary>
        /// <param name="referencia"></param>
        /// <returns>ValidationError.None (indica que es correcto)</returns>
        public static ValidationError IsError(string referencia)
        {
            refCatastral = referencia;
            return Validar();
        }
        /// <summary>
        /// Devuelve si es correcta o no la referencia
        /// </summary>
        /// <param name="referencia"></param>
        /// <returns></returns>
        public static bool IsOK(string referencia)
        {
            refCatastral = referencia;
            return Validar() == ValidationError.None;
        }
        /// <summary>
        /// Devuelve si es erróneo el formato de la referencia.
        /// </summary>
        /// <param name="referencia"></param>
        /// <returns></returns>
        public static bool IsFormatted(string referencia)
        {
            refCatastral = referencia;
            return ReferenciaCatastral.IsFormatted() == ValidationError.None;
        }
        private static ValidationError Validar()
        {
            ValidationError result = IsFormatted();
            if (result == ValidationError.None)
            {
                string dc = refCatastral.Substring(18, 2);
                if (dc != CalculateDC())
                {
                    result = ValidationError.DCError;
                }
            }
            return result;
        }
        private static ValidationError IsFormatted()
        {
            if (string.IsNullOrEmpty(refCatastral) || refCatastral.Length < 20)
            {
                return ValidationError.LengthError;
            }

            string pattern = @"^\d{5}[A-Z0-9]{13}[A-Z]{2}$";

            if (!Regex.Match(refCatastral, pattern).Success)
            {
                return ValidationError.BadFormat;
            }

            return ValidationError.None;

        }
        public static string CalculateDC()
        {
            string dc = string.Empty;
            if (letraValor.Count == 0)
            {
                RellenarMap();
            }

            dc = ObtenerLetraControl(refCatastral.Substring(0, 7) + refCatastral.Substring(14, 4)) +
                ObtenerLetraControl(refCatastral.Substring(7, 7) + refCatastral.Substring(14, 4));

            return dc;
        }
        private static void RellenarMap()
        {
            string alfabeto = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            Dictionary<string, int> map = new Dictionary<string, int>();
            for (int i = 0; i < 27; i++)
            {
                letraValor.Add(alfabeto.ElementAt(i), i + 1);
            }
            return;
        }
        private static string ObtenerLetraControl(string cadena)
        {
            string[] LetrasDC = { "M", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P"
                    , "A", "S", "D", "F", "G", "H", "J", "K", "L", "B", "Z", "X"};
            int valor = 0;
            for (int posi = 0; posi < cadena.Length; posi++)
            {
                char c = cadena[posi];
                if ("001234567890".Any(dt => dt == c))
                {
                    valor += int.Parse(c.ToString()) * pesos[posi];
                }
                else
                {
                    valor += letraValor[c] * pesos[posi];
                }
            }
            int posicion = valor % 23;
            return LetrasDC[posicion];
        }
    }
}
