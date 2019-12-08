using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EcoShop
{
    public class Player
    {
        public List<Skill> Skills { get; }
        public List<string> Talents { get; }

        //public Recipe[] PreferedRecipies { get; set; }

        public Player()
        {
            Talents = new List<string>();
            Skills = new List<Skill>();
        }
    }
}
