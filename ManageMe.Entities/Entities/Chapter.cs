using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Entities.Entities
{
    public class Chapter : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int MethodologyId { get; set; }

        public virtual Methodology Methodology { get; set; } = null!;

        public virtual ICollection<Section> Sections { get; set; } = new List<Section>();

        public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
