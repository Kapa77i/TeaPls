using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeaPls.Models;
using Xamarin.Essentials;
using TeaPls.Services;
using System.Runtime.CompilerServices;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;



//[assembly: Dependency(typeof(TeaService))]

namespace TeaPls.Services
{
    public class TeaService 
    {

        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            // Reiti databaseen
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Tea>();
        }

        public static async Task AddTea(string text, string description)
        {
            await Init();
            var tea = new Tea
            {
                Text = text,
                Description = description
            };
            var id = await db.InsertAsync(tea);
        }

        public static async Task RemoveTea(int id)
        {
            await Init();
            await db.DeleteAsync<Tea>(id);

        }

        public static async Task<IEnumerable<Tea>> GetTeas()
        {
            await Init();
            var tea = await db.Table<Tea>().ToListAsync();
            return tea;
        }


        public static async Task<Tea> GetTea(int id)
        {
            await Init();

            var tea = await db.Table<Tea>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return tea;
        }

    
    }
}
