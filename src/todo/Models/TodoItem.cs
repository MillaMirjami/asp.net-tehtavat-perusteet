namespace Todo.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public TodoItem(long id, string name, bool isComplete)
    {
        Id = id;
        Name = name;
        IsComplete = isComplete;
    }
}