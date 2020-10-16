using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestConsoleCore.Data;
using TestConsoleCore.Data.Entities;

namespace TestConsoleCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string connection_str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Students.DB;Integrated Security=True";


            //var service_collection = new ServiceCollection();
            //service_collection.AddDbContext<StudentsDB>(opt => opt.UseSqlServer(connection_str));

            //var services = service_collection.BuildServiceProvider();

            //using (var db = services.GetRequiredService<StudentsDB>())
            //{

            //}

            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connection_str).Options))
            {
                //await db.Database.EnsureCreatedAsync();
                await db.Database.MigrateAsync();
                
                var students_count = await db.Students.CountAsync();
                Console.WriteLine($"Число студентов в БД = {students_count}");
            }

            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connection_str).Options))
            {
                var k = 0;
                if(await db.Students.CountAsync() == 0)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        var group = new Group
                        {
                            Name = $"Группа {i}",
                            Description = $"Описание группы {i}",
                            Students = new List<Student>()
                        };

                        for (var j = 0; j < 10; j++)
                        {
                            var student = new Student
                            {
                                Name = $"Студент {k}",
                                Surname = $"Surname {k}",
                                Patronymic = $"Patronymic {k}",
                            };
                            k++;
                            group.Students.Add(student);
                        }

                        await db.Groups.AddAsync(group);
                    }

                    await db.SaveChangesAsync();
                }
            }

            using (var db = new StudentsDB(new DbContextOptionsBuilder<StudentsDB>().UseSqlServer(connection_str).Options))
            {
                var students = await db.Students
                    .Include(s => s.Group)
                    .Where(s => s.Group.Name == "Группа 5")
                    .ToArrayAsync();

                foreach(var student in students)
                {
                    Console.WriteLine($"[{student.Id}] {student.Name} - {student.Group.Name}");
                }
            }

            Console.WriteLine("Главный поток работу закончил!");
            Console.ReadLine();
        }
    }
}
