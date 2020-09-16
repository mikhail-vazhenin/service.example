using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Service.Example.YaAudience.Services.Converters;

namespace Service.Example.YaAudience.Tests.Services.Converters
{
    public class StatisticsJsonParserTests
    {
        [Test]
        public void ConvertStatistic_Success()
        {
            var _target = new StatisticsJsonParser();

            var jsonText = "{\"result\":{\"age\":{\"17\":0.026059015595439657,\"18\":0.08121775838093691,\"25\":0.31285779439832484,\"35\":0.2740905358340074,\"45\":0.1837405944208776,\"55\":0.1220343013704136},\"gender\":{\"0\":0.3966784747550663,\"1\":0.6033215252449338},\"device\":{\"1\":0.3896072005786179,\"2\":0.5581302292148477,\"3\":0.05226257020653445},\"city\":{\"Moscow\":0.24029076766367255,\"Saint Petersburg\":0.09664262894712473,\"Krasnodar\":0.04296462635429207,\"Samara\":0.03862626169089731,\"Yekaterinburg\":0.03829926382072414,\"Nizhny Novgorod\":0.029638739698120198,\"Rostov-na-Donu\":0.02070735716270025,\"Novosibirsk\":0.013855449578664691,\"Voronezh\":0.012679993517918325,\"Kazan\":0.011027062690989907},\"totals\":1800000,\"interests\":{\"217/405\":233,\"217/16\":229,\"217/17\":210,\"217/141\":243,\"217/15\":211,\"217/23\":226,\"217/56\":236,\"217/13\":183,\"217/44\":179,\"217/71\":222},\"segments\":{\"217/90\":168,\"216/519\":142,\"216/37\":169,\"217/272\":196,\"216/36\":151,\"217/188\":393,\"217/88\":302,\"217/566\":171,\"217/269\":402,\"217/421\":110},\"self_similarity\":5,\"goals\":[],\"settings\":{},\"no_data\":false},\"profile\":false}";

            var json = JsonConvert.DeserializeObject(jsonText) as JObject;


            var actual = _target.Parse(json);

            Assert.IsNotNull(actual);
            Assert.Positive(actual.Age_17);
            Assert.Positive(actual.Age_18);
            Assert.Positive(actual.Age_25);
            Assert.Positive(actual.Age_35);
            Assert.Positive(actual.Age_45);
            Assert.Positive(actual.Age_55);
            Assert.Positive(actual.Categories_CarOwners);
            Assert.Positive(actual.Categories_EngageInSport);
            Assert.Positive(actual.Categories_FollowFashion);
            Assert.Positive(actual.Categories_Gamers);
            Assert.Positive(actual.Categories_Homemakers);
            Assert.Positive(actual.Categories_OnlinePurchases);
            Assert.Positive(actual.Categories_Parents);
            Assert.Positive(actual.Categories_Students);
            Assert.Positive(actual.Categories_TravelAbroad);
            Assert.Positive(actual.Categories_TravelAroundCountry);
            Assert.Positive(actual.Interests_Auto);
            Assert.Positive(actual.Interests_Beauty);
            Assert.Positive(actual.Interests_Cooking);
            Assert.Positive(actual.Interests_Family);
            Assert.Positive(actual.Interests_Finance);
            Assert.Positive(actual.Interests_It);
            Assert.Positive(actual.Interests_Pets);
            Assert.Positive(actual.Interests_Realty);
            Assert.Positive(actual.Interests_Sport);
            Assert.Positive(actual.Interests_Tourism);
            Assert.Positive(actual.Total);
            Assert.Positive(actual.SelfSimilarity);
            Assert.Positive(actual.Cities.Count);
            Assert.Positive(actual.Device_PC);
            Assert.Positive(actual.Device_Phone);
            Assert.Positive(actual.Device_Tablet);
        }
    }
}
