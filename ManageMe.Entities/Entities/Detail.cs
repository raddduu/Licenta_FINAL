using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Entities.Entities
{
    public class Detail : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int? ParagraphId { get; set; }

        public int? ParentDetailId { get; set; }

        public virtual Detail? ParentDetail { get; set; }

        public virtual Paragraph Paragraph { get; set; } = null!;

        public virtual ICollection<Detail> ChildrenDetails { get; set; } = new List<Detail>();
    }
}
