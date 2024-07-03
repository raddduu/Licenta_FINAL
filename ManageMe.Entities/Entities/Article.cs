using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.Entities.Entities
{
    public class Article : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int ChapterId { get; set; }

        public virtual Chapter Chapter { get; set; } = null!;

        public virtual ICollection<Provision> Provisions { get; set; } = new List<Provision>();
    }
}
