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
        public static List<Beds> GetBeds(int userId)
        {
            List<Beds> result;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                result = (from bed in context.Beds
                          where bed.UserId == userId
                          select bed).ToList();            
            }
            return result;
        }
        
        public static Beds GetBed(int bedId)
        {
            Beds result;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                result = (from bed in context.Beds
                          where bed.Id == bedId
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
            Beds bedFound;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                bedFound = context.Beds.Find(bedId);
            }
            return bedFound != null;
        }

        public static void AddBed(Beds bed)
        {
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                context.Beds.Add(bed);
                context.SaveChanges();
            }
        }
    }
}
