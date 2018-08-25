using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeuMedicamento.data
{
    class UsuarioItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public UsuarioItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Usuario>().Wait();
        }

        public Task<List<Usuario>> GetItemsAsync()
        {
            return database.Table<Usuario>().ToListAsync();
        }


        public Task<Usuario> GetItemAsync(int idUsuario)
        {
            return database.Table<Usuario>().Where(i => i.IdUsuario == idUsuario).FirstOrDefaultAsync();
        }

        public Task<Usuario> buscaUsuarioEmail(string email)
        {
            return database.Table<Usuario>().Where(i => i.Email == email).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Usuario item)
        {
            if (item.IdUsuario != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Usuario item)
        {
            return database.DeleteAsync(item);
        }
    }
}
