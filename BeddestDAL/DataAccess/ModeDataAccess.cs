using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity;

namespace BeddestDAL
{
    public class ModeDataAccess
    {
        public static List<Modes> GetModes(int userId)
        {
            List<Modes> result;
            using (var context = new BeddestModel())
            {
                context.Modes.Load();
                result = (from mode in context.Modes
                          where mode.UserId == userId
                          select mode).ToList();
            }
            return result;
        }

        public static Modes GetMode(int modeId)
        {
            Modes result;
            using (var context = new BeddestModel())
            {
                context.Beds.Load();
                result = (from mode in context.Modes
                          where mode.Id == modeId
                          select mode).FirstOrDefault();
            }
            return result;
        }

        public static void CreateMode(Modes newMode)
        {
            using (var context = new BeddestModel())
            {
                context.Modes.Load();
                context.Modes.Add(newMode);
                context.SaveChanges();
            }
        }
    }
}
