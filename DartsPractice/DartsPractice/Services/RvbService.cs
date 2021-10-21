using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DartsPractice.Models;
using SQLite;
using Xamarin.Essentials;

namespace DartsPractice.Services
{
    public static class RvbService
    {

        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "RvbData.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<RVBScore>();
        }

        public static async Task AddRvbStats(RVBScore score)
        {
            await Init();
            await db.InsertAsync(score);
        }

        public static async Task RemoveRvbStats(int id)
        {
            await Init();
            await db.DeleteAsync<RVBScore>(id);
        }

        public static async Task<IEnumerable<RVBScore>> GetRvbStats()
        {
            await Init();
            var RvbScores = await db.Table<RVBScore>().ToListAsync();
            return RvbScores;
        }

        public static async Task<RVBScore> GetRvbStats(int id)
        {
            await Init();
            var RvbScore = await db.GetAsync<RVBScore>(id);
            return RvbScore;
        }
    }
}
