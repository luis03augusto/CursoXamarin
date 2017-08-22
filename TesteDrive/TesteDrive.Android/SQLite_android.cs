using SQLite;
using System.IO;
using TesteDrive.Data;
using TesteDrive.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_android))]
namespace TesteDrive.Droid
{
    class SQLite_android : ISQLite
    {
        private const string nomeArquivoDB = "Agendamento.db3";

        public SQLiteConnection PegarConexao()
        {
            var caminhoDB = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, nomeArquivoDB);

            return new SQLiteConnection(caminhoDB);
        }
    }
}