using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    private string tileEffect; // convert to Effect to try working with it
    private Sprite tileImage;
    private AudioClip tileSound;

    private string[] tileEffects = new string[] { "effect1", "effect2", "minigame1", "minigame2" };
    private Sprite[] tileImages;
    private AudioClip[] tileSounds;

    public void InitializeTile(float posX, float posZ)
    {
        GameObject specialTile = GameObject.CreatePrimitive(PrimitiveType.Plane);
        specialTile.transform.position = new Vector3(posX, 6.25f, posZ);
        specialTile.transform.localScale = new Vector3(0.6466f, 0.6466f, 0.6466f);

        RandomizeTile();
    }

    private void RandomizeTile()
    {
        this.tileEffect = this.tileEffects[Random.Range(0, 4)];
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