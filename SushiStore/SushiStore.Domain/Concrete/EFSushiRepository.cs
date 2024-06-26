﻿using SushiStore.Domain.Abstract;
using SushiStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiStore.Domain.Concrete
{
    public class EFSushiRepository : ISushiRepository
    {
        EFDbContext context=new EFDbContext();
        public IEnumerable<Sushi> Sushis
        {
            get {  return context.Sushis; }
        }

        public Sushi DeleteSushi(int sushiId)
        {
            Sushi DbEntry=context.Sushis.Find(sushiId);
            if (DbEntry != null)
            {
                context.Sushis.Remove(DbEntry);
                context.SaveChanges();
            }
            return DbEntry;
        }

        public void SaveSushi(Sushi sushi)
        {
            if (sushi.SushiId == 0)
                context.Sushis.Add(sushi);
            else
            {
                Sushi dbEntry = context.Sushis.Find(sushi.SushiId);

                if (dbEntry != null)
                {
                    dbEntry.Name = sushi.Name;
                    dbEntry.Description = sushi.Description;
                    dbEntry.Price = sushi.Price;
                    dbEntry.Category = sushi.Category;
                    dbEntry.ImageData = sushi.ImageData;
                    dbEntry.ImageMimeType = sushi.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
    }
}
