using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setFuel(int fuel)
    {
        slider.value = fuel;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setMaxFuel(int maxFuel)
    {
        slider.maxValue = maxFuel;
        slider.value = maxFuel;
        fill.color = gradient.Evaluate(1f);
    }
}
