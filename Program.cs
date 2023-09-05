using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters;

namespace student_2023_assignment
{
    internal class Program
    {
        static HttpClient  httpClient = new HttpClient();
        static async Task Main()
        {
           
            object jsonData = await httpClient.GetFromJsonAsync("https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/data.json", typeof(string[]));
            string[] dataJsonArray = (string[])jsonData;

            string currentPuth = Directory.GetCurrentDirectory();
            currentPuth = currentPuth.Replace("bin\\Debug", "");

            string path = currentPuth + "\\replacement.json";
            string json = File.ReadAllText(path);

            List<Replacement_Class> replacementJson = JsonConvert.DeserializeObject<List<Replacement_Class>>(json);
            List<string> source = new List<string>();
            List<string> replecement = new List<string>();
            foreach (Replacement_Class item in replacementJson)
            {
                if (replecement.Count == 0)
                {
                    replecement.Add(item.Replacement);
                    source.Add(item.Source);
                }
                else 
                {
                    bool uniqueness = true;
                    for (int i = 0; i < replecement.Count; i++)
                    {
                        if (replecement[i] == item.Replacement)
                        {
                            source[i] = item.Source;
                            uniqueness = false;
                        }
                    }

                    if (uniqueness == true)
                    {
                        replecement.Add(item.Replacement);
                        source.Add(item.Source);
                    }
                        
                    
                }
            }


           List<int> index = new List<int>();

            for (int i = 0; i < dataJsonArray.Length; i++)
            {
                for (int j = 0; j < replecement.Count; j++)
                {
                    if (source[j] == null)
                    {
                        dataJsonArray[i] = dataJsonArray[i].Replace(replecement[j], "");
                    } 
                    else if (source[j].Length == 1)
                    {
                        index.Add(j);
                    }
                    else
                    {
                        dataJsonArray[i] = dataJsonArray[i].Replace(replecement[j], source[j]);
                    }
                }
            }


            for (int i = 0; i < index.Count; i++)
            {
                for (int j = 0; j < dataJsonArray.Length; j++)
                {
                    dataJsonArray[j] = dataJsonArray[j].Replace(replecement[index[i]], source[index[i]]);
                }
            }


            List<string> data = new List<string>();

            for (int i = 0; i < dataJsonArray.Length; i++)
            {
                if (dataJsonArray[i] != "")
                {
                    data.Add(dataJsonArray[i]);
                }
            }

            string dataSerialize = JsonConvert.SerializeObject(data, Formatting.Indented);


           

            

            
            

            path = currentPuth + "result";

            Directory.CreateDirectory(path);

            string filePth = path + "\\result.json";

            File.WriteAllText(filePth, dataSerialize);


            Console.WriteLine($"Путь файла result.json: {filePth}");
            Console.WriteLine("Результат файла:");
            Console.WriteLine(File.ReadAllText(filePth));

        






            Console.ReadLine();
        }




       
    }
}
