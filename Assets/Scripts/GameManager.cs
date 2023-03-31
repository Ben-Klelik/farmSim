using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    /*
     * TODO
     * Add sound
     * Finish lighting
     * Add crops
     * Fix buttons
     * Auto days
     * Finish global intensity stuff
     * Polish feel
     * Polish lighting
     */

    public int curDay;
    public int money;
    private int minute;
    private int hour;
    private float timeBuffer;
    private float timeStep;

    public CropData selectedCropToPlant;

    [Header("Text Info")]
    public TextMeshProUGUI wheatSeedsCountText;
    public TextMeshProUGUI potatoSeedsCountText;
    public TextMeshProUGUI currentDayText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timeText;

    public Light2D globalLight;

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
        minute = 30;
        hour = 4;
        timeBuffer = 0;
        timeStep = 0.2f;
        // Initialize the singleton
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            print(instance);
        }
        globalLight.intensity = Mathf.Sin(hour * Mathf.PI / 24f) + 0.1f;
    }

    private void FixedUpdate()
    {
        timeBuffer += timeStep;
        while (timeBuffer > 0)
        {
            minute++;
            timeBuffer--;
        }
        if (minute >= 60)
        {
            hour = (hour + 1) % 24;
            globalLight.intensity = Mathf.Sin(hour * Mathf.PI / 24f) + 0.1f;
            minute = 0;
        }
        timeText.text = $"{hour % 12 + 1:00}:{minute % 60:00} {((hour < 12) ? "AM" : "PM")}";
    }

    private void updateGlobalIntensity(int hour)
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
        if (selectedCropToPlant.type.Equals("wheat"))
            wheatSeedsCountText.text = $"{crop.amount}";
        else if (selectedCropToPlant.type.Equals("potato"))
            potatoSeedsCountText.text = $"{crop.amount}";

        currentDayText.text = $"";
        moneyText.text = $"{money}";
    }
}
