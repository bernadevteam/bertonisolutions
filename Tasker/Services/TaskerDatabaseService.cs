using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Tasker.Models;

namespace Tasker.Services
{
    public class TaskerDatabaseService
    {
        bool initialized = false;
        SQLiteAsyncConnection Database;
        private TaskerDatabaseService()
        {
            Database = new SQLiteAsyncConnection(SqlLiteConfig.DatabasePath, SqlLiteConfig.Flags);
            InitializeAsync().SafeFireAndForget(false);
        }
        public static TaskerDatabaseService Instance { get; } = new TaskerDatabaseService();
        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(TaskItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(TaskItem)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<TaskItem>> GetItemsAsync()
        {
            return Database.Table<TaskItem>().ToListAsync();
        }

        public Task<List<TaskItem>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<TaskItem>("SELECT * FROM [TaskItem] WHERE [IsDone] = 0");
        }

        public Task<TaskItem> GetItemAsync(int id)
        {
            return Database.Table<TaskItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(TaskItem item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(TaskItem item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
