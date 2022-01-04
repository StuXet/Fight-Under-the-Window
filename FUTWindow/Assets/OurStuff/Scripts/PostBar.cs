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

    public void SetPost(float cPost, float mPost, float pBp)
    {
        slider.gameObject.SetActive(cPost < mPost);
        slider.value = cPost;
        slider.maxValue = mPost;

        if (cPost < pBp)
        {
            slider.fillRect.GetComponentInChildren<Image>().color = low;
        }
        else
        {
            slider.fillRect.GetComponentInChildren<Image>().color = high;
        }
    }
}
