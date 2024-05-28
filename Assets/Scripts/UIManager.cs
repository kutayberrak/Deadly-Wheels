using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject marketPanel;
    public GameObject vehicle;
    public GameObject topUI;
    public GameObject turret;
    public GameObject feedbackPanel;

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
    private ZombieSpawner zombieSpawner;
    //private ZombieBehaviour zombieBehaviour;

    public TextMeshProUGUI fuelCapacityText;
    private int fuelCapacityLevel = 0;

    public TextMeshProUGUI enginePowerText;
    private int enginePowerLevel = 0;


    public TextMeshProUGUI feedBackText;
    public TextMeshProUGUI subFeedBackText;
    private Vector3 initialPosition;
    void Start()
    {
        topUI.SetActive(false);
        feedbackPanel.SetActive(false);
        cameraMovement = FindObjectOfType<CameraMovement>();
        carController = FindObjectOfType<CarController>();
        collectible = FindObjectOfType<Collectible>();
        zombieSpawner = FindObjectOfType<ZombieSpawner>();
        //zombieBehaviour = FindObjectOfType<ZombieBehaviour>();

        fuelCapacityText.text = "Fuel Capacity Lvl. " + fuelCapacityLevel.ToString();
        enginePowerText.text = "Engine Power Lvl. " + enginePowerLevel.ToString();
        initialPosition = vehicle.transform.position;
    }

    

    public void StartGame()
    {
        marketPanel.SetActive(false);
        topUI.SetActive(true);
        vehicle.GetComponent<CarController>().enabled = true;
        cameraMovement.ToggleCameraMode();
        carController.currentFuel = carController.maxFuel;
        carController.fuelBar.setMaxFuel(carController.maxFuel);
        StartCoroutine(carController.DecreaseFuelOverTime());
        zombieSpawner.SpawnZombies();
    }


    public void UpgradeFuelCapacity()
    {
        if (fuelCapacityLevel < 10)
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

                if(fuelCapacityLevel == 10)
                {
                    fuelCapacityText.text = "Fuel Capacity Lvl. MAX";
                    requiredCoinText.text = "MAX";
                    requiredBoltText.text = "MAX";
                }
            }
            else
            {
                Debug.Log("Sources are not enough!");
            }
        }
    }

    public void UpgradeEnginePower()
    {
        if (enginePowerLevel < 10)
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

                if(enginePowerLevel == 10)
                {
                    enginePowerText.text = "Engine Power Lvl. MAX";
                    requiredCoinTextEngine.text = "MAX";
                    requiredBoltTextEngine.text = "MAX";
                }
            }
            else
            {
                Debug.Log("Sources are not enough!");
            }
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

    private void Update()
    {
        ShowFeedbackPanel();
        collectible.boltCounterText.text = collectible.getBolt().ToString();
        collectible.coinCounterText.text = collectible.getCoin().ToString();
    }

    public void ShowFeedbackPanel()
    {
        if (carController.isLost){
            feedbackPanel.SetActive(true);
            feedBackText.text = "You Lost";
            subFeedBackText.text = "The fuel is depleted!";
            //totalDistanceText.text = collectible.distanceTraveled.ToString() + " m";
            //killedZombieText.text = zombieBehaviour.deathCounter.ToString();
        }

        else if (carController.isWin)
        {
            feedbackPanel.SetActive(true);
            feedBackText.text = "You Won";
            subFeedBackText.text = "You successfully reached your destination!";
        } 
    }

    public void Restart()
    {
        carController.isLost = false;
        carController.isWin = false;
        vehicle.transform.position = initialPosition + new Vector3(0,1,0);
        vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
        vehicle.transform.rotation = Quaternion.Euler(0, -90, 0);
        feedbackPanel.SetActive(false);
        cameraMovement.ToggleCameraMode();
        marketPanel.SetActive(true);
        topUI.SetActive(false);
        vehicle.GetComponent<CarController>().enabled = false;
        zombieSpawner.DestroyAllPrefabs();
    }
}