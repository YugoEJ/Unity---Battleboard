using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    private GameObject specialTile;

    private string tileEffect;
    //private Sprite tileImage;
    //private AudioClip tileSound;

    private string[] tileEffects = new string[] { "effect" };
    //private Sprite[] tileImages;
    //private AudioClip[] tileSounds;

    public void InitializeTile(float posX, float posZ)
    {
        specialTile = GameObject.CreatePrimitive(PrimitiveType.Plane);
        specialTile.name = "Special Tile";
        specialTile.transform.position = new Vector3(posX, 6.25f, posZ);
        specialTile.transform.localScale = new Vector3(0.6466f, 0.6466f, 0.6466f);

        RandomizeTile();
    }

    public void InitEmptyTile()
    {
        specialTile = null;
        tileEffect = "no effect";
    }

    private void RandomizeTile()
    {
        this.tileEffect = this.tileEffects[0];

        Debug.Log("Random tile effect: " + this.tileEffect);
    }

    public string GetTileEffect()
    {
        return this.tileEffect;
    }

}