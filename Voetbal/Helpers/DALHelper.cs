
using SQLite;
using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;

namespace Voetbal.Helpers
{
    public static class CommandsHelper
    {
        private const String DATABASE_FOLDER = "databases";
        private static SQLiteConnection connection;
        private const String REGEX_DOMAIN_CHECK = @"^[a-zA-Z0-9][a-zA-Z0-9-_]{0,61}[a-zA-Z0-9]{0,1}\.([a-zA-Z]{1,6}|[a-zA-Z0-9-]{1,30}\.[a-zA-Z]{2,})$";
        private const String DatabaseFileName = "Voetbal.db3";


        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            if (type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att)
            {
                return valueSelector(att);
            }
            return default;
        }

        private static string DatabaseFolderPath
        {
            get
            {
                // Create folder
                string documentFolderPath = Environment.CurrentDirectory;
                string databaseFolderPath = Path.Combine(documentFolderPath, DATABASE_FOLDER);

                if (!Directory.Exists(databaseFolderPath))
                {
                    Directory.CreateDirectory(databaseFolderPath);
                }

                return databaseFolderPath;
            }
        }

        public static string DomainFolderPath
        {
            get
            {
                // Create folder
                string domainFolderPath = Path.Combine(DatabaseFolderPath, CurrentDomain);

                if (!Directory.Exists(domainFolderPath))
                {
                    Directory.CreateDirectory(domainFolderPath);
                }

                return domainFolderPath;
            }
        }

        private static String _currentDomain = "Voetbal";
        public static String CurrentDomain
        {
            get
            {
                return _currentDomain;
            }
            set
            {

                if (Regex.IsMatch(value, REGEX_DOMAIN_CHECK))
                {
                    _currentDomain = value;
                }
                else
                {
                    throw new ArgumentException("Invalid domain provided");
                }
            }
        }

        public static String GetDatabaseFile()
        {
            String filePath = GetDatabaseFilePath();

            return filePath;
        }

        public static SQLiteConnection GetDatabaseConnection()
        {
            if (connection == null)
            {
                String databaseFile = GetDatabaseFile();

                // Return connection
                if (databaseFile != null)
                {
                    try
                    {
                        connection = new SQLiteConnection(databaseFile);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return connection;
        }

        [SecurityCritical]
        public static String GetDatabaseFilePath()
        {
            return Path.Combine(DomainFolderPath, DatabaseFileName);
        }
    }
}