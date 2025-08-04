using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAbstractBehaviour : MonoBehaviour
{
    private Item _item;

    public Item Item
    {
        get { return _item; }
        set { _item = value; }
    }

    public abstract void OnInteractionFinished(List<Item> items);
}