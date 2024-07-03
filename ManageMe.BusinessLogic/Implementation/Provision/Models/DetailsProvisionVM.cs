using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class DetailsProvisionVM
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int? ArticleId { get; set; }

        public int? ParentProvisionId { get; set; }

        public virtual ICollection<DetailsProvisionVM> ChildrenProvisions { get; set; } = new List<DetailsProvisionVM>();
    }
}
