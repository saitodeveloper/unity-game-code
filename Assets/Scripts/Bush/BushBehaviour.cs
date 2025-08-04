using System.Collections.Generic;
using UnityEngine;

public class BushBehaviour : ItemAbstractBehaviour
{
    public int BerriesQuantity;

    public override void OnInteractionFinished(List<Item> items)
    {
        var toPlayer = new Berries();
        toPlayer.Quanity = Item.Quanity;
        Item.Quanity = 0;
        items.Add(toPlayer);
        Transform child = transform.Find("Sprites/Berries");
        if (child != null)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        Item = new Berries();
        BerriesQuantity = Item.Quanity;
    }

    void Update()
    {
        BerriesQuantity = Item.Quanity;
    }
}
