using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Example.YaAudience.Models
{
    public class AudienceStatistic
    {
        public int Id { get; set; }

        public ulong Total { get; set; }

        public decimal Age_17 { get; set; }
        public decimal Age_18 { get; set; }
        public decimal Age_25 { get; set; }
        public decimal Age_35 { get; set; }
        public decimal Age_45 { get; set; }
        public decimal Age_55 { get; set; }

        /// <summary>
        /// Gender 0
        /// </summary>
        public decimal Gender_Man { get; set; }

        /// <summary>
        /// Gender 1
        /// </summary>
        public decimal Gender_Woman { get; set; }


        /// <summary>
        /// Device 2
        /// </summary>
        public decimal Device_Phone { get; set; }
        /// <summary>
        /// Device 1
        /// </summary>
        public decimal Device_PC { get; set; }

        /// <summary>
        /// Device 3
        /// </summary>
        public decimal Device_Tablet { get; set; }


        public IDictionary<string, decimal> Cities { get; set; }

        public decimal Interests_It { get; set; }
        public decimal Interests_Realty { get; set; }
        public decimal Interests_Tourism { get; set; }
        public decimal Interests_Beauty { get; set; }
        public decimal Interests_Finance { get; set; }
        public decimal Interests_Sport { get; set; }
        public decimal Interests_Pets { get; set; }
        public decimal Interests_Family { get; set; }
        public decimal Interests_Auto { get; set; }
        public decimal Interests_Cooking { get; set; }

        public decimal Categories_TravelAbroad { get; set; }
        public decimal Categories_TravelAroundCountry { get; set; }
        public decimal Categories_CarOwners { get; set; }
        public decimal Categories_OnlinePurchases { get; set; }
        public decimal Categories_EngageInSport { get; set; }
        public decimal Categories_Students { get; set; }
        public decimal Categories_Parents { get; set; }
        public decimal Categories_FollowFashion { get; set; }
        public decimal Categories_Homemakers { get; set; }
        public decimal Categories_Gamers { get; set; }

        public int SelfSimilarity { get; set; }

    }
}
