using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop
{
    public class Recipe
    {
        public Item[] Products { get; set; }
        public Item[] Ingredients { get; set; }
        public string[] Tables { get; set; }
        public Skill[] Skills { get; set; }
    }
}
