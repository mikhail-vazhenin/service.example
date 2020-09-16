using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Service.Example.YaAudience.Interfaces
{
    public interface IJsonParser<TResult> where TResult: class
    {
        TResult Parse(JObject json);
    }
}
