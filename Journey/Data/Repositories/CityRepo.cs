﻿using Journey.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Data.Repositories
{
    public class CityRepo
    {
        private readonly DbSet<City> _cities;
        public CityRepo(ApplicationDbContext appDbContext)
        {
            _cities = appDbContext.Cities;
        }
        public IQueryable<City> Cities()
        {
            var cities = (from a in _cities
                          orderby a.CityName
                          select a);
            return cities;
        }
        public Dictionary<int, string> ToDictionary()
        {
            var citiesDictionary = new Dictionary<int, string>();
            foreach (var item in Cities())
            {
                citiesDictionary.Add(item.Id, item.CityName);
            }
            return citiesDictionary;
        }
    }
}