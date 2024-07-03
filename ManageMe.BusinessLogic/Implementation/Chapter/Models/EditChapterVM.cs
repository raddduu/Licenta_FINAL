using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class EditChapterVM
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int MethodologyId { get; set; }
    }
}
