using System.Collections.Generic;

namespace Models.NeonSet
{
    class NeonTorso: Item
    {
        public NeonTorso()
        {
            Name = "Neon Torso";
            Type = "Equip";
            Quanity = 1;
            SpriteResources = "Sprites/Equips/Armors/Neon Set";
            SpriteRender = "Sprites/Body/Torso";
        }
    }
    class NeonLeftLeg: Item
    {
        public NeonLeftLeg()
        {
            Name = "Neon Left Leg";
            Type = "Equip";
            Quanity = 1;
            SpriteResources = "Sprites/Equips/Armors/Neon Set";
            SpriteRender = "Sprites/Body/Leg L";
        }
    }

    class NeonRightLeg : Item
    {
        public NeonRightLeg()
        {
            Name = "Neon Right Leg";
            Type = "Equip";
            Quanity = 1;
            SpriteResources = "Sprites/Equips/Armors/Neon Set";
            SpriteRender = "Sprites/Body/Leg R";
        }
    }

    class NeonLeftShoulder: Item
    {
        public NeonLeftShoulder()
        {
            Name = "Neon L Shoulder";
            Type = "Equip";
            Quanity = 1;
            SpriteResources = "Sprites/Equips/Armors/Neon Set";
            SpriteRender = "Sprites/Body/Shoulder L";
        }
    }
    
    class NeonRightShoulder: Item
    {
        public NeonRightShoulder()
        {
            Name = "Neon R Shoulder";
            Type = "Equip";
            Quanity = 1;
            SpriteResources = "Sprites/Equips/Armors/Neon Set";
            SpriteRender = "Sprites/Body/Shoulder R";
        }
    }

    class NeonLeftArm : Item
    {
        public NeonLeftArm()
        {
            Name = "Neon Arm";
            Type = "Equip";
            Quanity = 1;
            SpriteResources = "Sprites/Equips/Armors/Neon Set";
            SpriteRender = "Sprites/Body/Shoulder L/Arm L";
        }
    }

    class NeonRightArm: Item
    {
        public NeonRightArm()
        {
            Name = "Neon Arm";
            Type = "Equip";
            Quanity = 1;
            SpriteResources = "Sprites/Equips/Armors/Neon Set";
            SpriteRender = "Sprites/Body/Shoulder R/Arm R";
        }
    }

    class NeonHead: Item
    {
        public NeonHead()
        {
            Name = "Neon Head";
            Type = "Equip";
            Quanity = 1;
            SpriteResources = "Sprites/Equips/Armors/Neon Set";
            SpriteRender = "Sprites/Face/Equip Head";
        }
    }


    class NeonMouth: Item
    {
        public NeonMouth()
        {
            Name = "Neon Mouth";
            Type = "Equip";
            Quanity = 1;
            SpriteResources = "Sprites/Equips/Armors/Neon Set";
            SpriteRender = "Sprites/Face/Mouth Equip";
        }
    }


    public class NeonSet : EquipSet
    {
        public NeonSet()
        {
            Items = new List<Item> {
                new NeonLeftShoulder(),
                new NeonRightShoulder(),
                new NeonTorso(),
                new NeonLeftArm(),
                new NeonRightArm(),
                new NeonLeftLeg(),
                new NeonRightLeg(),
                new NeonMouth(),
                new NeonHead()
            };

            HideSprites = new List<string> { "Sprites/Face/Hair" };
        }
    }
}
