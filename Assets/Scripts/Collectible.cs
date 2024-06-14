using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
    private int boltCounter = 0;
    private int coinCounter = 0;
    public TextMeshProUGUI boltCounterText;
    public TextMeshProUGUI coinCounterText;

    private float lastCoinPositionX;
    private float initialPositionX;
    float currentPositionX;
    private float coinDistanceThreshold = 5f;
    private float distanceTraveledAfterCoin;
    public float totalDistanceTravelled;


    private void Start()
    {
        initialPositionX = transform.position.x;
        lastCoinPositionX = transform.position.x;
    }
    private void Update()
    {
        currentPositionX = transform.position.x;
        distanceTraveledAfterCoin = Mathf.Abs(currentPositionX - lastCoinPositionX);
        totalDistanceTravelled = Mathf.Abs(currentPositionX - initialPositionX);

        if (distanceTraveledAfterCoin >= coinDistanceThreshold)
        {
            GainCoinAtPositionX();
            lastCoinPositionX = currentPositionX;
        }
    }

    public void IncreaseBoltCount()
    {
        boltCounter++;
    }
    public void GainCoinAtPositionX()
    {
        coinCounter++;
    }

    public void setCoin(int coin)  //For debug button
    {
        coinCounter = coin;
    }

    public int getCoin()
    {
        return coinCounter;
    }

    public void setBolt(int bolt)  //For debug button
    {
        boltCounter = bolt;
    }

    public int getBolt()
    {
        return boltCounter;
    }
}
