using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject marketPanel;
    public GameObject vehicle;
    public GameObject topUI;
    public GameObject turret;

    public Button upgradeFuelCapacityButton;
    public TextMeshProUGUI requiredCoinText;
    public TextMeshProUGUI requiredBoltText;

    public Button upgradeEnginePowerButton;
    public TextMeshProUGUI requiredCoinTextEngine;
    public TextMeshProUGUI requiredBoltTextEngine;

    public Button purchaseTurretButton;
    public TextMeshProUGUI purchaseTurretButtonText;
    public TextMeshProUGUI requiredCoinTextTurret;
    public TextMeshProUGUI requiredBoltTextTurret;

    public Slider fuelCapacitySlider; 
    public Slider enginePowerSlider; 

    private CameraMovement cameraMovement;
    private CarController carController;
    private Collectible collectible;

    public TextMeshProUGUI fuelCapacityText;
    private int fuelCapacityLevel = 0;

    public TextMeshProUGUI enginePowerText;
    private int enginePowerLevel = 0;
    void Start()
    {
        topUI.SetActive(false);
        cameraMovement = FindObjectOfType<CameraMovement>();
        carController = FindObjectOfType<CarController>();
        collectible = FindObjectOfType<Collectible>();

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
        int requiredCoin = int.Parse(requiredCoinText.text);
        int requiredBolt = int.Parse(requiredBoltText.text);
        if (collectible.getCoin() >= requiredCoin && collectible.getBolt() >= requiredBolt)
        {
            fuelCapacityLevel += 1;
            fuelCapacityText.text = "Fuel Capacity Lvl. " + fuelCapacityLevel.ToString();
            requiredCoinText.text = (requiredCoin + 100).ToString();
            requiredBoltText.text = (requiredBolt + 5).ToString();
            carController.maxFuel += 5;
            fuelCapacitySlider.value += 1;
            collectible.setCoin(collectible.getCoin() - requiredCoin);
            collectible.setBolt(collectible.getBolt() - requiredBolt);
        }
        else
        {
            Debug.Log("Sources are not enough!");
        }
    }

    public void UpgradeEnginePower()
    {
        int requiredCoin = int.Parse(requiredCoinTextEngine.text);
        int requiredBolt = int.Parse(requiredBoltTextEngine.text);
        if (collectible.getCoin() >= requiredCoin && collectible.getBolt() >= requiredBolt)
        {
            enginePowerLevel += 1;
            enginePowerText.text = "Engine Power Lvl. " + enginePowerLevel.ToString();
            requiredCoinTextEngine.text = (requiredCoin + 200).ToString();
            requiredBoltTextEngine.text = (requiredBolt + 10).ToString();
            carController.maxAcceleration += 50;
            enginePowerSlider.value += 1;
            collectible.setCoin(collectible.getCoin() - requiredCoin);
            collectible.setBolt(collectible.getBolt() - requiredBolt);
        }
        else
        {
            Debug.Log("Sources are not enough!");
        }
    }

    public void purchaseTurret()
    {
        int requiredCoin = int.Parse(requiredCoinTextTurret.text);
        int requiredBolt = int.Parse(requiredBoltTextTurret.text);
        if (collectible.getCoin() >= requiredCoin && collectible.getBolt() >= requiredBolt)
        {
            requiredCoinTextTurret.text = 0.ToString();
            requiredBoltTextTurret.text = 0.ToString();
            collectible.setCoin(collectible.getCoin() - requiredCoin);
            collectible.setBolt(collectible.getBolt() - requiredBolt);
            purchaseTurretButton.interactable = false;
            purchaseTurretButtonText.text = "Purchased";
            turret.SetActive(true);
        }
        else
        {
            Debug.Log("Sources are not enough!");
        }
    }


    public void SetCoinDebug()
    {
        collectible.setCoin(collectible.getCoin() + 1000);
    }

    public void SetBoltDebug()
    {
        collectible.setBolt(collectible.getBolt() + 100);
    }
}