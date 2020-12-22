using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WatchEcology.Models
{
    public class SampleData
    {
        public static async Task InitializeMusicStoreDatabaseAsync(IServiceProvider serviceProvider, bool createUsers = false)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<WatchecologyContext>();

                if (await db.Database.EnsureCreatedAsync())
                {
                    await InsertTestData(scopeServiceProvider);
                    if (createUsers)
                    {
                        await CreateAdminUser(scopeServiceProvider);
                    }
                }
            }
        }

        private static Task CreateAdminUser(IServiceProvider scopeServiceProvider)
        {
            throw new NotImplementedException();
        }

        private static Task InsertTestData(IServiceProvider scopeServiceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
