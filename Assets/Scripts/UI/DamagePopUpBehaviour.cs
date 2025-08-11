using TMPro;
using UnityEngine;

public class DamagePopUpBehaviour : MonoBehaviour
{
    private TMP_Text _damageValue;
    public float TimeToLive = 1f;

    void Awake()
    {
        _damageValue = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    public void SetText(string text)
    {
        _damageValue.SetText(text);
    }
}
