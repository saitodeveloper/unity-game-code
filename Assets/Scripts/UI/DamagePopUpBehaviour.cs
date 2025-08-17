using TMPro;
using UnityEngine;

public class DamagePopUpBehaviour : MonoBehaviour
{
    private TMP_Text _damageValue;
    public float TimeToLive = 1f;
    private Color _enemyRedColor;

    void Awake()
    {
        _damageValue = GetComponentInChildren<TMP_Text>();
        ColorUtility.TryParseHtmlString("#ff4141", out _enemyRedColor);
    }

    void Start()
    {
        Destroy(gameObject, TimeToLive);
    }

    public void SetText(string text, bool enemyPerspective = false)
    {
        if (enemyPerspective) _damageValue.color = _enemyRedColor;
        _damageValue.SetText(text);
    }
}
