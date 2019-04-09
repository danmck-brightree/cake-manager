using System;
using System.Collections.Generic;

namespace CakeManager.Shared
{
    public class CakeMarkGridData
    {
        public DateTime LatestEventDate { get; set; }
        public List<CakeMarkGridDataItem> Items { get; set; } = new List<CakeMarkGridDataItem>();
    }

    public class CakeMarkGridDataItem
    {
        public string Email { get; set; }
        public int CakeMarks { get; set; }
        public int SuperCakeMarks { get; set; }
        public DateTime LatestEventDate { get; set; }
    }
}
