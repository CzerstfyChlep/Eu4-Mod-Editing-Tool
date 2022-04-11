using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eu4ModEditor
{
    class LanguageEngine
    {
        //english
        static string[] ensyllables = new string[] { "AL", "AN", "AR", "AS", "AT", "EA", "ED", "EN", "ER", "ES", "HA", "HA", "HI", "IN", "IS", "IT", "LE", "ME", "ND", "NE", "NG", "NT", "ON", "OR", "OU", "RE", "SE", "ST", "TE", "TH", "TI", "TO", "VE", "WA", "ALL", "AND", "ARE", "BUT", "ENT", "ERA", "ERE", "EVE", "FOR", "HAD", "HAT", "HEN", "HER", "HIN", "HIS", "ING", "ION", "ITH", "NOT", "OME", "OUL", "OUR", "SHO", "TED", "TER", "THA", "THE", "THI", "TIO", "ULD", "VER", "WAS", "WIT", "YOU" };
        static string[] enstartingdisabled = new string[] { "ND" };

        //swedish
        static string[] scsyllables = new string[] { "AD", "AN", "AR", "AR", "AT", "CH", "DE", "EN", "ER", "ET", "FÖ", "GE", "HA", "IG", "IN", "KA", "LA", "LI", "LL", "ME", "NA", "ND", "NG", "OC", "OM", "OR", "OR", "RA", "RE", "SK", "ST", "TA", "TE", "TI", "TT", "VA", "ADE", "ALL", "AND", "ANS", "ARA", "ATT", "DEN", "DER", "DET", "ERA", "ETT", "FÖR", "GEN", "HAN", "ILL", "ING", "INT", "JAG", "KAN", "LIG", "LLA", "LLE", "MED", "MEN", "NDE", "NGE", "NIN", "NNE", "NTE", "OCH", "SAM", "SOM", "STA", "STE", "TER", "TIL", "TTA", "VAR", "VER" };

        //nahuatl
        static string[] nhvowels = new string[] { "a", "e", "i", "o" };
        static string[] nhsyllables = new string[] { "ma", "me", "mi", "mo", "m", "na", "ne", "ni", "no", "n", "pa", "pe", "pi", "po", "p", "ta", "te", "ti", "to", "t", "ka", "ke", "ki", "ko", "k", "kwa", "kwe", "kwi", "kwo", "kw", "tsa", "tse", "tsi", "tso", "ts", "tla", "tle", "tli", "tlo", "cha", "che", "chi", "cho", "sa", "se", "si", "so", "s", "la", "le", "li", "lo", "l", "xa", "xe", "xi", "xo", "x", "ya", "ye", "yi", "yo", "y", "ra", "re", "ri", "ro", "r", "h", "w" };

        //polish
        //static string[] plsyllables = new string[] { "AN", "AL", "BY", "CH", "CI", "CZ", "DO", "DZ", "ER", "IA", "IE", "JE", "KO", "NA", "NI", "ON", "OW", "PO", "PR", "RA", "RO", "RZ", "ST", "SZ", "TA", "WI", "ZA", "ZE", "ZI", "ZY", "ACH", "ALE", "ANI", "BYŁ", "CIE", "CZE", "CZY", "DZI", "EGO", "ENI", "ERA", "EST", "IAŁ", "IED", "IEJ", "JAK", "JES", "KIE", "NIA", "NIE", "OST", "OWA", "OWI", "POW", "PRZ", "RZE", "RZY", "ŚCI", "SIĘ", "STA", "WIE", "YCH", "ZIE" };
        static string[] plsyllables = new string[] { "AN", "AL", "BY", "H", "CI", "CH", "DO", "DZ", "ER", "IA", "IE", "JE", "KO", "NA", "NI", "ON", "OW", "PO", "PR", "RA", "RO", "RZ", "ST", "SH", "TA", "WI", "ZA", "ZE", "ZI", "ZY", "AH", "ALE", "ANI", "BYL", "CYE", "CHE", "CHY", "DZI", "EGO", "ENI", "ERA", "EST", "IAL", "IED", "IEJ", "JAK", "JES", "KIE", "NIA", "NIE", "OST", "OWA", "OWI", "POW", "PRZ", "RZE", "RZY", "SCI", "SIE", "STA", "WIE", "YCH", "ZIE" };

        //mongol
        static string[] mnsyllables = new string[] { "AA", "AG", "AJ", "AL", "AN", "AR", "BA", "BO", "GA", "GE", "DA", "DE", "IJ", "JN", "LA", "ND", "OL", "ON", "OR", "SA", "TA", "UL", "UU", "HA", "HE", "YN", "EG", "EL", "EN", "ER", "EE", "ҮҮ", "AAN", "AAR", "AGA", "AJN", "ARA", "BAJ", "BOL", "GAA", "GIJ", "GOL", "GEE", "GҮJ", "DAA", "DEE", "ZEE", "IJG", "IJN", "LAG", "LIJ", "LYN", "MON", "NGO", "NIJ", "OLO", "ONG", "RÈG", "SAN", "SON", "SÈN", "TAJ", "ULA", "ULS", "HIJ", "ERE", "EER" };

        //french
        static string[] frsyllables = new string[] { "AI", "AN", "AR", "AU", "CE", "CH", "CO", "DE", "EM", "EN", "ER", "ES", "ET", "EU", "IE", "IL", "IN", "IS", "IT", "LA", "LE", "MA", "ME", "NE", "NS", "NT", "ON", "OU", "PA", "QU", "RA", "RE", "SE", "TE", "TI", "TR", "UE", "UN", "UR", "US", "VE", "AIN", "AIS", "AIT", "ANS", "ANT", "ATI", "AVA", "AVE", "CHA", "CHE", "COM", "CON", "DAN", "DES", "ELL", "EME", "ENT", "EST", "ÉTA", "EUR", "EUX", "FAI", "IEN", "ION", "IRE", "LES", "LLE", "LUS", "MAI", "MEN", "MME", "NTE", "OMM", "ONT", "OUR", "OUS", "OUT", "OUV", "PAR", "PAS", "PLU", "POU", "QUE", "RES", "SON", "SUR", "TAI", "TIO", "TOU", "TRE", "UNE", "URE", "VER", "VOU" };

        //italian
        static string[] itsyllables = new string[] { "AL", "AN", "AR", "AT", "CA", "CH", "CO", "DE", "DI", "EL", "EN", "ER", "ES", "HE", "IA", "IL", "IN", "IO", "LA", "LE", "LI", "LL", "MA", "ME", "NA", "NE", "NO", "NT", "OL", "ON", "OR", "PE", "RA", "RE", "RI", "RO", "SE", "SI", "SO", "ST", "TA", "TE", "TI", "TO", "TR", "TT", "UN", "ALE", "ALL", "ANC", "AND", "ANT", "ARE", "ATO", "ATT", "CHE", "CHI", "COM", "CON", "DEL", "ELL", "ENT", "ERA", "ERE", "ESS", "EST", "ETT", "GLI", "ION", "LLA", "MEN", "NON", "NTE", "NTI", "NTO", "OLO", "ONE", "ONO", "PER", "QUE", "SON", "STA", "STO", "TAT", "TRA", "TTO", "UNA", "VER", "ZIO" };

        //latin
        static string[] lasyllables = new string[] { "AE", "AM", "AN", "AR", "AT", "CI", "CO", "DE", "DI", "EM", "EN", "ER", "ES", "ET", "IA", "IN", "IS", "IT", "IU", "LI", "NE", "NI", "NT", "ON", "OR", "OS", "PE", "QU", "RA", "RE", "RI", "RU", "SE", "SI", "ST", "TA", "TE", "TI", "TU", "UE", "UI", "UM", "UR", "US", "ANT", "ATI", "ATU", "BUS", "CON", "CUM", "ENT", "ERA", "ERE", "ERI", "EST", "IAM", "IBU", "ILI", "ISS", "ITA", "ITU", "IUM", "IUS", "NTE", "NTI", "ORU", "PER", "PRO", "QUA", "QUE", "QUI", "QUO", "RAT", "RUM", "SSE", "TAT", "TER", "TIS", "TUM", "TUR", "TUS", "UNT" };

        //spain
        static string[] essyllables = new string[] { "AD", "AL", "AN", "AR", "AS", "CI", "CO", "DE", "DO", "EL", "EN", "ER", "ES", "IE", "IN", "LA", "LO", "ME", "NA", "NO", "NT", "ON", "OR", "OS", "PA", "QU", "RA", "RE", "RO", "SE", "ST", "TA", "TE", "TO", "UE", "UN", "ACI", "ADA", "ADO", "ANT", "ARA", "CIÓ", "COM", "CON", "DES", "DOS", "ENT", "ERA", "ERO", "EST", "IDO", "IEN", "IER", "IÓN", "LAS", "LOS", "MEN", "NTE", "NTO", "PAR", "PER", "POR", "QUE", "RES", "STA", "STE", "TEN", "TRA", "UNA", "VER" };
        //russian
        static string[] rusyllables = new string[] { "AL", "AN", "BY", "VE", "VO", "GO", "DE", "EL", "EN", "ER", "ET", "KA", "KO", "LA", "LI", "LO", "LY", "NA", "NE", "NI", "NO", "OV", "OL", "ON", "OR", "OS", "OT", "PO", "PR", "RA", "RE", "RO", "ST", "TA", "TE", "TO", "TY", "ATY", "BYL", "VER", "EGO", "ENI", "ENN", "EST", "KAK", "LYN", "OVA", "OGO", "OLY", "ORO", "OST", "OTO", "PRI", "PRO", "STA", "STV", "TOR", "CHTO", "ETO" };

        //chinese
        static string[] zhsyllables = new string[] {"er","ai","ao","ou","an","en","ang","eng","yi","ya","yao","ye","you","yan","yin","yang","ying","yong","wu","wa","wo","wai","wei","wan","wen","wang","weng","yu","yue","yuan","yun","ba","bo","bai","bei","bao","ban","bang","beng","ben","bi","biao","bie","bian","bin","bing","bu","pa","po","pai","pei","pao","pou","pan","pen","pang","peng","piao","pi","pie","pin","ping","pu","ma","mo","me","mai","mei","mao","mou","man","men","mang","meng","pian","mi","miao","mie","miu","mian","min","ming","mu","fa","fo","fei",
            "fou","fan","fen","fang","feng","ding","fu","da","de","dai","dei","dao","dou","dan","den","dang","deng","dong","di","diao","die","diu","dian","du","duo","dui","duan","dun","ta","te","tai","tei","tao","tou","tan","tang","teng","tong","ti","tiao","tie","tian","ting","tu","tuo","tui","tuan","tun","na","ne","nai","nei","nao","nou","nan","nen","nang","neng","nong","ni","niao","nie","niu","nian","nin","niang","ning","nu","nuo","nuan","nü","nüe","la","le","lai","lei","lao","lou","lan","lang","leng","long","li","lia","liao","lie","liu","lian","lin",
            "liang","ling","lu","luo","luan","lun","lü","lüe","ga","ge","gai","gei","gao","gou","gan","gen","gang","geng","gong","gu","gua","guo","guai","gui","guan","gun","guang","ka","ke","kai","kei","kao","kou","kan","ken","kang","keng","kong","ku","kua","kuo","kuai","kui","kuan","kun","kuang","ha","he","hai","hei","hao","hou","han","hen","hang","heng","hong","hu","hua","huo","huai","hui","huan","hun","huang","za","ze","zi","zai","zei","zao","zou","zan","zen","zang","zeng","zong","zu","zuo","zui","zuan","zun","ca","ce","ci","cai","cao","cou","can","cen",
            "cang","ceng","cong","cu","cuo","cui","cuan","cun","sa","se","si","sai","sao","sou","san","sen","sang","seng","song","su","suo","sui","suan","sun","zha","zhe","zhi","zhai","zhei","zhao","zhou","zhan","zhen","zhang","zheng","zhong","zhu","zhua","zhuo","zhuai","zhui","zhuan","zhun","zhuang","cha","che","chi","chai","chao","chou","chan","chen","chang","cheng","chong","chu","chua","chuo","chuai","chui","chuan","chun","chuang","sha","she","shi","shai","shei","shao","shou","shan","shen","shang","sheng","shu","shua","shuo","shuai","shui","shuan","shun",
            "shuang","re","ri","rao","rou","ran","ren","rang","reng","rong","ru","rua","ruo","rui","ruan","run","ji","jia","jiao","jie","jiu","jian","jin","jiang","jing","jiong","ju","jue","juan","jun","qi","qia","qiao","qie","qiu","qian","qi","qiang","qing","qiong","qu","que","quan","qun","xi","xia","xiao","xie","xiu","xian","xi","xiang","xing","xiong","xu","xue","xuan","xun"};

        //german
        static string[] desyllables = new string[] { "AN", "AU", "BE", "CH", "DA", "DE", "DI", "EI", "EL", "EN", "ER", "ES", "GE", "HE", "HT", "IC", "IE", "IN", "IT", "LE", "LI", "ND", "NE", "NG", "RE", "SC", "SE", "SI", "ST", "TE", "UN", "ABE", "ACH", "AND", "AUF", "AUS", "BEN", "BER", "CHE", "CHT", "DAS", "DEN", "DER", "DIE", "EIN", "EIT", "END", "ERE", "ERS", "ESE", "GEN", "HEN", "ICH", "IGE", "INE", "IST", "LIC", "LLE", "MEN", "MIT", "NDE", "NEN", "NGE", "NIC", "NTE", "REN", "SCH", "SEI", "SEN", "SIC", "SIE", "STE", "TEN", "TER", "UND", "UNG", "VER" };
        static string[] destartingdisabled = new string[] { "CH", "HT", "ND", "NG", "LLE", "NDE", "NTE" };

        //dutch
        static string[] ndsyllables = new string[] { "AA", "AL", "AN", "AR", "AT", "BE", "CH", "DA", "DE", "EE", "EL", "EN", "ER", "ET", "GE", "HE", "IE", "IJ", "IN", "KE", "LE", "ME", "ND", "NG", "OE", "ON", "OO", "OR", "RE", "ST", "TE", "VA", "VE", "WA", "ZE", "AAN", "AAR", "ACH", "ALS", "AND", "CHT", "DAT", "DEN", "DER", "EDE", "EEN", "EER", "ELĲ", "END", "ERD", "ERE", "ERS", "GEN", "HAA", "HET", "IET", "ING", "KEN", "LLE", "LIJK", "MAA", "MEN", "MET", "NDE", "NEN", "NGE", "NIE", "OND", "OOR", "RDE", "REN", "SCH", "STE", "TEN", "TER", "UIT", "VAN", "VER", "VOO", "WAS", "ZIJN" };

        //greek
        static string[] grsyllables = new string[] { "AI", "AN", "AP", "AS", "I", "IS", "IA", "IK", "KA", "MA", "ME", "NA", "I", "PO", "RA", "RO", "SE", "ST", "TA", "TE", "TI", "TI", "TO", "APO", "AIT", "YIA", "DEN", "ÍN", "IKh", "ENA", "ITA", "INA", "KAI", "MEN", "NAI", "DA", "IS", "PI", "PRO", "SI", "STI", "STO", "TAI", "TAN", "TIN", "TIS", "TIK", "TI" };

        //japanese
        static string[] jpvowels = new string[] { "a", "e", "i", "o", "u" };
        static string[] jpconsonants = new string[] { };


        //Vyeshal translator

        static Dictionary<string, string> VyeshalfromEnglish = new Dictionary<string, string> { { "ing", "eeng" },{"ang", "ang"},{"ong", "ong"},
        {"ung", "oong"},{"eng", "eng"},{"ee", "oo"},{"oo", "ee"},{"ow", "ow"},{"ah", "yaw"},{"uh", "ye"},{"oy", "yoo"},
        { "er", "al"},{"sh", "ch"},{"th", "sh"},{"ch", "j"},{"zh", "j"},{"y", "z"},{"s", "s"},{"I ", "yo "},{"A ", "ya "},{"a", "i"},
        {"e", "o"},{"i", "e"},{"o", "aw"},{"t", "t"},{"n", "n"},{"k", "k"},{"d", "g"},{"g", "d"},{"m", "v"},{"v", "m"},{"f", "h"},
        {"h", "f"},{"p", "b"},{"b", "p"},{"r", "l"},{"l", "l"},{"w", "vw"},{"z", "zh"},{"j", "z"}};


        public static void InitialiseLanguagePack()
        {
            string[] conststart = new string[] { "k", "s", "t", "n", "h", "m", "r", "g", "z", "d", "p", "b" };
            List<string> toAdd = new List<string>();
            toAdd.AddRange(new string[] { "yu", "ya", "yo", "wa", "wo", "n", "kya", "kyu", "kyo", "sha", "shu", "sho", "cha", "chu", "cho", "nya", "nyu", "nyo", "hya", "hyu", "hyo", "mya", "myu", "myo", "rya", "ryu", "ryo", "gya", "gyu", "gyo", "ja", "ju", "jo", "bya", "byu", "byo", "pya", "pyu", "pyo" });
            foreach (string cons in conststart)
            {
                foreach (string vo in jpvowels)
                {
                    if (cons == "s" && vo == "i")
                        toAdd.Add("shi");
                    else if (cons == "t" && vo == "i")
                        toAdd.Add("chi");
                    else if (cons == "t" && vo == "u")
                        toAdd.Add("tsu");
                    else if (cons == "h" && vo == "u")
                        toAdd.Add("fu");
                    else if (cons == "z" && vo == "i")
                        toAdd.Add("ji");
                    else if (cons == "d" && vo == "i")
                        ;
                    else if (cons == "d" && vo == "u")
                        ;
                    else
                        toAdd.Add(cons + vo);
                }
            }
            jpconsonants = toAdd.ToArray();
        }

        public enum Language { en = 0, sc = 1, nh, pl, mn, fr, it, la, es, ru, zh, de, nd, gr, jp, vy };

        public static string GenerateAWord(Random r, Language[] lang = null, int approximete_length = -1, bool ignorestartingrest = false)
        {
            if (lang == null)
                lang = new Language[] { Language.en };
            string[] vowels = new string[] { };
            string[] consonants = new string[] { };
            if (approximete_length == -1)
                approximete_length = r.Next(5, 10);
            string created = "";
            bool doublelettersallowed = true;
            if (lang.Count() == 1 || lang[0] == Language.vy)
            {
                switch (lang[0])
                {
                    case Language.en:
                        do
                            created += ensyllables[r.Next(0, ensyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.de:
                        do
                            created += desyllables[r.Next(0, desyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.mn:
                        do
                            created += mnsyllables[r.Next(0, mnsyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.fr:
                        do
                            created += frsyllables[r.Next(0, frsyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.nd:
                        do
                            created += ndsyllables[r.Next(0, ndsyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.it:
                        do
                            created += itsyllables[r.Next(0, itsyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.la:
                        do
                            created += lasyllables[r.Next(0, lasyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.es:
                        do
                            created += essyllables[r.Next(0, essyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.gr:
                        do
                            created += grsyllables[r.Next(0, grsyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.zh:
                        do
                            created += zhsyllables[r.Next(0, zhsyllables.Length)];
                        while (created.Length < approximete_length);
                        doublelettersallowed = false;
                        break;
                    case Language.pl:
                        do
                            created += plsyllables[r.Next(0, plsyllables.Length)];
                        while (created.Length < approximete_length);
                        doublelettersallowed = false;
                        break;
                    case Language.sc:
                        do
                            created += scsyllables[r.Next(0, scsyllables.Length)];
                        while (created.Length < approximete_length);
                        break;
                    case Language.ru:
                        do
                            created += rusyllables[r.Next(0, rusyllables.Length)];
                        while (created.Length < approximete_length);
                        doublelettersallowed = false;
                        break;
                    case Language.nh:
                        if (r.Next(0, 4) == 0)
                            created += nhvowels[r.Next(0, nhvowels.Length)];
                        int c = 0;
                        do
                        {
                            created += nhsyllables[r.Next(0, nhsyllables.Length)];
                            if (c > 0 && created.Length + 1 < approximete_length && r.Next(0, 11) == 0)
                                created += "'";
                            c++;
                        }
                        while (created.Length < approximete_length);
                        break;
                    case Language.jp:
                        vowels = jpvowels;
                        consonants = jpconsonants;
                        bool lastvowel = false;
                        do
                        {
                            if (!lastvowel && r.Next(0, 4) == 0)
                            {
                                created += vowels[r.Next(0, vowels.Length)];
                                lastvowel = true;
                            }
                            else
                            {
                                created += consonants[r.Next(0, consonants.Length)];
                                lastvowel = false;
                            }
                        }
                        while (created.Length < approximete_length);
                        break;
                    case Language.vy:
                        do
                            created += ensyllables[r.Next(0, ensyllables.Length)];
                        while (created.Length < approximete_length);
                        created = created.Replace(" the ", " ");
                        created = created.Replace(" a ", " ");
                        created = created.Replace(" an ", " ");
                        created = created.Replace(" that ", " this ");
                        created = created.Replace(" those ", " these ");
                        created = created.ToLower();
                        string translated = "";
                        while (created.Length > 0)
                        {
                            string buffer = "";
                            int current = 3;
                            do
                            {
                                if (created.Length < current)
                                    current = created.Length;
                                buffer = created.Substring(0, current);
                                if (VyeshalfromEnglish.Keys.Contains(buffer))
                                {
                                    translated += VyeshalfromEnglish[buffer];
                                    break;
                                }
                                else
                                {
                                    current--;
                                    if (current == 0)
                                        created = created.Remove(0, 1);

                                }

                            } while (current > 0);

                            created = created.Substring(current);
                        }
                        created = translated;
                        
                        break;
                }
            }
            else if (lang.Count() > 1)
            {
                do
                {
                    Language picked = lang[r.Next(0, lang.Count())];
                    created += GenerateAWord(r, new Language[] { picked }, 4);
                } while (created.Length < approximete_length);

                
            }

            if (doublelettersallowed)
            {
                string necreated = "";
                created += "  ";
                for (int a = 0; a < created.Length - 2; a++)
                {
                    if (created[a] != created[a + 1] || created[a] != created[a + 2])
                    {
                        necreated += created[a].ToString();
                    }
                }
                created = necreated;
            }
            else
            {
                string necreated = "";
                created += " ";
                for (int a = 0; a < created.Length - 1; a++)
                {
                    if (created[a] != created[a + 1])
                    {
                        necreated += created[a].ToString();
                    }
                }
                created = necreated;
            }
            if (created.Length > 1)
                return created[0].ToString().ToUpper() + created.Substring(1, created.Length - 1).ToLower();
            else if (created.Length == 1)
                return created[0].ToString().ToUpper();
            else
                return "lolololol";
        }

        
    }
}
