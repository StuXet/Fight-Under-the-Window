using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;

    public void SetHealth(float cHealth, float mHealth)
    {
        slider.value = cHealth;
        slider.maxValue = mHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
}
