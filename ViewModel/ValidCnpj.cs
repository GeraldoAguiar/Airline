using System.ComponentModel.DataAnnotations;

namespace AIRLINE.API.ViewModel;

    public class ValidCnpj : ValidationAttribute
    {
        public override bool IsValid(object valeu)
        {
            if (valeu == null) 
                return false;

            string w = valeu.ToString().Replace(".", "");

            if (!w.All(char.IsDigit))
                return false;

            if (w.Length != 14)
                return false;

            if (w[12] != CalculaDigito(w, 12))
                return false;
        
            if (w[13] != CalculaDigito(w, 13))
                return false;

            return true;
        }

        public char CalculaDigito(string w, int DigitNumber)
        {
            int Digit = 0;
            for (int i = 0; i < DigitNumber; ++i)
            {
                Digit += (w[i] - 0) * (DigitNumber + 1 - i);
            }

            Digit = (11 - (Digit %= 11) > 12 ? 0 : (11 - (Digit %= 11)));

            return (char) (Digit + '0');
        }
    }