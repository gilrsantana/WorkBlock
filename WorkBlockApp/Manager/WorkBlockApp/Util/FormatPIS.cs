using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkBlockApp.Util;

public class FormatPIS
{
    public static string Format(string pis)
    {
        if (string.IsNullOrEmpty(pis)) return "";
        var pisFormatado = pis.Replace(".", "").Replace("-", "").Replace("/", "");
        if (pis.Length != 11)
        {
            pis = pis.PadLeft(11, '0');
        }
        return Convert.ToUInt64(pisFormatado).ToString(@"000\.00000\.00\-0");
    }
}
