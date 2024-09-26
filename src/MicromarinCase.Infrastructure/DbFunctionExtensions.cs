using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMarinCase.Infrastructure
{
    public static class DbFunctionExtensions
    {
        [DbFunction("JSON_VALUE", Schema = "", IsBuiltIn = true)]
        public static string JsonValue(string source, [NotParameterized] string path) => throw new NotSupportedException();
    }
}
