using System.Linq;
using System.Text.RegularExpressions;

namespace Base.Validations
{
    public class Cif 
    {
        private static string _cif;
        /// <summary>
        /// Devuelve un error si no es correcto el código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns>ValidationError.None (indica que es correcto)</returns>
        public static ValidationError IsError(string  codigo)
        {
            _cif = codigo;
            return Validar();
        }
        /// <summary>
        /// Comprueba si es correcto el código.
        /// <param name="nif"></param>
        /// <returns></returns>
        public static bool IsOK(string codigo)
        {
            _cif = codigo;
            return (Validar() == ValidationError.None);
        }
        /// <summary>
        /// Comprueba si bien formado el cif.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static bool IsFormatted(string codigo)
        {
            _cif = codigo;
            return IsFormatted() == ValidationError.None;
        }

        private static ValidationError Validar()
        {
            string dc = _cif.ElementAt(8).ToString();
            ValidationError result = IsFormatted();
            if (result == ValidationError.None)
            {
                if ("KLM".Any(car => car == _cif.ElementAt(0)))
                {
                    result = Nif.IsError(_cif);
                }
                else
                {
                    if (dc != CalculateDC())
                    {
                        result = ValidationError.DCError;
                    }
                }
            }
            return result;
        }
        private static ValidationError IsFormatted()
        {
            string codigo = _cif;
            if (string.IsNullOrEmpty(codigo) || codigo.Length < 9)
            {
                return ValidationError.LengthError;
            }
            //A - Sociedad Anónima.
            //B - Sociedad de responsabilidad limitada.
            //C - Sociedad colectiva.
            //D - Sociedad comanditaria.
            //E - Comunidad de bienes y herencias yacentes.
            //F - Sociedad cooperativa.
            //G - Asociaciones.
            //H - Comunidad de propietarios en régimen de propiedad horizontal.
            //J - Sociedades Civiles, con o sin personalidad jurídica.
            //K - Formato antiguo, en desuso.
            //L - Formato antiguo, en desuso.
            //M - Formato antiguo, en desuso.
            //N - Entidades extranjeras.
            //P - Corporación local.
            //Q - Organismo público.
            //R - Congregaciones e Instituciones Religiosas.
            //S - Órganos de la Administración del Estado y Comunidades Autónomas.
            //U - Uniones temporales de Empresas.
            //V - Otros tipos no definidos en el resto de claves.
            //W - Establecimientos permanentes de entidades no residentes en España.
            string pattern = @"^[ABCDEFGHJKLMNPQRSUVW]\d{7}([A-J]|[0-9])$";

            if (!Regex.Match(codigo, pattern).Success)
            {
                return ValidationError.BadFormat;
            }
            return ValidationError.None;

        }

        public static string CalculateDC()
        {
            string dc = string.Empty;
            string letras = "JABCDEFGHI";
            char tipoSoc = _cif.ElementAt(0);
            string codigo = _cif.Substring(1, 7);
            int A = int.Parse(codigo.Substring(1, 1)) +
                int.Parse(codigo.Substring(3, 1)) +
                int.Parse(codigo.Substring(5, 1));
            int B = 0;
            for (int pos = 0; pos < 7; pos += 2)
            {
                int aux = int.Parse(codigo.Substring(pos, 1));
                aux *= 2;
                if (aux > 9)
                {
                    aux = int.Parse(aux.ToString().Substring(0, 1)) + int.Parse(aux.ToString().Substring(1, 1));
                }
                B += aux;
            }
            int C = A + B;
            if (C > 9)
            {
                C = int.Parse(C.ToString().Substring(1));
            }
            C = 10 - C;
            if ("ABCDEFGHJUV".Any(letra => tipoSoc == letra))
            {
                dc = C.ToString();
            }
            else
            {
                dc = letras.Substring(C, 1);
            }
            return dc;
        }
    }
}
