using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Entities.Entities
{
    public class Section : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? ChapterId { get; set; }

        public int? ParentSectionId { get; set; }

        public virtual Section? ParentSection { get; set; }

        public virtual Chapter Chapter { get; set; } = null!;

        public virtual ICollection<Section> ChildrenSections { get; set; } = new List<Section>();

        public virtual ICollection<Paragraph> Paragraphs { get; set; } = new List<Paragraph>();
    }
}
