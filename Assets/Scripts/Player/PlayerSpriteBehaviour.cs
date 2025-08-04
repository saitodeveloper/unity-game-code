using System.Linq;
using UnityEngine;

public class PlayerSpriteBehaviour : MonoBehaviour
{
    public string EquipSetName;
    private EquipSet _equipSet;

    void Update()
    {

        _equipSet = PlayerEquipSetFactory.Get(EquipSetName);
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
