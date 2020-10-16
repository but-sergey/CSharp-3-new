using Microsoft.EntityFrameworkCore;
using TestConsoleCore.Data.Entities;

namespace TestConsoleCore.Data
{
    public class StudentsDB : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; }

        public StudentsDB(DbContextOptions<StudentsDB> options) : base(options) { }
    }
}
