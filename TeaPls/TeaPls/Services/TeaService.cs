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

        public static List<Tea> teaList { get; private set; }

        static async Task Init()
        {
            if (db != null)
            {
                return;
            }

            teaList = new List<Tea>()
            {
                new Tea { Text = "C. S. Lewis", Description=" “You can never get a cup of tea large enough or a book long enough to suit me.”" },
                new Tea { Text = "Bill Watterson", Description="“Rainy days should be spent at home with a cup of tea and a good book.”" },
                new Tea { Text = "Fyodor Dostoevsky", Description="“I say let the world go to hell, but I should always have my tea.”" },
                new Tea { Text = "Lin Yutang", Description= "“There is something in the nature of tea that leads us into a world of quiet contemplation of life.”" },
                new Tea { Text = "Kakuzo Okakura", Description="“Tea … is a religion of the art of life.”" },
                new Tea { Text = "Lewis Carroll", Description="“Yes, that’s it! Said the Hatter with a sigh, it’s always tea time.”" },

                new Tea { Text = "Eisai", Description="“Tea is the elixir of life.”" },
                new Tea { Text = "George Orwell", Description="“Tea is one of the main stays of civilization in this country.”" },
                new Tea { Text = "Ancient proverb", Description="“The path to Heaven passes through a teapot.”" },
                new Tea { Text = "Unknown", Description= "“Life is like a cup of tea. It’s all in how you make it.”" },
                new Tea { Text = "Cristina Re", Description="“A cup of tea is an excuse to share great thoughts with great minds.”" },
                new Tea { Text = "Lo Tung", Description="“When I drink tea I am conscious of peace. The cool breath of heaven rises in my sleeves, and blows my cares away.” " }
            };

            // Reiti databaseen
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Tea>();
        }

        //public static async Task AddTeaList(List<Tea> teaList)
        //{
        //    await Init();

        //    for (int i = 0; i < teaList; i++)
        //    {

        //    }

        //    var id = await db.InsertAsync(tea);
        //}

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

  

        public static async Task<int> DeleteTea(Tea tea)
        {
            return await db.DeleteAsync(tea);

        }


        public static async Task<int>UpdateTea(Tea tea)
        {
            return await db.UpdateAsync(tea);

        }


        public static async Task<IEnumerable<Tea>> GetTeas()
        {
            await Init();
            var tea = await db.Table<Tea>().ToListAsync();
            return tea;
        }

        public static async Task<List<Tea>> GetTeasit()
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
