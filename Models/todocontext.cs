using Microsoft.EntityFrameworkCore;  
namespace W1.Models {     
public class TodoContext : DbContext     
  {         
    public TodoContext(DbContextOptions<TodoContext> options)                            : base(options)         
{         
}       
    public DbSet<todoitem> TodoItems { get; set; }     
  
   } 
}