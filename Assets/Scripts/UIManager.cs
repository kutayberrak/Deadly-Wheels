using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    private Vector3 initialPosition;
    public GameObject marketPanel;
    public GameObject vehicle;
    public GameObject topUI;
    public GameObject turret;
    public GameObject feedbackPanel;
    public GameObject nitrousBar;

    private CameraMovement cameraMovement;
    private CarController carController;
    private Collectible collectible;
    private Spawner spawner;
    private TurretController turretController;
    private ZombieBehaviour zombieBehaviour;

    [Header("Upgrades")]
    public Slider fuelCapacitySlider; 
    public Slider enginePowerSlider;
    public TextMeshProUGUI fuelCapacityText;
    private int fuelCapacityLevel = 0;

    public TextMeshProUGUI enginePowerText;
    private int enginePowerLevel = 0;

    [Header("Fuel Capacity Upgrade")]
    public Button upgradeFuelCapacityButton;
    public TextMeshProUGUI requiredCoinText;
    public TextMeshProUGUI requiredBoltText;

    [Header("Engine Upgrade")]
    public Button upgradeEnginePowerButton;
    public TextMeshProUGUI requiredCoinTextEngine;
    public TextMeshProUGUI requiredBoltTextEngine;

    [Header("Turret Upgrade")]
    public Button purchaseTurretButton;
    public TextMeshProUGUI purchaseTurretButtonText;
    public TextMeshProUGUI requiredCoinTextTurret;
    public TextMeshProUGUI requiredBoltTextTurret;

    public Button purchaseAmmoButton;
    public TextMeshProUGUI ammoText;

    [Header("Nitrous Upgrade")]
    public Button purchaseNitrousButton;
    public TextMeshProUGUI purchaseNitrousButtonText;
    public TextMeshProUGUI requiredCoinTextNitrous;
    public TextMeshProUGUI requiredBoltTextNitrous;

    [Header("Feedback Panel (Statistics)")]
    public TextMeshProUGUI feedBackText;
    public TextMeshProUGUI subFeedBackText;
    public Button restartButton;
    public Button nextLevelButton;
    private int beforeCoinAmount;
    private int beforeBoltAmount;
    private int totalAttemptCount = 0;

    public TextMeshProUGUI totalDistanceText;
    public TextMeshProUGUI killedZombiesText;
    public TextMeshProUGUI attemptCountText;
    public TextMeshProUGUI gatheredCoinInAttemptText;
    public TextMeshProUGUI gatheredBoltInAttemptText;
    public TextMeshProUGUI levelText;

    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;

    void Start()
    {
        topUI.SetActive(false);
        feedbackPanel.SetActive(false);
        purchaseAmmoButton.interactable = false;
        cameraMovement = FindObjectOfType<CameraMovement>();
        carController = FindObjectOfType<CarController>();
        collectible = FindObjectOfType<Collectible>();
        spawner = FindObjectOfType<Spawner>();

        carController.enabled = false;
        
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
        spawner.SpawnPrefabs();
        totalAttemptCount += 1;

        if (zombieBehaviour != null) { 
        zombieBehaviour.deathCounter = 0;
        }

        beforeCoinAmount = collectible.getCoin();
        beforeBoltAmount = collectible.getBolt();
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
                requiredCoinText.text = (requiredCoin + 5).ToString();
                requiredBoltText.text = (requiredBolt + 1).ToString();
                carController.maxFuel += 10;
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
                requiredCoinTextEngine.text = (requiredCoin + 5).ToString();
                requiredBoltTextEngine.text = (requiredBolt + 1).ToString();
                carController.maxAcceleration += 15;
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
            purchaseAmmoButton.interactable = true;
            turretController = FindAnyObjectByType<TurretController>();
            ammoText.text = turretController.maxAmmo.ToString();
        }
        else
        {
            Debug.Log("Sources are not enough!");
        }
    }

    public void purchaseAmmo()
    {
        int requiredCoin = 100;
        int requiredBolt = 10;
        if (collectible.getCoin() >= requiredCoin && collectible.getBolt() >= requiredBolt)
        {
            collectible.setCoin(collectible.getCoin() - requiredCoin);
            collectible.setBolt(collectible.getBolt() - requiredBolt);
            turretController.maxAmmo += 5;
            turretController.currentAmmo = turretController.maxAmmo;
            ammoText.text = turretController.maxAmmo.ToString();
        }
        else
        {
            Debug.Log("Sources are not enough!");
        }
    }

    public void purchaseNitrous()
    {
        int requiredCoin = int.Parse(requiredCoinTextNitrous.text);
        int requiredBolt = int.Parse(requiredBoltTextNitrous.text);
        if (collectible.getCoin() >= requiredCoin && collectible.getBolt() >= requiredBolt)
        {
            requiredCoinTextNitrous.text = 0.ToString();
            requiredBoltTextNitrous.text = 0.ToString();
            collectible.setCoin(collectible.getCoin() - requiredCoin);
            collectible.setBolt(collectible.getBolt() - requiredBolt);
            purchaseNitrousButton.interactable = false;
            purchaseNitrousButtonText.text = "Purchased";
            nitrousBar.SetActive(true);
            carController.isNitroPurchased = true;
        }
        else
        {
            Debug.Log("Sources are not enough!");
        }
    }

    public void SetCoinDebug()
    {
        collectible.setCoin(collectible.getCoin() + 10000);
    }

    public void SetBoltDebug()
    {
        collectible.setBolt(collectible.getBolt() + 1000);
    }

    private void Update()
    {
        ShowFeedbackPanel();
        collectible.boltCounterText.text = collectible.getBolt().ToString();
        collectible.coinCounterText.text = collectible.getCoin().ToString();
        if(turretController != null)
        {
            ammoText.text = turretController.currentAmmo.ToString();
        }
    }

    private void CalculateStatistics()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        int currentCoin = collectible.getCoin();
        int currentBolt = collectible.getBolt();

        totalDistanceText.text = collectible.totalDistanceTravelled.ToString() + "m";
        killedZombiesText.text = spawner.CalculateKilledZombies().ToString();
        attemptCountText.text = totalAttemptCount.ToString();
        gatheredCoinInAttemptText.text = (currentCoin - beforeCoinAmount).ToString();
        gatheredBoltInAttemptText.text = (currentBolt - beforeBoltAmount).ToString();
        levelText.text = currentLevel + "/3";
    }

    public void ShowFeedbackPanel()
    {
        CalculateStatistics();

        if (carController.isLost){
            feedbackPanel.SetActive(true);
            feedBackText.text = "You Lost";
            subFeedBackText.text = "The fuel is depleted!";
            restartButton.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(false);
        }

        else if (carController.isWin)
        {
            feedbackPanel.SetActive(true);
            feedBackText.text = "You Won";
            restartButton.gameObject.SetActive(false); 

            if(SceneManager.GetActiveScene().buildIndex != 2)
            {
                nextLevelButton.gameObject.SetActive(true);
                subFeedBackText.text = "You successfully completed Level " + (SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                subFeedBackText.text = "Congrats!!! You reached your final destination!";
            } 
        }
    }

    public void Restart()
    {
        carController.isLost = false;
        carController.isWin = false;
        vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
        vehicle.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        foreach (var wheel in carController.wheels)
        {
            wheel.wheelCollider.motorTorque = 0;
        }

        vehicle.transform.position = initialPosition + new Vector3(0,1,0);
        
        if (vehicle.CompareTag("Police"))
        {
            vehicle.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            vehicle.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        feedbackPanel.SetActive(false);
        cameraMovement.ToggleCameraMode();
        marketPanel.SetActive(true);
        topUI.SetActive(false);
       
        spawner.DestroyAllPrefabs();
        carController.remainingNitrous = carController.nitrousDuration;
        carController.nitrousBar.SetMaxNitrous(carController.nitrousDuration);
        

        vehicle.GetComponent<CarController>().enabled = false;
        if (turretController != null)
        {
            turretController.currentAmmo = turretController.maxAmmo;
        }
    }

    public void NextLevel()
    {
        loadingScreen.SetActive(true);

        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            Application.Quit();
        }
        
    }

    IEnumerator LoadNextLevel(int level)
    {
        float duration = 3f; 
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / duration);
            loadingSlider.value = progress;
            loadingText.text = "Level " + (SceneManager.GetActiveScene().buildIndex + 1) + " is loading... " + Mathf.RoundToInt(progress * 100) + "%";
            yield return null;
        }       
        SceneManager.LoadScene(level);
    }
}