var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// In-memory list to store tasks
List<TaskItem> tasks = new List<TaskItem>();

// GET all tasks
app.MapGet("/tasks", () => Results.Ok(tasks));

// POST - Add a new task
app.MapPost("/tasks", (TaskItem task) =>
{
    tasks.Add(task);
    return Results.Created($"/tasks/{task.Id}", task);
});

// PUT - Update a task
app.MapPut("/tasks/{id}", (int id, TaskItem updatedTask) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task == null) return Results.NotFound();

    task.Name = updatedTask.Name;
    task.IsCompleted = updatedTask.IsCompleted;
    return Results.Ok(task);
});

// DELETE - Remove a task
app.MapDelete("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task == null) return Results.NotFound();

    tasks.Remove(task);
    return Results.Ok(task);
});

// Run the app
app.Run();

// Define the TaskItem model
public class TaskItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}