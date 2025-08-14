using Acacia.Data.Entities;
using System.Globalization;

namespace Acacia.Data.Commons
{
    public class GeneralLocalizableEntity : BaseEntity
    {
        public string Localize(string textAr, string textEN)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            {
                return textAr;
            }
            return textEN;
        }
    }
}
