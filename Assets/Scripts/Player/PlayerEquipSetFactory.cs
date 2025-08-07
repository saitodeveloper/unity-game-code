using System;
using System.Collections.Generic;
using Models.NeonSet;

public class PlayerEquipSetFactory
{
    private static readonly Dictionary<string, Func<EquipSet>> Factory = new()
    {
        { "Neon Set", () => new NeonSet() },
        { "Neon Right Sword", () => new AnyEquipSet
            {
                Items = { new NeonRightSword() },
                HideSprites = new List<string>
                {
                    "Sprites/Body/Shoulder L/Arm L/Equip Arm L (Shield)"
                }
            }
        },
        { "Neon Left Sword", () => new AnyEquipSet
            {
                Items = new List<Item>
                {
                    new NeonLeftSword()
                },
                HideSprites = new List<string>
                {
                    "Sprites/Body/Shoulder L/Arm L/Equip Arm L (Shield)"
                }
            }
        },
        { "Neon Knight", () => new AnyEquipSet
            {
                Items = new List<Item>
                {
                    new NeonRightSword(),
                    new NeonShield()
                },
                HideSprites = new List<string>
                {
                    "Sprites/Body/Shoulder L/Arm L/Equip Arm L (Sword)"
                }
            }
        }
    };

    public static EquipSet Get(string equipSetName)
    {
        if (!string.IsNullOrEmpty(equipSetName) && Factory.TryGetValue(equipSetName, out var creator))
        {
            return creator();
        }

        return null;
    }
}