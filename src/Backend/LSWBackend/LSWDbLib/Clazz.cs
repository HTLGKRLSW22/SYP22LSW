using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSWDbLib
{
    public class Clazz
    {
        public int ClazzId { get; set; }
        public string ClazzName { get; set; }
        public int? TeacherId { get; set; }

        public Teacher? Teacher { get; set; }
        public virtual List<Student> Students { get; set; }
    }
}
