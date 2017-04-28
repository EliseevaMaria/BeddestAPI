using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeddestDAL
{
    public static class Connection
    {
        public static string ConnectionString = @"Server=tcp:beddest.database.windows.net,1433;Data Source=beddest.database.windows.net;Initial Catalog=Beddest;Persist Security Info=False;User ID=maria.eliseeva;Password=Qwerty123;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";        
    }
}
