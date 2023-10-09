using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_2023_assignment
{
    interface IClientAPI
    {
        Task<object> GetJsonData(string URL);
        Task<object> GetJsonReplacement(string URL);
    }
}
