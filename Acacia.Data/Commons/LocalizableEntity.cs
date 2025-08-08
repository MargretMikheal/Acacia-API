using System.Globalization;

namespace Acacia.Data.Commons
{
    public class LocalizableEntity
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public string GetLocalized()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
            {
                return NameAr;
            }
            return NameEn;
        }
    }
}
