﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class CreateParagraphVM
    {
        public int Id { get; set; }

        public string Text { get; set; } = null!;

        public int SectionId { get; set; }
    }
}
