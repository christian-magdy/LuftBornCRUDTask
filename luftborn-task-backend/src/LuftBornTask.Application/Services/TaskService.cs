using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuftBornTask.Domain.Interfaces;
using LuftBornTask.Domain.Entities;
using LuftBornTask.Application.DTOs;
using LuftBornTask.Application.Interfaces;
using LuftBornTask.Application.Exceptions;

namespace LuftBornTask.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository repository;

        public TaskService(ITaskRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllAsync()
        {
            var tasks = await repository.GetAllAsync();

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted
            });
        }

        public async Task<TaskDto> CreateAsync(TaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            await repository.AddAsync(task);

            dto.Id = task.Id;

            return dto;
        }

        public async Task<TaskDto> GetByIdAsync(int id)
        {
            var task = await repository.GetByIdAsync(id);

            if (task == null)
                throw new NotFoundException($"Task with id {id} not found");

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted
            };
        }

        public async Task UpdateAsync(TaskDto dto)
        {
            var existingTask = await repository.GetByIdAsync(dto.Id);

            if (existingTask == null)
                throw new NotFoundException($"Task with id {dto.Id} not found");

            existingTask.Title = dto.Title;
            existingTask.Description = dto.Description;
            existingTask.IsCompleted = dto.IsCompleted;

            await repository.UpdateAsync(existingTask);
        }

        public async Task DeleteAsync(int id)
        {
            var task = await repository.GetByIdAsync(id);

            if (task == null)
                throw new NotFoundException($"Task with id {id} not found");

            await repository.DeleteAsync(id);
        }
    }
}