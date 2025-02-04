using Todo.Models;

namespace Todo.Data
{
    public class TodoData
    {
        // Esimerkkitiedot, jotka palautetaan, jos tietokanta on tyhjä
        public static List<TodoItem> GetInitialTodoItems()
        {
            return new List<TodoItem>
            {
                new TodoItem(1, "Eat", false), // Anna id, name ja isComplete
                new TodoItem(2, "Sleep", true),  // Sama täällä
                new TodoItem(3, "Repeat", false)
            };
        }
    }
}
