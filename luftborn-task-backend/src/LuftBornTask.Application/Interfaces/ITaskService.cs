using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuftBornTask.Application.DTOs;


namespace LuftBornTask.Application.Interfaces
    ;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync();

    Task<TaskDto> GetByIdAsync(int id);

    Task<TaskDto> CreateAsync(TaskDto task);

    Task UpdateAsync(TaskDto task);

    Task DeleteAsync(int id);
}