using Accounting.WebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.WebAPI.Extensions
{
    public static class RepositoryExtensions
    {
        //public static IQueryable<RealPerson> FilterRealPoeple(this IQueryable<RealPerson> realPeople, uint minAge, uint maxAge) =>
        //    FindByCondition(e => e.Age >= realPersonParameters.MinAge && e.Age <= realPersonParameters.MaxAge, trackChanges);


        public static IQueryable<RealPerson> Search(this IQueryable<RealPerson> realPeople, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return realPeople;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return realPeople.Where(e => e.FirstName.ToLower().Contains(lowerCaseTerm));
        }

    }
}
