using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eu4ModEditor
{
    public static partial class LoadFilesClass
    {
        public static void LoadLocalisation(LoadingProgress progress)
        {
            string[] splitValues;
            string[] apostrophSplit;
            if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.localisation] != 1)
            {
                if (!Directory.Exists(GlobalVariables.pathtogame + "localisation\\"))
                    progress.ReportError($"Error: Localisation directory missing! Expected path: {GlobalVariables.pathtogame + "localisation\\"}");
                else
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(GlobalVariables.pathtogame + "localisation\\"))
                        {
                            if (file.Contains('.'))
                            {
                                if (file.Contains("l_english") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.English)
                                    continue;
                                if (file.Contains("l_french") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.French)
                                    continue;
                                if (file.Contains("l_spanish") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.Spanish)
                                    continue;
                                if (file.Contains("l_german") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.German)
                                    continue;
                                if (!file.Contains("l_english") && !file.Contains("l_french") && !file.Contains("l_spanish") && !file.Contains("l_german"))
                                    continue;
                                if (file.Split('.')[1] == "yml")
                                {
                                    int linenumber = 0;
                                    foreach (string line in File.ReadAllLines(file, Encoding.GetEncoding(1252)))
                                    {
                                        linenumber++;
                                        if (linenumber == 1)
                                            continue;
                                        string linetoread = line.Split('#')[0];
                                        if (string.IsNullOrWhiteSpace(linetoread))
                                            continue;
                                        if (!linetoread.Contains("\""))
                                        {
                                            progress.ReportError($"Alert: Strange line number {linenumber} in localisation file '{Path.GetFileName(file)}'. Skipping.");
                                            continue;
                                        }
                                        try
                                        {
                                            splitValues = linetoread.Split(':');
                                            splitValues[0] = splitValues[0].Trim();
                                            if (string.IsNullOrWhiteSpace(splitValues[0]))
                                            {
                                                progress.ReportError($"Alert: Strange line number {linenumber} in localisation file '{Path.GetFileName(file)}'. Skipping.");
                                                continue;
                                            }
                                            if (splitValues.Count() < 2)
                                            {
                                                progress.ReportError($"Alert: Strange line number {linenumber} in localisation file '{Path.GetFileName(file)}'. Skipping.");
                                                continue;
                                            }
                                            apostrophSplit = splitValues[1].Split('"');
                                            if (apostrophSplit.Count() < 2)
                                            {
                                                progress.ReportError($"Alert: Strange line number {linenumber} in localisation file '{Path.GetFileName(file)}'. Skipping.");
                                                continue;
                                            }
                                            if (GlobalVariables.LocalisationEntries.Keys.Contains(splitValues[0]))
                                                GlobalVariables.LocalisationEntries[splitValues[0]] = apostrophSplit[1];
                                            else
                                                GlobalVariables.LocalisationEntries.Add(splitValues[0], apostrophSplit[1]);
                                        }
                                        catch
                                        {
                                            if (GlobalVariables.__DEBUG)
                                                throw;
                                            progress.ReportError($"Critical error: Localisation issue! -> { Path.GetFileName(file) } -> Line '{line}' is invalid!");
                                        }

                                    }
                                }
                            }
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        if (GlobalVariables.__DEBUG)
                            throw;
                        progress.ReportError("Error: No access to localisation files! Program will exit after continuing");
                    }
                }
            }


            if (GlobalVariables.UseMod[(int)GlobalVariables.LoadFilesOrder.localisation] != 0)
            {
                foreach (string file in Directory.GetFiles(GlobalVariables.pathtomod + "localisation\\"))
                {
                    if (file.Contains('.'))
                    {
                        if (file.Contains("l_english") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.English)
                            continue;
                        if (file.Contains("l_french") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.French)
                            continue;
                        if (file.Contains("l_spanish") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.Spanish)
                            continue;
                        if (file.Contains("l_german") && GlobalVariables.LocalisationLanguage != GlobalVariables.Languages.German)
                            continue;
                        if (!file.Contains("l_english") && !file.Contains("l_french") && !file.Contains("l_spanish") && !file.Contains("l_german"))
                            continue;
                        if (file.Split('.')[1] == "yml")
                        {
                            int linenumber = 0;
                            foreach (string line in File.ReadAllLines(file, Encoding.GetEncoding(1252)))
                            {
                                linenumber++;
                                if (linenumber == 1)
                                    continue;
                                string linetoread = line.Split('#')[0];
                                if (string.IsNullOrWhiteSpace(linetoread))
                                    continue;
                                if (!linetoread.Contains("\""))
                                {
                                    progress.ReportError($"Alert: Strange line number {linenumber} in mod localisation file '{Path.GetFileName(file)}'. Skipping.");
                                    continue;
                                }
                                try
                                {
                                    splitValues = linetoread.Split(':');
                                    splitValues[0] = splitValues[0].Trim();
                                    if (string.IsNullOrWhiteSpace(splitValues[0]))
                                    {
                                        progress.ReportError($"Alert: Strange line number {linenumber} in mod localisation file '{Path.GetFileName(file)}'. Skipping.");
                                        continue;
                                    }
                                    if (splitValues.Count() < 2)
                                    {
                                        progress.ReportError($"Alert: Strange line number {linenumber} in mod localisation file '{Path.GetFileName(file)}'. Skipping.");
                                        continue;
                                    }
                                    apostrophSplit = splitValues[1].Split('"');
                                    if (apostrophSplit.Count() < 2)
                                    {
                                        progress.ReportError($"Alert: Strange line number {linenumber} in mod localisation file '{Path.GetFileName(file)}'. Skipping.");
                                        continue;
                                    }
                                    if (GlobalVariables.ModLocalisationEntries.Keys.Contains(splitValues[0]))
                                        GlobalVariables.ModLocalisationEntries[splitValues[0]] = apostrophSplit[1];
                                    else
                                        GlobalVariables.ModLocalisationEntries.Add(splitValues[0], apostrophSplit[1]);
                                }
                                catch
                                {
                                    if (GlobalVariables.__DEBUG)
                                        throw;
                                    progress.ReportError($"Critical error: Localisation issue! { Path.GetFileName(file) } has an unexpected error on line '{line}'!");
                                }

                            }


                            foreach (string line in File.ReadAllLines(file, Encoding.GetEncoding(1252)))
                            {
                                string linetoread = line.Split('#')[0];

                                if (linetoread.Contains("\""))
                                {
                                    string name = "";
                                    string value = "";
                                    try
                                    {
                                        name = linetoread.Split(':')[0].Trim();
                                        value = linetoread.Split(':')[1].Split('"')[1];
                                        if (GlobalVariables.ModLocalisationEntries.Keys.Contains(name))
                                            GlobalVariables.ModLocalisationEntries[name] = value;
                                        else
                                            GlobalVariables.ModLocalisationEntries.Add(name, value);
                                    }
                                    catch
                                    {
                                        if (GlobalVariables.__DEBUG)
                                            throw;
                                        progress.ReportError($"Critical error: Localisation issue! { Path.GetFileName(file) } has an unexpected error on line '{line}'!");
                                    }

                                }

                            }

                        }
                    }
                }
            }
        }
    }
}
