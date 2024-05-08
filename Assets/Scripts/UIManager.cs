using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject marketPanel;
    public GameObject distanceBar;
    public GameObject fuelBar;
    public GameObject boltCounter;
    public GameObject vehicle;

    private CameraMovement cameraMovement;
    void Start()
    {
        distanceBar.SetActive(false);
        fuelBar.SetActive(false);
        boltCounter.SetActive(false);
        cameraMovement = FindObjectOfType<CameraMovement>();
    }

    public void StartGame()
    {
        marketPanel.SetActive(false);
        distanceBar.SetActive(true);
        fuelBar.SetActive(true);
        boltCounter.SetActive(true);
        vehicle.GetComponent<CarController>().enabled = true;
        cameraMovement.ToggleCameraMode();
    }
}