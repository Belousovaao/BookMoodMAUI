using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace BookMoodMAUI2.Services
{
    public class DatabaseInitializerService
    {
        private readonly BookDbContext _context;

        public DatabaseInitializerService(BookDbContext context)
        {
            _context = context; 
        }

        public void Initialize() 
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "books.db");

            if (File.Exists(dbPath))
            {
                Debug.WriteLine("Database already exists");
                return;
            }
            try
            {
                var stream = FileSystem.Current.OpenAppPackageFileAsync("books.db").GetAwaiter().GetResult();
                
                if (stream == null)
                {
                    Debug.WriteLine("books.db not found");
                    return;
                }

                var tempPath = Path.GetTempFileName();
                using (var tempStream = File.Create(tempPath))
                {
                    stream.CopyTo(tempStream);
                }

                // 4. Копируем в AppData
                File.Copy(tempPath, dbPath);
                File.Delete(tempPath);


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Copy failed: {ex.Message}");
                throw;
            }
        }
    }
}
