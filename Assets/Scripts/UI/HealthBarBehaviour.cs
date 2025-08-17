using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    public Slider slider;
    private int _targetValue;
    public float smoothSpeed = 5f;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        _targetValue = health;
    }

    public void UpdateHealthBar(int currentHealth)
    {
        _targetValue = currentHealth;
    }

    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, _targetValue, Time.deltaTime * smoothSpeed);
    }
}
