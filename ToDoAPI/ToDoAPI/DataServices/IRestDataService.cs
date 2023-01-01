using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApi.Models;

namespace ToDoApi.DataServices
{
    public interface IRestDataService
    {
        Task<List<ToDo>> GetAllToDosAsync();
        Task AddTodoAsync(ToDo toDo);
        Task DeleteTodoAsync(int id);
        Task UpdateToDoAsync(ToDo todo);
    }
}
