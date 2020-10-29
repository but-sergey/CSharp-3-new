using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace TestConsoleCore.Data.Entities
{
    public class Student : NamedEntity
    {
        //[Key]
        //public int PrimaryKey { get; set; }

        [Required, MaxLength(120)]
        public string Surname { get; set; }

        [MaxLength(120)]
        public string Patronymic { get; set; }

        public virtual Group Group { get; set; }  // навигационные свойства
    }
}
