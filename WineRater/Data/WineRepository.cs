using System;
using System.Collections.Generic;
using System.Linq;
using WineRater.Entities;

namespace WineRater.Data
{
    public class WineRepository : IWineRepository
    {
        private readonly WineDataContext _context;

        public WineRepository(WineDataContext context)
        {
            this._context = context;

            if (_context.DatabaseExists() == false)
            {
                _context.CreateDatabase();
                SaveWine(new Wine() { Name = "Test", Rating = 5 });
            }
        }

        public IEnumerable<Wine> GetAllWines()
        {
            return _context.Wines;
        }

        public IEnumerable<Wine> GetWinesByCriteria(Func<Wine, bool> criteria)
        {
            return _context.Wines.Where(criteria);
        }

        public void SaveWine(Wine wine)
        {
            if (wine.WineId == 0)
            {
                wine.DateAdded = DateTime.Now;
                _context.Wines.InsertOnSubmit(wine);
                _context.SubmitChanges();
            }
            else
            {
                var originalWine = _context.Wines.SingleOrDefault(x => x.WineId == wine.WineId);
                if (originalWine == null)
                {
                    throw new KeyNotFoundException(string.Format("Wine with ID {0} is not in db.", wine.WineId));
                }
                else
                {
                    originalWine.Name = wine.Name;
                    originalWine.Picture = wine.Picture;
                    originalWine.Rating = wine.Rating;
                    originalWine.WineType = wine.WineType;

                    _context.SubmitChanges();
                }
            }
        }

        public void RemoveWine(Wine wine)
        {
            _context.Wines.DeleteOnSubmit(wine);
            _context.SubmitChanges();
        }
    }
}