using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    //private Crop curCrop;
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

    public void Interact()
    {

    }
}
