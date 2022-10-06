using Microsoft.EntityFrameworkCore;
using SimpleBlog.Model.Entities;

namespace SimpleBlog.Data;

public class BlogContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    { }



}
