using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostBar : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offSet;


    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offSet);
    }

    public void SetHealth(float cHealth, float mHealth)
    {
        slider.gameObject.SetActive(cHealth < mHealth);
        slider.value = cHealth;
        slider.maxValue = mHealth;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
}
