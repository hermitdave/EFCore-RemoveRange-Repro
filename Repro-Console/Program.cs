using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repro_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.Migrate();
            }

            while (true)
            {
                Console.Write("Enter a channel code from 1 to 10 to reinsert records to db: ");

                string channelCode = Console.ReadLine();

                int channel = 0;

                int.TryParse(channelCode, out channel);

                if (channel == 0 || channel > 10)
                {
                    break;
                }

                ReinsertDbRecondsUsingORM(channelCode); // throws error every so often

                //ReinsertDbRecondsUsingSql(channelCode); // works like a charm
            }


        }

        static void ReinsertDbRecondsUsingORM(string channelcode)
        {
            using (var db = new AppDbContext())
            {
                var itemsToDelete = db.DbArticleData.Where(ad => ad.Channel == channelcode);
                if (itemsToDelete.Any())
                {
                    db.DbArticleData.RemoveRange(itemsToDelete);
                    db.SaveChanges();
                }
                
                Console.WriteLine("Existing records deleted");

                for (int i = 1; i <= 200; i++)
                {
                    db.DbArticleData.Add(new Database.DbArticleData() { ID = i, Channel = channelcode, Data = $"Dummy data for row {i}" });
                }

                db.SaveChanges();

                Console.WriteLine("New records added");
            }
        }

        static void ReinsertDbRecondsUsingSql(string channelcode)
        {
            using (var db = new AppDbContext())
            {
                var deleteCommand = "DELETE FROM DbArticleData WHERE Channel = @channelName";

                var channelParam = new SqliteParameter("@channelName", channelcode);

                db.Database.ExecuteSqlCommand(deleteCommand, channelParam);

                Console.WriteLine("Existing records deleted");

                for (int i = 1; i <= 200; i++)
                {
                    db.DbArticleData.Add(new Database.DbArticleData() { ID = i, Channel = channelcode, Data = $"Dummy data for row {i}" });
                }

                db.SaveChanges();

                Console.WriteLine("New records added");
            }
        }
    }
}
