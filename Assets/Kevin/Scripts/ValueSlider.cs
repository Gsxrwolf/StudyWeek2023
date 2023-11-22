using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueSlider : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();  
    }

    public void SetValue(float _currentValue, float _maxValue)
    {
        image.fillAmount = _currentValue / _maxValue;
    }
}
