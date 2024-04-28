using UnityEngine;
using UnityEngine.UI;

public class DistanceBar : MonoBehaviour
{
    public Transform vehicle;
    public Transform target;
    public Slider slider;

    private float initialDistance;

    void Start()
    {
        initialDistance = Mathf.Abs(target.position.x - vehicle.position.x);
    }

    void Update()
    {
        float currentDistance = Mathf.Abs(target.position.x - vehicle.position.x);
        float progress = 1f - (currentDistance / initialDistance);
        slider.value = progress;
    }
}
