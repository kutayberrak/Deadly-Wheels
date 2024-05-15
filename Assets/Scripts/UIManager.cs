using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject marketPanel;
    public GameObject vehicle;
    public GameObject topUI;

    public Button upgradeFuelCapacityButton;
    public TextMeshProUGUI requiredCoinText;
    public TextMeshProUGUI requiredBoltText;

    public Button upgradeEnginePowerButton;
    public TextMeshProUGUI requiredCoinTextEngine;
    public TextMeshProUGUI requiredBoltTextEngine;

    public Slider fuelCapacitySlider; 
    public Slider enginePowerSlider; 

    private CameraMovement cameraMovement;
    private CarController carController;

    public TextMeshProUGUI fuelCapacityText;
    private int fuelCapacityLevel = 0;

    public TextMeshProUGUI enginePowerText;
    private int enginePowerLevel = 0;
    void Start()
    {
        topUI.SetActive(false);
        cameraMovement = FindObjectOfType<CameraMovement>();
        carController = FindObjectOfType<CarController>();

        fuelCapacityText.text = "Fuel Capacity Lvl. " + fuelCapacityLevel.ToString();
        enginePowerText.text = "Engine Power Lvl. " + enginePowerLevel.ToString();
    }

    public void StartGame()
    {
        marketPanel.SetActive(false);
        topUI.SetActive(true);
        vehicle.GetComponent<CarController>().enabled = true;
        cameraMovement.ToggleCameraMode();
    }


    public void UpgradeFuelCapacity()
    {
        fuelCapacityLevel += 1;
        fuelCapacityText.text = "Fuel Capacity Lvl. " + fuelCapacityLevel.ToString();
        int requiredCoin = int.Parse(requiredCoinText.text);
        int requiredBolt = int.Parse(requiredBoltText.text);
        requiredCoinText.text = (requiredCoin + 100).ToString();
        requiredBoltText.text = (requiredBolt + 5).ToString();
        carController.maxFuel += 5;
        fuelCapacitySlider.value += 1;
    }

    public void UpgradeEnginePower()
    {
        enginePowerLevel += 1;
        enginePowerText.text = "Engine Power Lvl. " + enginePowerLevel.ToString();
        int requiredCoin = int.Parse(requiredCoinTextEngine.text);
        int requiredBolt = int.Parse(requiredBoltTextEngine.text);
        requiredCoinTextEngine.text = (requiredCoin + 200).ToString();
        requiredBoltTextEngine.text = (requiredBolt + 10).ToString();
        carController.maxAcceleration += 50;
        enginePowerSlider.value += 1;
    }

}