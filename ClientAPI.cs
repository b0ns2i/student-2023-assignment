using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace student_2023_assignment
{
    class ClientAPI
    {
        HttpClient httpClient = new HttpClient();
   

        public async Task<object> GetJsonData(string URL)
        {
            object jsonData = null;
            try
            {
                jsonData = await httpClient.GetFromJsonAsync(URL, typeof(string[]));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return jsonData;
          
        }

        public async Task<object> GetJsonReplacement(string URL)
        {
            object jsonReplacement = null;
            try
            {
                jsonReplacement = await httpClient.GetFromJsonAsync(URL, typeof(List<Replacement_Class>));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return jsonReplacement;
        }
       

    }
}
