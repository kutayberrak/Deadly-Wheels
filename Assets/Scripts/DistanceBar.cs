using UnityEngine;
using UnityEngine.UI;

public class DistanceBar : MonoBehaviour
{
    public Transform vehicle;
    public Transform target;
    public Slider slider;
    public float currentDistance;

    private float initialDistance;

    void Start()
    {
        initialDistance = Mathf.Abs(target.position.x - vehicle.position.x);
    }

    void Update()
    {
        currentDistance = Mathf.Abs(target.position.x - vehicle.position.x);
        float progress = 1f - (currentDistance / initialDistance);
        slider.value = progress;
    }
}
