using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Entities.Entities
{
    public class Semester : IEntity
    {
        public int Id { get; set; }
        public int SemesterNumber { get; set; }
    }
}
