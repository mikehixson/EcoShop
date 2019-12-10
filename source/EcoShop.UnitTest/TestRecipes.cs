using EcoShop.DynamicValue;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.UnitTest
{
    public static class TestRecipes
    {
        public static Recipe IronBar { get; }
        public static Recipe IronGearFromIronBar { get; }
        public static Recipe IronGearFromSteel { get; }
        public static Recipe Brazier { get; }
        public static Recipe PreparedMeat { get; }
        public static Recipe MeatStock { get; }
        public static Recipe SimmeredMeat { get; }
        public static Recipe ScrapMeat { get; }

        static TestRecipes()
        {
            IronBar = new Recipe
            {
                Products = new[]
                {
                    new Item { Name = "Iron Bar", Quantity = new ConstantValue { Value = 1 } },
                    new Item
                    {
                        Name = "Tailings",
                        Quantity = new SkillModifiedValue
                        {
                            Skill = "Smelting",
                            Values = new [] { 2.0M, 1.0M, 0.9M, 0.8M, 0.7M, 0.6M, 0.5M, 0.4M }
                        }
                    }
                },
                Ingredients = new[]
                {
                    new Item
                    {
                        Name = "Iron Ore",
                        Quantity = new MultiDynamicValue
                        {
                            Children =
                            {
                                new SkillModifiedValue
                                {
                                    Skill = "Smelting",
                                    Values = new [] { 10.0M, 5.0M, 4.5M, 4.0M, 3.5M, 3.0M, 2.5M, 2.0M }
                                },
                                new TalentModifiedValue
                                {
                                    Talent = "SmeltingLavishResourcesTalent",
                                    Values = new [] { 1.0M, 0.9M }
                                }
                            }
                        }
                    }
                },
                Tables = new[]
                {
                    "Blast Furnace"
                },
                Skills = new[]
                {
                    new Skill("Smelting", 0)
                }
            };

            IronGearFromIronBar = new Recipe
            {
                Products = new[]
                {
                    new Item { Name = "Iron Gear", Quantity = new ConstantValue { Value = 1 } }
                },
                Ingredients = new[]
                {
                    new Item
                    {
                        Name = "Iron Bar",
                        Quantity = new MultiDynamicValue
                        {
                            Children =
                            {
                                new SkillModifiedValue
                                {
                                    Skill = "Mechanics",
                                    Values = new [] { 3.0M, 1.5M ,1.35M ,1.2M ,1.05M ,0.9M ,0.75M ,0.6M }
                                },
                                new TalentModifiedValue
                                {
                                    Talent = "MechanicsLavishResourcesTalent",
                                    Values = new [] { 1.0M, 0.9M }
                                }
                            }
                        }
                    }
                },
                Tables = new[]
                {
                    "Shaper"
                },
                Skills = new[]
                {
                    new Skill("Mechanics", 0)
                }
            };

            IronGearFromSteel = new Recipe
            {
                Products = new[]
                {
                    new Item { Name = "Iron Gear", Quantity = new ConstantValue { Value = 2 } }
                },
                Ingredients = new[]
                {
                    new Item
                    {
                        Name = "Steel",
                        Quantity = new MultiDynamicValue
                        {
                            Children =
                            {
                                new SkillModifiedValue
                                {
                                    Skill = "Advanced Smelting",
                                    Values = new [] { 2.0M, 1.0M, 0.9M, 0.8M, 0.7M, 0.6M, 0.5M, 0.4M }
                                },
                                new TalentModifiedValue
                                {
                                    Talent = "AdvancedSmeltingLavishResourcesTalent",
                                    Values = new [] { 1.0M, 0.9M }
                                }
                            }
                        }
                    }
                },
                Tables = new[]
                {
                    "Electric Machinist Table"
                },
                Skills = new[]
                {
                    new Skill("Advanced Smelting", 0)
                }
            };

            Brazier = new Recipe
            {
                Products = new[]
                {
                    new Item { Name = "Brazier", Quantity = new ConstantValue { Value = 1 } }
                },
                Ingredients = new[]
                {
                    new Item
                    {
                        Name = "Iron Bar",
                        Quantity = new MultiDynamicValue
                        {
                            Children =
                            {
                                new SkillModifiedValue
                                {
                                    Skill = "Smelting",
                                    Values = new[] { 10.0M, 5.0M, 4.5M, 4.0M, 3.5M, 3.0M, 2.5M, 2.0M }
                                },
                                new TalentModifiedValue
                                {
                                    Talent = "SmeltingLavishResourcesTalent",
                                    Values = new [] { 1.0M, 0.9M }
                                }
                            }
                        }
                    }
                },
                Tables = new[]
                {
                    "Anvil"
                },
                Skills = new[]
                {
                    new Skill("Smelting", 2)
                }
            };

            PreparedMeat = new Recipe
            {
                Products = new[]
                {
                    new Item { Name = "Prepared Meat", Quantity = new ConstantValue { Value = 1 } },
                    new Item { Name = "Scrap Meat", Quantity = new ConstantValue { Value = 2 } }
                },
                Ingredients = new[]
                {
                    new Item
                    {
                        Name = "Raw Meat",
                        Quantity = new MultiDynamicValue
                        {
                            Children =
                            {
                                new SkillModifiedValue
                                {
                                    Skill = "Butchery",
                                    Values = new[] { 10.0M, 5.0M, 4.5M, 4.0M, 3.5M, 3.0M, 2.5M, 2.0M }
                                },
                                new TalentModifiedValue
                                {
                                    Talent = "ButcheryLavishResourcesTalent",
                                    Values = new [] { 1.0M, 0.9M }
                                }
                            }
                        }
                    }
                },
                Tables = new[]
                {
                    "Butchery Table"
                },
                Skills = new[]
                {
                    new Skill("Butchery", 1)
                }
            };

            MeatStock = new Recipe
            {
                Products = new[]
                {
                    new Item { Name = "Meat Stock", Quantity = new ConstantValue { Value = 1 } },
                },
                Ingredients = new[]
                {
                    new Item
                    {
                        Name = "Scrap Meat",
                        Quantity = new MultiDynamicValue
                        {
                            Children =
                            {
                                new SkillModifiedValue
                                {
                                    Skill = "Cooking",
                                    Values = new[] { 20.0M, 10.0M, 9.0M, 8.0M, 7.0M, 6.0M, 5.0M, 4.0M }
                                },
                                new TalentModifiedValue
                                {
                                    Talent = "CookingLavishResourcesTalent",
                                    Values = new [] { 1.0M, 0.9M }
                                }
                            }
                        }
                    }
                },
                Tables = new[]
                {
                    "Cast Iron Stove"
                },
                Skills = new[]
                {
                    new Skill("Cooking", 1)
                }
            };

            SimmeredMeat = new Recipe
            {
                Products = new[]
                {
                    new Item { Name = "Simmered Meat", Quantity = new ConstantValue { Value = 1 } },
                },
                Ingredients = new[]
                {
                    new Item
                    {
                        Name = "Prepared Meat",
                        Quantity = new MultiDynamicValue
                        {
                            Children =
                            {
                                new SkillModifiedValue
                                {
                                    Skill = "Cooking",
                                    Values = new[] { 5.0M, 2.5M, 2.25M, 2.0M, 1.75M, 1.5M, 1.25M, 1.0M }
                                },
                                new TalentModifiedValue
                                {
                                    Talent = "CookingLavishResourcesTalent",
                                    Values = new[] { 1.0M, 0.9M }
                                }
                            }
                        }
                    },
                    new Item
                    {
                        Name = "Meat Stock",
                        Quantity = new MultiDynamicValue
                        {
                            Children =
                            {
                                new SkillModifiedValue
                                {
                                    Skill = "Cooking",
                                    Values = new[] { 2.0M, 1.0M, 0.9M, 0.8M, 0.7M, 0.6M, 0.5M, 0.4M }
                                },
                                new TalentModifiedValue
                                {
                                    Talent = "CookingLavishResourcesTalent",
                                    Values = new[] { 1.0M, 0.9M }
                                }
                            }
                        }
                    }
                },
                Tables = new[]
                {
                    "Cast Iron Stove"
                },
                Skills = new[]
                {
                    new Skill("Cooking", 2)
                }
            };

            ScrapMeat = new Recipe
            {
                Products = new[]
                {
                    new Item { Name = "Scrap Meat", Quantity = new ConstantValue { Value = 1 } },
                },
                Ingredients = new[]
                {
                    new Item
                    {
                        Name = "Raw Meat",
                        Quantity = new MultiDynamicValue
                        {
                            Children =
                            {
                                new SkillModifiedValue
                                {
                                    Skill = "Butchery",
                                    Values = new[] { 1.0M, 0.5M, 0.45M, 0.4M, 0.35M, 0.3M, 0.25M, 0.2M }
                                },
                                new TalentModifiedValue
                                {
                                    Talent = "ButcheryLavishResourcesTalent",
                                    Values = new[] { 1.0M, 0.9M }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
