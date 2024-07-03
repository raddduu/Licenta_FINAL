using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Entities.Entities
{
    public class Paragraph : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int SectionId { get; set; }

        public virtual Section Section { get; set; } = null!;

        public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();
    }
}
