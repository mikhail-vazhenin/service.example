using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Example.YaAudience.Interfaces;
using Service.Example.YaAudience.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Service.Example.YaAudience.Services.Converters
{
    public class StatisticsJsonParser : IJsonParser<AudienceStatistic>
    {
        public AudienceStatistic Parse(JObject json)
        {
            var statistic = new AudienceStatistic();

            var age = json.SelectToken("result.age");
            statistic.Age_17 = age.SelectToken("17").Value<decimal>();
            statistic.Age_18 = age.SelectToken("18").Value<decimal>();
            statistic.Age_25 = age.SelectToken("25").Value<decimal>();
            statistic.Age_35 = age.SelectToken("35").Value<decimal>();
            statistic.Age_45 = age.SelectToken("45").Value<decimal>();
            statistic.Age_55 = age.SelectToken("55").Value<decimal>();

            var gender = json.SelectToken("result.gender");
            statistic.Gender_Man = gender.SelectToken("0").Value<decimal>();
            statistic.Gender_Woman = gender.SelectToken("1").Value<decimal>();


            var device = json.SelectToken("result.device");
            statistic.Device_Phone = device.SelectToken("2").Value<decimal>();
            statistic.Device_PC = device.SelectToken("1").Value<decimal>();
            statistic.Device_Tablet = device.SelectToken("3").Value<decimal>();

            var cities = json.SelectToken("result.city").ToObject<Dictionary<string, decimal>>();

            statistic.Cities = cities;

            statistic.Total = json.SelectToken("result.totals").Value<ulong>();
            statistic.SelfSimilarity = json.SelectToken("result.self_similarity").Value<int>();

            var interests = json.SelectToken("result.interests");
            statistic.Interests_It = interests.SelectToken("217/44").Value<decimal>();
            statistic.Interests_Realty = interests.SelectToken("217/56").Value<decimal>();
            statistic.Interests_Tourism = interests.SelectToken("217/17").Value<decimal>();
            statistic.Interests_Beauty = interests.SelectToken("217/141").Value<decimal>();
            statistic.Interests_Finance = interests.SelectToken("217/15").Value<decimal>();
            statistic.Interests_Sport = interests.SelectToken("217/71").Value<decimal>();
            statistic.Interests_Pets = interests.SelectToken("217/405").Value<decimal>();
            statistic.Interests_Family = interests.SelectToken("217/16").Value<decimal>();
            statistic.Interests_Auto = interests.SelectToken("217/13").Value<decimal>();
            statistic.Interests_Cooking = interests.SelectToken("217/23").Value<decimal>();


            var categories = json.SelectToken("result.segments");
            statistic.Categories_CarOwners = categories.SelectToken("216/519").Value<decimal>();
            statistic.Categories_EngageInSport = categories.SelectToken("217/269").Value<decimal>();
            statistic.Categories_FollowFashion = categories.SelectToken("217/272").Value<decimal>();
            statistic.Categories_Gamers = categories.SelectToken("217/421").Value<decimal>();
            statistic.Categories_Homemakers = categories.SelectToken("217/90").Value<decimal>();
            statistic.Categories_OnlinePurchases = categories.SelectToken("217/188").Value<decimal>();
            statistic.Categories_Parents = categories.SelectToken("217/566").Value<decimal>();
            statistic.Categories_Students = categories.SelectToken("217/88").Value<decimal>();
            statistic.Categories_TravelAbroad = categories.SelectToken("216/37").Value<decimal>();
            statistic.Categories_TravelAroundCountry = categories.SelectToken("216/36").Value<decimal>();

            return statistic;
        }
    }
}
