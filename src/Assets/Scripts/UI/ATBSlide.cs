using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ATBSlide : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxSlider(float amount)
    {
        slider.maxValue = amount;
    }

    public void SetSlider(float amount)
    {
        slider.value = amount;
    }
}
