using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity;

namespace BeddestDAL
{
    public class BedDataAccess
    {
        public static List<Bed> GetAllBeds()
        {
            List<Bed> result;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                result = context.Beds.ToList();
            }
            return result;
        }

        public static List<Bed> GetBeds(int userId)
        {
            List<Bed> result;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                result = (from bed in context.Beds
                          where bed.UserId == userId
                          select bed).ToList();
            }
            return result;
        }

        public static Bed GetBed(int bedId)
        {
            Bed result;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                result = (from bed in context.Beds
                          where bed.BedId == bedId
                          select bed).FirstOrDefault();
            }
            return result;
        }

        public static Bed GetBedByBlock(int blockId)
        {
            Bed result;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                result = (from bed in context.Beds
                          where bed.Blocks.Select(x => x.BlockId).Contains(blockId)
                          select bed).FirstOrDefault();
            }
            return result;
        }

        public static void UpdateHeatingTime(int bedId, int newTime)
        {
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                var bedToChange = context.Beds.Find(bedId);
                if (bedToChange != null)
                {
                    bedToChange.StopHeatingTime = newTime;
                    context.SaveChanges();
                }
            }
        }

        public static void UpdateTemperature(int bedId, int newTemp)
        {
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                var bedToChange = context.Beds.Find(bedId);
                if (bedToChange != null)
                {
                    bedToChange.Temperature = newTemp;
                    context.SaveChanges();
                }
            }
        }

        public static bool BedRegistered(int bedId)
        {
            Bed bedFound;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                bedFound = context.Beds.Find(bedId);
            }
            return bedFound != null;
        }

        public static int AddBed(Bed bed)
        {
            int newId;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                context.Beds.Add(bed);
                context.Entry(bed).State = System.Data.Entity.EntityState.Added;

                context.SaveChanges();

                newId = context.Beds.ToList().Last().BedId;
                var bedToChange = context.Beds.Find(newId);
                if (bedToChange != null)
                {
                    bedToChange.Name = "Bed" + newId.ToString();
                    context.SaveChanges();
                }
            }
            return newId;
        }

        public static void RemoveBed(int bedId)
        {
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                Bed bedToRemove = context.Beds.Find(bedId);
                context.Beds.Remove(bedToRemove);
                context.SaveChanges();
            }
        }
    }
}
