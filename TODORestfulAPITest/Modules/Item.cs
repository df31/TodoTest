using System;
using System.Collections.Generic;
using System.Text;

namespace TodoAPITest.Modules
{
    public class Item
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }
}
