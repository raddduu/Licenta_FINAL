using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Entities.Entities
{
    public class Provision : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int? ArticleId { get; set; }

        public int? ParentProvisionId { get; set; }

        public virtual Provision? ParentProvision { get; set; }

        public virtual Article Article { get; set; } = null!;

        public virtual ICollection<Provision> ChildrenProvisions { get; set; } = new List<Provision>();
    }
}
