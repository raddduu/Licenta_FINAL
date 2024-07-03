using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class DetailsDetailVM
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int? ParagraphId { get; set; }

        public int? ParentDetailId { get; set; }

        public List<DetailsDetailVM> ChildrenDetails { get; set; } = new List<DetailsDetailVM>();
    }
}
