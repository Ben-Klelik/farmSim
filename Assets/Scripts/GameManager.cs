using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int curDay;
    public int money;
    public CropData selectedCropToPlant;

    // Singleton
    public static GameManager instance;

    private void Awake()
    {
        // Initialize the singleton
        if(instance != null && instance != this) {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Called when we press the next day button
    public void OnPlantCrop(CropData crop)
    {
    }

    // Called when we want to purchase a crop
    public void PurchaseCrop (CropData crop)
    {
    }

    // Do we have enough crops to plant?
    public bool CanPlantCrop()
    {
        return true;
    }

    // Called when the buy crop button is pressed.
    public void OnBuyCropButton (CropData crop)
    {
    }

    // Update the stats text to display our current stats.
    void UpdateStatsText()
    {
    }
}
