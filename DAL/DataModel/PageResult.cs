using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DataModel
{
    public class PageResult
    {
        public int totalCount { get; set; }
        public dynamic listItems { get; set; }
    }
}
