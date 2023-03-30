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

    [Header("Text Info")]
    public TextMeshProUGUI wheatSeedsCountText;
    public TextMeshProUGUI potatoSeedsCountText;
    public TextMeshProUGUI currentDayText;
    public TextMeshProUGUI moneyText;

    public event UnityAction onNewDay;

    // Singleton
    public static GameManager instance;

    void OnEnable()
    {
        Crop.onPlantCrop += OnPlantCrop;
        Crop.onHarvestCrop += OnHarvestCrop;
    }

    void OnDisable()
    {
        Crop.onPlantCrop -= OnPlantCrop;
        Crop.onHarvestCrop -= OnHarvestCrop;
    }

    void Awake()
    {
        // Initialize the singleton
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            print(instance);
        }
    }

    // Called when we press the next day button
    public void SetNextDay()
    {
        
    }

    public void SetSelectedCrop(CropData crop)
    {
        selectedCropToPlant = crop;
    }

    //Called when a crop has been planted
    // Listening to the Crop.onPlantCrop event.
    public void OnPlantCrop(CropData crop)
    {
        crop.amount--;
        UpdateStatsText(crop);
    }

    // Called when a crop has been harvested.
    // Listening to the Crop.onCropHarvest event.
    public void OnHarvestCrop(CropData crop)
    {
        money += crop.sellPrice;
        UpdateStatsText(crop);
    }

    // Called when we want to purchase a crop
    public void PurchaseCrop (CropData crop)
    {
        if (money > crop.purchasePrice || crop.amount <= 0)
        {
            money -= crop.purchasePrice;
            crop.amount++;
            UpdateStatsText(crop);
        }
    }

    // Do we have enough crops to plant?
    public bool CanPlantCrop()
    {
        return selectedCropToPlant.amount > 0;
    }

    // Called when the buy crop button is pressed.
    public void OnBuyCropButton (CropData crop)
    {
    }

    // Update the stats text to display our current stats.
    void UpdateStatsText(CropData crop)
    {

        //statsText.text = $"Day: {curDay}\nMoney: ${money}\nCrop Inventory: {cropInventory}";
        if (selectedCropToPlant.type.Equals("wheat"))
            wheatSeedsCountText.text = $"{crop.amount}";
        else if (selectedCropToPlant.type.Equals("potato"))
            potatoSeedsCountText.text = $"{crop.amount}";

        currentDayText.text = $"{curDay}";
        moneyText.text = $"{money}";
    }
}
