using ERS.DataControler;
using ERS.Model;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conValue = builder.Configuration.GetValue<string>("ConnectionString:DB");

builder.Services.AddTransient<SqlRepository>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//--------------Ticket-----------------\\


// Get all tickets
app.MapGet("/tickets", (SqlRepository repo) => repo.getAllTickets(conValue));

//get all Pending Tickets
app.MapGet("/tickets/pending", (SqlRepository repo) => repo.GetPendingTickets(conValue));

// add new Ticket
app.MapPost("/tickets", (Ticket t, SqlRepository repo) =>
{
    repo.AddNewTicket( t, conValue);
    return Results.NoContent();
});

//get tickets from specific employee
app.MapPost("/tickets/employee", (User employee, SqlRepository repo) =>
{
    
  return repo.GetMyTickets(employee.empId, conValue);
    
});

//get tickets from specific employee with a specific ticket type
app.MapPost("/tickets/employee/type", (User employee, SqlRepository repo) =>
{
    int type =employee.requestInt;
    return repo.GetMyTicketsByType(employee.empId, type, conValue);
});

// change Ticket status
app.MapPut("/tickets/{id}", (Ticket t, SqlRepository repo) =>
{
    TicketStatus s = (TicketStatus) t.requestInt;
    repo.ChangeTicketStatus(t, s, conValue);
    return Results.NoContent();
});

//----------------Employee---------------------\\

// add new Employeee
app.MapPost("/employees", (User u, SqlRepository repo) =>
    {

        User user = repo.CreateNewUser(u, conValue);
        return Results.Created("/users/{user.empId}", user);
    });


//Employee Login
app.MapPost("/employees/login", (SqlRepository repo, User temp) =>
{
    User u = repo.EmployeeLogIn(temp, conValue);
    return Results.Created("/users/{u.empId}", u);
});

//get all Employees
app.MapGet("/employees", (SqlRepository repo) => repo.GetAllUsers(conValue));



//----------------Manager------------------\\

//manager login portal
app.MapPost("/managers/login", (SqlRepository repo, Manager temp) =>
{
    Manager m = repo.ManagerLogIn(temp, conValue);
    return Results.Created("/managers/{m.empId}", m);
});

//add new Manager
app.MapPost("/managers", (Manager m, SqlRepository repo) =>
{
    Manager man = repo.CreateNewManager(m, conValue);
    return Results.Created("/managers/ {man.empId}", man);
});

//get all Managers
app.MapGet("/managers", (SqlRepository repo) => repo.GetAllManagers(conValue));

//Promote employee to manager
app.MapPut("/managers/promote", (User u, SqlRepository repo) =>
{
    return repo.PromoteEmployee(u, conValue);   
});



app.Run();

