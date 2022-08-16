using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using W1.Models;
using System.Text.Json;

namespace W1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext context){
             _context = context;              
                if (_context.TodoItems.Count() == 0)
                {                 
                _context.TodoItems.Add(new todoitem { desc = "Item1" });
                _context.SaveChanges();             
                }         
        }
        [HttpGet] 
        public ActionResult<List<todoitem>> GetAll() 
        {     
        return _context.TodoItems.ToList(); 
        } 
        
        [HttpGet("{id}", Name = "GetTodo")] 
        public string GetById(int id) 
        {    
        var item = _context.TodoItems.Find(id);     
        if (item == null)    
        {         
            string b="NotFound";
        return b;
        }     
        var opt=new JsonSerializerOptions{WriteIndented=true};
            string a="Task list \n" + JsonSerializer.Serialize(_context.TodoItems,opt);    
            return a;
        }
        [HttpPost]
        public string Post(todoitem t1){
            _context.TodoItems.Add(t1);
            _context.SaveChanges();  
            var opt=new JsonSerializerOptions{WriteIndented=true};
            string a="Task list" + JsonSerializer.Serialize(t1,opt);    
            return a;
        }
        [HttpPut("{id}")]
    public ActionResult Update(int id,todoitem item)
    {
        if (item == null)
        {
            return BadRequest();
        }

        var supplier = _context.TodoItems.Find(id);
        if (supplier == null)
        {
            return NotFound();
        }

        supplier.desc = item.desc;

        _context.TodoItems.Update(supplier);
        _context.SaveChanges();
        return Ok();
    }
    }
}
