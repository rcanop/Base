using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Base.Validations
{
    public static class Idufir
    {
        private static string _idufir;

        /// <summary>
        /// Devuelve un error si no es correcto el código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns>ValidationError.None (indica que es correcto)</returns>
        public static ValidationError IsError(string codigo) 
        {
            _idufir = codigo;
            return Validar();
        }
        /// <summary>
        /// Comprueba si es correcto el código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static bool IsOK(string codigo)
        {
            _idufir = codigo;
            return Validar() == ValidationError.None;
        }
        private static ValidationError Validar()
        {
            ValidationError result = IsFormatted();
            if (result == ValidationError.None)
            {
                string dc = _idufir.Substring(13, 1);
                result = dc == CalculateDC() ? ValidationError.None : ValidationError.DCError;
                
            }
            return result;
        }
        /// <summary>
        /// Comprueba si está formado por 14 caracteres numéricos.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static bool IsFormatted(string codigo)
        {
            _idufir = codigo;
            return IsFormatted() == ValidationError.None;
        }
        private static ValidationError IsFormatted()
        {
            if (string.IsNullOrEmpty(_idufir) || _idufir.Length < 14)
            {
                return ValidationError.LengthError;
            }

            string pattern = @"^\d{14}$";

            if (!Regex.Match(_idufir, pattern).Success)
            {
                return ValidationError.BadFormat;
            }

            return ValidationError.None;
        }
        /// <summary>
        /// Calcular el dígito de control.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static string CalculateDC(string codigo)
        {
            string dc = string.Empty;
            if (codigo.Length >= 13 && codigo.Length <= 14)
            {
                _idufir = codigo.PadRight(14, '0');
                if (IsFormatted() == ValidationError.None)
                {
                    dc = CalculateDC();
                }
            }
            return dc;
        }
        private static string CalculateDC()
        {
            string dc = string.Empty;
            int valor = 0;
            for (int pos = 0; pos < 13; pos++)
            {
                int aux = int.Parse(_idufir.Substring(pos, 1));
                aux *= (pos % 2 == 0 ? 3 : 1);
                valor += aux;
            }
            valor = int.Parse(valor.ToString().Last().ToString());
            valor = 10 - valor;
            dc = valor.ToString().Last().ToString();
            return dc;
        }
    }
}

