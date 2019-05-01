using SQLite;
using System;
using System.Collections.Generic;
using Voetbal.BLL.Models;
using Voetbal.Helpers;

namespace Voetbal.DAL
{
    public class SQLiteDatabase : IDisposable
    {
        private Boolean _disposed = false;
        private static readonly object _locker = new object();
        public SQLiteConnection database;

        public SQLiteDatabase(SQLiteConnection connection)
        {
            this.database = connection;

            this.database.CreateTable<Coach>();
            this.database.CreateTable<Person>();
            this.database.CreateTable<Player>();
            this.database.CreateTable<Team>();
        }

        ~SQLiteDatabase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (database != null)
                    {
                        database.Dispose();
                    }
                }

                _disposed = true;
            }
        }

        public TableQuery<T> Table<T>() where T : class, IDatabaseEntity, new()
        {
            lock (_locker)
            {
                return database.Table<T>();
            }
        }

        public int Execute(String query, params object[] args)
        {
            lock (_locker)
            {
                return database.Execute(query, args);
            }
        }

        public T ExecuteScalar<T>(String query, params object[] args) where T : class, IDatabaseEntity
        {
            lock (_locker)
            {
                return database.ExecuteScalar<T>(query, args);
            }
        }

        public IEnumerable<T> Query<T>(String query, params object[] args) where T : class, IDatabaseEntity, new()
        {
            lock (_locker)
            {
                return database.Query<T>(query, args);
            }
        }

        public IEnumerable<T> QueryEntity<T>(String query, params object[] args) where T : class, IDatabaseEntity, new()
        {
            lock (_locker)
            {
                return database.Query<T>(query, args);
            }
        }

        public T FindWithQuery<T>(String Query, params object[] Params) where T : class, IDatabaseEntity, new()
        {
            return database.FindWithQuery<T>(Query, Params);
        }

        public T GetItem<T>(Guid id) where T : class, IDatabaseEntity, new()
        {
            lock (_locker)
            {
                return database.FindWithQuery<T>($"SELECT * FROM {(typeof(T).GetAttributeValue((TableAttribute ta) => ta.Name))} WHERE Id = ? LIMIT 1", id);
            }
        }


        public bool InsertItem<T>(T item) where T : class, IDatabaseEntity, new()
        {
            lock (_locker)
            {
                T existingItem = GetItem<T>(item.Id);

                if (existingItem != null)
                {
                    return database.Update(item) > 0;
                }
                else
                {
                    return database.Insert(item) > 0;
                }
            }
        }

        public bool SaveItems<T>(IEnumerable<T> items) where T : class, IDatabaseEntity
        {
            lock (_locker)
            {
                return database.InsertAll(items, typeof(T)) > 0;
            }
        }

        public bool UpdateItems<T>(IEnumerable<T> items) where T : class, IDatabaseEntity
        {
            lock (_locker)
            {
                return database.UpdateAll(items) > 0;
            }
        }

        public int DeleteItem<T>(T item) where T : class, IDatabaseEntity
        {
            lock (_locker)
            {
                return database.Delete<T>(item.Id);
            }
        }
    }
}
