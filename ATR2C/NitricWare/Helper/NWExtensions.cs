using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ATCSVCreator.NitricWare.Helper; 

public static class NwExtensions {
    public static string Truncate(this string s, int length) {
        if (s.Length > length)
            return s.Substring(0, length);
        return s;
    }

    // https://stackoverflow.com/questions/7470997/replace-german-characters-umlauts-accents-with-english-equivalents
    
    private static readonly IReadOnlyDictionary<string, string> SpecialDiacritics = new Dictionary<string, string> {
        { "ä".Normalize(NormalizationForm.FormD), "ae".Normalize(NormalizationForm.FormD) },
        { "Ä".Normalize(NormalizationForm.FormD), "Ae".Normalize(NormalizationForm.FormD) },
        { "ö".Normalize(NormalizationForm.FormD), "oe".Normalize(NormalizationForm.FormD) },
        { "Ö".Normalize(NormalizationForm.FormD), "Oe".Normalize(NormalizationForm.FormD) },
        { "ü".Normalize(NormalizationForm.FormD), "ue".Normalize(NormalizationForm.FormD) },
        { "Ü".Normalize(NormalizationForm.FormD), "Ue".Normalize(NormalizationForm.FormD) },
        { "ß".Normalize(NormalizationForm.FormD), "ss".Normalize(NormalizationForm.FormD) },
    };

    public static string RemoveDiacritics(this string s) {
        var stringBuilder = new StringBuilder(s.Normalize(NormalizationForm.FormD));

        // Replace certain special chars with special combinations of ascii chars (eg. german umlauts and german double s)
        foreach (var keyValuePair in SpecialDiacritics)
            stringBuilder.Replace(keyValuePair.Key, keyValuePair.Value);

        // Remove other diacritic chars eg. non spacing marks https://www.compart.com/en/unicode/category/Mn
        for (int i = 0; i < stringBuilder.Length; i++)
        {
            char c = stringBuilder[i];

            if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark)
                stringBuilder.Remove(i, 1);
        }
        
        return stringBuilder.ToString();
    }
    
    /// <summary>
    /// https://stackoverflow.com/questions/34423441/insert-a-newline-and-tab-every-nth-character-in-string-c-sharp
    /// </summary>
    /// <param name="text"></param>
    /// <param name="maxLineLength"></param>
    /// <param name="indent"></param>
    /// <returns></returns>
    public static string AddLineBreaks(this string text, int maxLineLength, string indent = "\t") {        
        // Strip off any whitespace (including \r) before each pre-existing end of line character.
        text = Regex.Replace(text, @"\s+\n", "\n", RegexOptions.Multiline);

        // Matches that are too long include a trailing whitespace character (excluding newline)
        // which is then used to sense that an indent should occur
        // Regex to match whitespace except newline: https://stackoverflow.com/a/3469155/538763
        string regex = @"(\n)|([^\n]{0," + maxLineLength + @"}(?!\S)[^\S\n]?)";

        return Regex.Replace(text, regex, m => m.Value +
                                               (m.Value.Length > 1 && Char.IsWhiteSpace(m.Value[m.Value.Length - 1]) ? ("\n" + indent) : ""));
    }
}