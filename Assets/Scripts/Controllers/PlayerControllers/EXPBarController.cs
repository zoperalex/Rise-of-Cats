using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXPBarController : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    public Image fill;

    public void SetMaxEXP(float amount)
    {
        slider.maxValue = amount;
        slider.value = 0;

        fill.color = gradient.Evaluate(0f);
    }

    public void SetEXP(float exp)
    {
        slider.value = exp;

        fill.color = gradient.Evaluate(slider.value / slider.maxValue);
    }
}
