using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TestConsoleCore.Data
{
    class StudentDbContextFactory : IDesignTimeDbContextFactory<StudentsDB>
    {
        public StudentsDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentsDB>();
            const string connection_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Students.DB;Integrated Security=True";
            optionsBuilder.UseSqlServer(connection_str);

            return new StudentsDB(optionsBuilder.Options);
        }
    }
}
