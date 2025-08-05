using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerSpriteBehaviour : MonoBehaviour
{
    public string EquipSetName;
    private EquipSet _equipSet;
    private Dictionary<string, SpriteVisibilityData> _currentSprites = new Dictionary<string, SpriteVisibilityData>();

    void Awake()
    {
        FillSpritesDataRecursive(transform);
    }

    void Update()
    {

        _equipSet = PlayerEquipSetFactory.Get(EquipSetName);

        if (EquipSetName == "Revert")
        {
            foreach (KeyValuePair<string, SpriteVisibilityData> kvp in _currentSprites)
            {
                Sprite sprite = Resources.LoadAll<Sprite>(kvp.Value.selectedSpritePng).FirstOrDefault(s => s.name == kvp.Value.selectedSpritePiece);
                SpriteRenderer render = transform.Find(kvp.Key)?.GetComponent<SpriteRenderer>();

                if (render != null && sprite != null) render.sprite = sprite;
                else if (render != null) render.sprite = null;
            }
            _equipSet = null;
            EquipSetName = null;
            return;
        }

        if (_equipSet == null || string.IsNullOrEmpty(EquipSetName))
        {
            return;
        }

        foreach (var item in _equipSet.Items)
        {
            Sprite sprite = Resources.LoadAll<Sprite>(item.SpriteResources).FirstOrDefault(s => s.name == item.Name);
            SpriteRenderer render = transform.Find(item.SpriteRender)?.GetComponent<SpriteRenderer>();

            if (render != null && sprite != null) render.sprite = sprite;
        }

        foreach (var resouceToHide in _equipSet.HideSprites)
        {
            SpriteRenderer child = transform.Find(resouceToHide)?.GetComponent<SpriteRenderer>();
            if (child != null)
            {
                child.sprite = null;
            }
        }

        _equipSet = null;
        EquipSetName = null;
    }

    void FillSpritesDataRecursive(Transform transform)
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer spriteRender = child?.GetComponent<SpriteRenderer>();
            if (spriteRender != null)
            {
                var path = GetFullPath(child);
                string assetPath = AssetDatabase.GetAssetPath(spriteRender.sprite);
                _currentSprites[path] = new SpriteVisibilityData(RemoveParent(assetPath, 2).Replace(".png", ""), spriteRender.sprite?.name);
            }

            FillSpritesDataRecursive(child);
        }
    }

    string GetFullPath(Transform transform)
    {
        string path = transform.name;
        while (transform.parent != null)
        {
            transform = transform.parent;
            path = transform.name + "/" + path;
        }

        return RemoveParent(path);
    }

    string RemoveParent(string path, int skip = 1)
    {
        return string.Join("/", path.Split('/').Skip(skip));
    }
}

public class SpriteVisibilityData
{
    public string selectedSpritePng;
    public string selectedSpritePiece;

    public SpriteVisibilityData(string selectedSpritePng, string selectedSpritePiece)
    {
        this.selectedSpritePng = selectedSpritePng;
        this.selectedSpritePiece = selectedSpritePiece;
    }
}
