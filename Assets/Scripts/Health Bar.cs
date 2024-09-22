using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Slider Health_Bar_Slider; // slide var for controlling the slide
    [SerializeField] private Gradient Slider_Color_Gradient; // gradient for slider color
    [SerializeField] private Image Health_Bar_Fill_Image; // fill image to display slide color

    public void SetHealthSlider(int health)
    {
        Health_Bar_Slider.value = health; // set slide value to new health
        Health_Bar_Fill_Image.color = Slider_Color_Gradient.Evaluate(Health_Bar_Slider.normalizedValue); // change color using normalized slide value 
    }

}
