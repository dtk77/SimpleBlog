using Microsoft.EntityFrameworkCore;
using SimpleBlog.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlog.Data
{
    public class BlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        { }
        

    
    }
}
