using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EcoShop
{
    public class Player
    {
        public Skill[] Skills { get; }
        public string[] Talents { get; }

        public Recipe[] PreferedRecipies { get; set; }

        public Player(Skill[] skills, string[] talents)
        {
            Skills = skills;
            Talents = talents;
        }
    }
}
