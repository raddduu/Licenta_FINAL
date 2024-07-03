using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class DetailsChapterVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int MethodologyId { get; set; }

        public List<DetailsSectionVM> Sections { get; set; } = new List<DetailsSectionVM>();

        public List<DetailsArticleVM> Articles { get; set; } = new List<DetailsArticleVM>();
    }
}
