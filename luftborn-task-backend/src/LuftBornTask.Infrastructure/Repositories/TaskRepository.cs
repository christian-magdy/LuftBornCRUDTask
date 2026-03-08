using LuftBornTask.Domain.Entities;
using LuftBornTask.Domain.Interfaces;
using LuftBornTask.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace LuftBornTask.Infrastructure.Repositories
{


    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext context;

        public TaskRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await context.Tasks.ToListAsync();
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await context.Tasks.FindAsync(id);
        }

        public async Task AddAsync(TaskItem task)
        {
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            context.Tasks.Update(task);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await context.Tasks.FindAsync(id);

            context.Tasks.Remove(task);

            await context.SaveChangesAsync();
        }
    }
}