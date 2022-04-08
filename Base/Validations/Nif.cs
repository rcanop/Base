using Base.Validations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Base.Validations
{
    /// <summary>
    /// Validación de Nif, Nie
    /// </summary>
    public static class Nif
    {
        private static string _nif = "";
        private static string digitosControl = "TRWAGMYFPDXBNJZSQVHLCKE";
        /// <summary>
        /// Devuelve si es correcto el código.
        /// <param name="nif"></param>
        /// <returns></returns>
        public static bool IsOK(string nif)
        {
            _nif = nif;
            return Validar() == ValidationError.None;
        }
        /// <summary>
        /// Devuelve un error si no es correcto el código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns>ValidationError.None (indica que es correcto)</returns>
        public static ValidationError IsError(string codigo)
        {
            _nif = codigo;
            return Validar();
        }
        public static bool IsFormatted(string codigo)
        {
            _nif = codigo;
            return IsFormatted() == ValidationError.None;
        }

        private static ValidationError Validar()
        {
            ValidationError result = IsFormatted();
            if ( result == ValidationError.None)
            {
                string letra = CalculateDC();

                if (string.IsNullOrEmpty(letra) || letra != _nif.Substring(8, 1))
                {
                    result = ValidationError.DCError;
                }
            }
            return result;
        }
        private static ValidationError IsFormatted()
        {

            if (string.IsNullOrEmpty(_nif) || _nif.Length < 9)
            {
                return ValidationError.LengthError;
            }
            string pattern = @"^([XYZKLM]|[0-9])\d{7}[" + digitosControl + "]$";

            if (!Regex.Match(_nif, pattern).Success)
            {
                return ValidationError.BadFormat;
            }
            return ValidationError.None;
        }
        public static string CalculateDC()
        {
            string cadena = "";
            int numero = 0;
            if ("XYZKLM".Any(car => car.ToString() == _nif.Substring(0, 1)))
            {
                switch (_nif.Substring(0, 1))
                {
                    case "Y":
                        cadena = "1" + _nif.Substring(1);
                        break;
                    case "Z":
                        cadena = "2" + _nif.Substring(1);
                        break;
                    default:
                        cadena = "0" + _nif.Substring(1);
                        break;

                }
            }
            else
            {
                cadena = _nif;
            }
            int.TryParse(cadena.Substring(0, 8), out numero);
            if (numero > 0)
            {
                return digitosControl.Substring((numero % 23), 1);
            }
            return "";
        }
    }
}
