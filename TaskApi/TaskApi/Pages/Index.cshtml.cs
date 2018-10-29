using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskApi.Models;
using Microsoft.EntityFrameworkCore;


namespace TaskApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TaskContext _context;       

        public IndexModel(TaskContext db)
        {
            _context = db;
            
        }

        //public string Message { get; private set; } = "PageModel in C#";

        public IList<TaskItem> Tasks { get; private set; }

        public async Task OnGetAsync() 
        {
            //Message += $" Server time is { DateTime.Now }";                     

            Tasks = await _context.TaskItems.AsNoTracking().ToListAsync();
        }     

        

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {            
                 
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
          
        }
    }
}