using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
    private int boltCounter = 0;
    public TextMeshProUGUI counterText;

    public void IncreaseBoltCount()
    {
        boltCounter++;
        counterText.text = boltCounter.ToString();
    }
}
