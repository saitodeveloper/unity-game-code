using UnityEngine;

public class Berries : Item
{
    public Berries()
    {
        Name = "Berries";
        Type = "Consumable";
        Quanity = Random.Range(1, 3);
    }
}