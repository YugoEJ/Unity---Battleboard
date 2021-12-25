using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    private GameObject specialTile;

    private string tileEffect; // convert to Effect to try working with it
    //private Sprite tileImage;
    //private AudioClip tileSound;

    private string[] tileEffects = new string[] { "effect1", "effect2", "effect3", "effect4", "minigame1", "minigame2", "minigame3" };
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

    private void RandomizeTile()
    {
        int randomEffectIndex = Random.Range(0, 7);
        this.tileEffect = this.tileEffects[randomEffectIndex];

        Debug.Log("Random tile effect: " + this.tileEffect);
    }

    enum Effect // use this enum instead of tileEffects string array
    {
        Effect1,
        Effect2,
        Minigame1,
        Minigame2
    }
}