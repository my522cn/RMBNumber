using System.Text.RegularExpressions;

namespace RMBNumber
{
    public class RMBNumber
    {
        public static string ConvertToChinese(decimal number)
        {
            var s = number.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            var d = Regex.Replace(s, @"((?'x'-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${x}${b}${z}");
            var r = Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰"[m.Value[0] - '-'].ToString());
            return r.EndsWith("元") ? r + "整" : r;
        }

        public static decimal ConvertToNumber(string number)
        {
            bool fushu = number.StartsWith("负");
            // -14321432143214321432143214321.12M
            // 负壹穰肆仟叁佰贰拾壹秭肆仟叁佰贰拾壹垓肆仟叁佰贰拾壹京肆仟叁佰贰拾壹兆肆仟叁佰贰拾壹亿肆仟叁佰贰拾壹万肆仟叁佰贰拾壹元整
            var s1 = Regex.Replace(number.Replace("负", ""), ".", m =>
            {
                string s = "零壹贰叁肆伍陆柒捌玖分角拾佰仟万亿兆京垓秭穰元整";
                int index = s.IndexOf(m.Value.ToString());
                if (index > 0 && index <= 9) return index.ToString();
                else if (index > 9) return s[index].ToString();
                else return "";
            });
            var s2 = Regex.Matches(s1, @"(.+穰)|(.+秭)|(.+垓)|(.+京)|(.+兆)|(.+亿)|(.+万)|(.+元)|(.角)|(.分)");
            decimal r = 0;

            foreach (var s3 in s2)
            {
                var s4 = Regex.Matches(s3.ToString(), @".");
                switch (s4[s4.Count - 1].Value)
                {
                    case "分": { r += decimal.Parse(s4[0].Value) * 0.01M; }; break;
                    case "角": { r += decimal.Parse(s4[0].Value) * 0.1M; }; break;
                    case "元": { r += ToNumb(s4) * 1M; }; break;
                    case "万": { r += ToNumb(s4) * 10000M; }; break;
                    case "亿": { r += ToNumb(s4) * 100000000M; }; break;
                    case "兆": { r += ToNumb(s4) * 1000000000000M; }; break;
                    case "京": { r += ToNumb(s4) * 10000000000000000M; }; break;
                    case "垓": { r += ToNumb(s4) * 100000000000000000000M; }; break;
                    case "秭": { r += ToNumb(s4) * 1000000000000000000000000M; }; break;
                    case "穰": { r += ToNumb(s4) * 10000000000000000000000000000M; }; break;
                }
            }
            return fushu ? -r : r;
        }

        private static decimal ToNumb(MatchCollection s)
        {
            decimal r = 0;
            for (int i = 1; i < s.Count; i++)
            {
                switch (s[i].Value)
                {
                    case "仟": { r += decimal.Parse(s[i - 1].Value) * 1000M; }; break;
                    case "佰": { r += decimal.Parse(s[i - 1].Value) * 100M; }; break;
                    case "拾": { r += decimal.Parse(s[i - 1].Value) * 10M; }; break;
                    default: { r += decimal.Parse(s[i - 1].Value); }; break;
                }
                i++;
            }
            return r;
        }
    }
}
