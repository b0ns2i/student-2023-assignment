using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace student_2023_assignment
{
    public class ParserJsonReplacement
    {

        public Dictionary<string, string> replacemets = new Dictionary<string, string>();



        public void ParseReplacement_Class(List<Replacement_Class> replacementJson)
        {

            foreach (Replacement_Class item in replacementJson)
            {
                    if (replacemets.ContainsKey(item.Replacement))
                    {
                        replacemets[item.Replacement] = item.Source;
                    }
                    else
                    {
                        replacemets.Add(item.Replacement, item.Source);

                    }
            }
        }


        public void ParseData(string[] dataJsonArray, Dictionary<string, string> replacemets)
        {
            List<int> index = new List<int>();

            for (int i = 0; i < dataJsonArray.Length; i++)
            {
                foreach (var replacement in replacemets)
                {
                    if (replacement.Key.Count() != 1)
                    {
                        dataJsonArray[i] = dataJsonArray[i].Replace(replacement.Key, replacement.Value);
                    }
                    else
                    {
                        index.Add(i);
                    }
                }
            }

            foreach (int item in index)
            {
                foreach (var replacement in replacemets)
                {
                    dataJsonArray[item] = dataJsonArray[item].Replace(replacement.Key, replacement.Value);
                }
            }
        }

    }
}
