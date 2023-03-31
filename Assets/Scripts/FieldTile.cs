using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    private Crop curCrop;
    public GameObject cropPrefab;

    public SpriteRenderer sr;
    private bool tilled;

    [Header("Sprites")]
    public Sprite grassSprite;
    public Sprite tilledSprite;
    public Sprite wateredTilledSprite;

    // Start is called before the first frame update
    void Start()
    {
        // Default tile
        sr.sprite = grassSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called every time a new day occurs.
    // Only does something if the tile contains a crop.
    void OnNewDay ()
    {
        if(!HasCrop())
        {
            tilled = false;
            sr.sprite = grassSprite;
            GameManager.instance.onNewDay -= OnNewDay;
        }
        else if(HasCrop()) 
        {
            sr.sprite = tilledSprite;
            curCrop.NewDayCheck();
        }
    }

    //Called when we interact with a tilled tile and we have crops to plant
    public void PlantNewCrop (CropData crop)
    {
        if (!tilled)
            return;
        
        curCrop = Instantiate(cropPrefab,transform).GetComponent<Crop>();
        curCrop.Plant(crop);

        GameManager.instance.onNewDay += OnNewDay;
    }

    // Called when we interact with a grass tile.
    void Till()
    {
        tilled = true;
        sr.sprite = tilledSprite;
    }

    // Called when we interact with a crop tile.
    void Water()
    {
        sr.sprite = wateredTilledSprite;

        if (HasCrop())
        {
            curCrop.Water();
        }
    }

    private bool HasCrop()
    {
        return curCrop != null;
    }

    public void Interact()
    {
        print(GameManager.instance);
        if (!tilled)
        {
            Till();
        }
        else if (!HasCrop() && GameManager.instance.CanPlantCrop())
        {
            PlantNewCrop(GameManager.instance.selectedCropToPlant);
        }
        else if (HasCrop() && curCrop.CanHarvest())
        {
            curCrop.Harvest();
        }
        else
        {
            Water();
        }
    }
}
