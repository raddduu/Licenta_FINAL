using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class DetailsParagraphVM
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int SectionId { get; set; }

        public List<DetailsDetailVM> Details { get; set; } = new List<DetailsDetailVM>();
    }
}
