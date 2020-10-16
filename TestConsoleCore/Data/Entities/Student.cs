using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestConsoleCore.Data.Entities
{
    abstract class Entity
    {
        public int Id { get; set; }
    }

    abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }

    class Student : NamedEntity
    {
        //[Key]
        //public int PrimaryKey { get; set; }

        [Required, MaxLength(120)]
        public string Surname { get; set; }

        [MaxLength(120)]
        public string Patronymic { get; set; }

        public virtual Group Group { get; set; }  // навигационные свойства
    }

    class Group : NamedEntity
    {
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
