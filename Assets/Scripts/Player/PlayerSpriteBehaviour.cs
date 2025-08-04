using System.Collections.Generic;
using System.Linq;
using Models.NeonSet;
using UnityEngine;

public class PlayerSpriteBehaviour : MonoBehaviour
{
    public string EquipSetName;
    private EquipSet _equipSet;

    void Update()
    {
        if (EquipSetName == "Neon Set")
        {
            _equipSet = new NeonSet();
        }

        if (EquipSetName == "Neon Sword")
        {
            _equipSet = new AnyEquipSet()
            {
                Items = { new NeonRightSword() }
            };

            _equipSet.HideSprites = new List<string>()
            { 
                "Sprites/Body/Shoulder L/Arm L/Equip Arm L (Shield)"
            };
        }

        if (EquipSetName == "Neon Double Sword")
        {
            _equipSet = new AnyEquipSet();
            _equipSet.Items = new List<Item>()
            {
                new NeonRightSword(),
                new NeonLeftSword()
            };

            _equipSet.HideSprites = new List<string>()
            { 
                "Sprites/Body/Shoulder L/Arm L/Equip Arm L (Shield)"
            };
        }

        if (EquipSetName == "Neon Knight")
        {
            _equipSet = new AnyEquipSet();
            _equipSet.Items = new List<Item>()
            {
                new NeonRightSword(),
                new NeonShield()
            };

            _equipSet.HideSprites = new List<string>()
            { 
                "Sprites/Body/Shoulder L/Arm L/Equip Arm L (Sword)"
            };
        }

        if (_equipSet == null) return;

        foreach (var item in _equipSet.Items)
        {
            Sprite sprite = Resources.LoadAll<Sprite>(item.SpriteResources).FirstOrDefault(s => s.name == item.Name);
            SpriteRenderer render = transform.Find(item.SpriteRender)?.GetComponent<SpriteRenderer>();

            if (render != null && sprite != null) render.sprite = sprite;
        }

        foreach (var resouceToHide in _equipSet.HideSprites)
        {
            SpriteRenderer child = transform.Find(resouceToHide)?.GetComponent<SpriteRenderer>();;
            if (child != null)
            {
                child.sprite = null;
            }
        }

        _equipSet = null;
        EquipSetName = null;
    }
}
