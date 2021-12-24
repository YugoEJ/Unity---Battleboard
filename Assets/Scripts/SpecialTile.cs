using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    private string tileEffect;
    private Sprite tileImage;
    private AudioClip tileSound;

    private string[] tileEffects;
    private Sprite[] tileImages;
    private AudioClip[] tileSounds;

    private void Start()
    {
        tileEffects = new string[] { "effect1", "effect2", "minigame1", "minigame2"};
    }
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
}