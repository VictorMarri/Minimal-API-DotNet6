using MiniTodo.DataAccess;
using MiniTodo.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(); //Registrando nosso Contexto de aplicação
builder.Services.AddEndpointsApiExplorer();//Swagger
builder.Services.AddSwaggerGen();//Swagger

var app = builder.Build();
app.UseSwagger(); //Swagger
app.UseSwaggerUI(); //Swagger

app.MapGet("v1/todos", (ApplicationDbContext context) =>
{
    var todos = context.Todos.ToList();
    return Results.Ok(todos);
}).Produces<Todo>();

app.MapPost("v1/todos", (ApplicationDbContext context, CreateTodoViewModel model) =>
{
    var todo = model.MapTo();

    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);

});

app.Run();
