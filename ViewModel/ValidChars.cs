using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AIRLINE.API.ViewModel;

public class ValidChars : ValidationAttribute
{
    private readonly string valChars;

    public ValidChars(string ValidChars)
    {

        this.valChars = ValidChars;
    }

    public override bool IsValid(object valeu)
    {
        if (valeu == null)
        {
            return false;
        }

        string w = valeu.ToString();

        for (int i = 0; i < w.Length; i++)
        {
            if (!valChars.Contains(w[i]))
            {
                return false;
                
            }
        } 
        return true;
    }
}