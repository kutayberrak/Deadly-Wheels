using UnityEngine;
using UnityEngine.UI;

public class NitrousBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxNitrous(float nitrous)
    {
        slider.maxValue = nitrous;
        slider.value = nitrous;
    }

    public void SetNitrous(float nitrous)
    {
        slider.value = nitrous;
    }
}
