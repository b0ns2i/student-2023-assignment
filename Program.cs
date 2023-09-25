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
       
        static async Task Main()
        {

            string URL_data = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/data.json";
            string URL_replacenent = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/replacement.json";

            ClientAPI api = new ClientAPI();
            ParserJsonReplacement parser = new ParserJsonReplacement();

            object jsonData = await api.GetJsonData(URL_data);
            string[] dataJsonArray;
           
            object jsonReplacement = await api.GetJsonReplacement(URL_replacenent);
            List<Replacement_Class> replacementJson;
           
           



            if (jsonReplacement != null)
            {
                replacementJson = (List<Replacement_Class>)jsonReplacement;
                parser.ParseReplacement_Class(replacementJson);

                if (jsonData != null)
                {
                    dataJsonArray = (string[])jsonData;
                    parser.ParseData(dataJsonArray, parser.replacemets);

                    List<string> data = new List<string>();

                    for (int i = 0; i < dataJsonArray.Length; i++)
                    {
                        if (dataJsonArray[i] != "")
                        {
                            data.Add(dataJsonArray[i]);
                        }
                    }

                    string dataSerialize = JsonConvert.SerializeObject(data, Formatting.Indented);


                    string filePth = "result\\result.json";

                    File.WriteAllText(filePth, dataSerialize);


                    Console.WriteLine($"Путь файла result.json: {filePth}");
                    Console.WriteLine("Результат файла:");
                    Console.WriteLine(File.ReadAllText(filePth));

                }
                else
                {
                    Console.WriteLine("Не удалось получить испорченные сообщения!");
                }
            }
            else
            {
                Console.WriteLine("Не удалось получить список замен!");
            }





            Console.ReadLine();
        }




       
    }
}
