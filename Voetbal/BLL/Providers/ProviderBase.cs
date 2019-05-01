using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voetbal.DAL;

namespace Voetbal.BLL.Providers
{
    public class ProviderBase : IDisposable
    {
        protected SQLiteDatabase Database { get; set; }
        public ProviderBase(SQLiteConnection connection)
        {
            this.Database = new SQLiteDatabase(connection);
        }

        public void Dispose()
        {
        }
    }
}
