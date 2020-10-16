using System.Collections.Generic;

namespace TestConsoleCore.Data.Entities
{
    public class Group : NamedEntity
    {
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
