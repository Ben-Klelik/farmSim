using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crop : MonoBehaviour
{
    private CropData curCrop;
    private int plantDay;
    private int daysSinceLastWatered;

    public SpriteRenderer sr;

    public static event UnityAction<CropData> onPlantCrop;
    public static event UnityAction<CropData> onHarvestCrop;

    // Returns the number of days that the crop has been planted for.
    int CropProgress()
    {
        return GameManager.instance.curDay - plantDay;
    }

    // Can we currently harvest the crop?
    public bool CanHarvest()
    {
        return CropProgress() >= curCrop.daysToGrow;
    }
}
