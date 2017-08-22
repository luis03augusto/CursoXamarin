using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TesteDrive.Data
{
    public interface ISQLite
    {
        SQLiteConnection PegarConexao();
    }
}
