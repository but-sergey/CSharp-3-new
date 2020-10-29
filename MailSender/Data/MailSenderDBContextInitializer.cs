using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MailSender.Data
{
    class MailSenderDBContextInitializer : IDesignTimeDbContextFactory<MailSenderDB>
    {
        public MailSenderDB CreateDbContext(string[] args)
        {
            const string connection_string = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MailSender.DB;Integrated Security=True";
            //const string connection_string = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Students.DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            var optionsBuilder = new DbContextOptionsBuilder<MailSenderDB>();
            optionsBuilder.UseSqlServer(connection_string);

            return new MailSenderDB(optionsBuilder.Options);
        }
    }
}
