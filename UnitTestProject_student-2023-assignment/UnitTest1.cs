using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using student_2023_assignment;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UnitTestProject_student_2023_assignment
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public async Task TestParseData()
        {
            //Данный тест проверяяет правильность отработками метода ParserJsonReplacement.ParseData(string[] dataJsonArray, Dictionary<string, string> replacemets)

            //arrange 
            ClientAPI api = new ClientAPI();
            ParserJsonReplacement parser = new ParserJsonReplacement();
            string URL_replacenent = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/replacement.json";
            string URL_data = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/data.json";
            object jsonReplacement = await api.GetJsonReplacement(URL_replacenent);
            object jsonData = await api.GetJsonData(URL_data);
            List<Replacement_Class> replacementJson;
            string[] dataJsonArray;
            string firstItemArray = "Two roads diverged in a yellow wood,";


            //act 


            replacementJson = (List<Replacement_Class>)jsonReplacement;
            parser.ParseReplacement_Class(replacementJson);
            dataJsonArray = (string[])jsonData;
            parser.ParseData(dataJsonArray, parser.replacemets);

            //assert 

            StringAssert.Equals(dataJsonArray[0], firstItemArray);

        }

        [TestMethod]
        public async Task TestParseReplacement_Class()
        {
            //Данный тест проверят привильность отработки метода ParserJsonReplacement.ParseReplacement_Class(List<Replacement_Class> replacementJson) если есть повторяющиеся элементы

            //arrange 
            ClientAPI api = new ClientAPI();
            ParserJsonReplacement parser = new ParserJsonReplacement();
            string URL_replacenent = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/replacement.json";
            object jsonReplacement = await api.GetJsonReplacement(URL_replacenent);
            List<Replacement_Class> replacementJson;
            string replacementKey = "Skooby-dooby-doooo";
            string sourceValue = "knowing how way";

            //act
            replacementJson = (List<Replacement_Class>)jsonReplacement;
            parser.ParseReplacement_Class(replacementJson);

            //assert
            StringAssert.Equals(parser.replacemets[replacementKey], sourceValue);

        }


        [TestMethod]
        public async Task TestClientApi_GetJsonData()
        {

            //В данном тесте проверяется правильность получаемой информации с gitHub, а также ее обработка методом ClientAPI.GetJsonData

            //arrange
            string URL_data = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/data.json";
            string jsonDataFile = File.ReadAllText("C:\\TheWhite\\HW2\\student-2023-assignment\\bin\\Debug\\data\\data.json");
            string[] jsonDataFileArr = JsonConvert.DeserializeObject<string[]>(jsonDataFile);

            //act
            ClientAPI api = new ClientAPI();
            object jsonData = await api.GetJsonData(URL_data);
            string[] data = (string[])jsonData;

            //assert
            CollectionAssert.AreEqual(jsonDataFileArr, data);
        }

        [TestMethod]
        public async Task TestClientApi_GetJsonReplacement_Error()
        {
            //Данный тест проверяет вывод исключения при обнаружении проблем в методе ClientAPI.GetJsonReplacement()


            //arrange
            string URL_replacenent = "https://raw.githubusercontent.com/thewhitesoft/student-2023-assignment/main/replacement.jsn"; //намерено испорченная ссылка

            //act
            ClientAPI api = new ClientAPI();
            object jsonReplacement = await  api.GetJsonReplacement(URL_replacenent);

            //assert
            StringAssert.Equals(api.exception, "Возникла ошибка в ClientAPI.GetJsonReplacement()");


        }






    }
}
