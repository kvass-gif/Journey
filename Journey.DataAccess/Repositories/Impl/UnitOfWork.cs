﻿using Journey.DataAccess.Database;

namespace Journey.DataAccess.Repositories.Impl;

public class UnitOfWork : IUnitofWork
{
    private readonly JourneyWebContext _context;
    private PlaceRepository placeRepo;
    public UnitOfWork(JourneyWebContext context)
    {
        _context = context;
    }
    public IPlaceRepository PlaceRepo
    {
        get
        {
            if (placeRepo == null)
            {
                placeRepo = new PlaceRepository(_context);
            }
            return placeRepo;
        }
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

