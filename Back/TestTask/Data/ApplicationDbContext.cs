//using Microsoft.EntityFrameworkCore;
//using TestTask.Enums;
//using TestTask.Models;

//namespace TestTask.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public DbSet<ToDoTask> Tasks { get; set; }

//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//        {
//            Database.EnsureDeleted();
//            Database.EnsureCreated();
//        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {

//            modelBuilder.Entity<ToDoTask>().HasData(
//                new ToDoTask[]
//                {
//                    new ToDoTask {Id=1, Text="Apple", Completed=true },
//                    new ToDoTask {Id=2, Text="Banana", Completed=false },
//                    new ToDoTask {Id=3, Text="Dog", Completed=true },


//                });


//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ToDoTask> Tasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        //{
        //    Database.EnsureDeleted();
        //    Database.EnsureCreated();
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<ToDoTask>().HasData(
        //        new ToDoTask[]
        //        {
        //            new ToDoTask {Id=1, Text="Apple", Completed=true },
        //            new ToDoTask {Id=2, Text="Banana", Completed=false },
        //            new ToDoTask {Id=3, Text="Dog", Completed=true },


        //        });


        //}
    }
}

