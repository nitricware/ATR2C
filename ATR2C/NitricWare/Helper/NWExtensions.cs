using System.Text;
using System.Text.RegularExpressions;

namespace ATCSVCreator.NitricWare.Helper; 

public static class NwExtensions {
    public static string Truncate(this string s, int length) {
        if (s.Length > length)
            return s.Substring(0, length);
        return s;
    }

    public static string ReplaceUmlaut(this string s) {
        StringBuilder sb = new StringBuilder (s);

        sb.Replace("ä", "ae");
        sb.Replace("Ä", "Ae");
        sb.Replace("ö", "oe");
        sb.Replace("Ö", "Oe");
        sb.Replace("ü", "üe");
        sb.Replace("Ü", "Ue");

        return sb.ToString();
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