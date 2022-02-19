using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    private GameObject specialTile;

    private string tileEffect;
    private string[] tileEffects = new string[] { "Extra-Life", "Skip-Turn" };

    public void InitializeTile(float posX, float posZ)
    {
        this.specialTile = GameObject.CreatePrimitive(PrimitiveType.Plane);
        this.specialTile.name = "Special Tile";
        this.specialTile.transform.position = new Vector3(posX, 6.25f, posZ);
        this.specialTile.transform.localScale = new Vector3(0.6466f, 0.6466f, 0.6466f);

        RandomizeTile();
    }

    public void InitEmptyTile()
    {
        this.specialTile = GameObject.CreatePrimitive(PrimitiveType.Plane);
        this.specialTile.SetActive(false);
        this.specialTile.name = "";
        this.tileEffect = "None";
    }

    private void RandomizeTile()
    {
        var tile = specialTile.GetComponent<Renderer>();

        if (Random.Range(0, 7) <= 4)
        {
            Color colorGreen = new Color32(153, 226, 79, 255);
            this.tileEffect = this.tileEffects[0];
            tile.material.SetColor("_Color", colorGreen);
        }
        else
        {
            Color colorRed = new Color32(196, 41, 41, 255);
            this.tileEffect = this.tileEffects[1];
            tile.material.SetColor("_Color", colorRed);
        }

        Debug.Log("Random tile effect: " + this.tileEffect);
    }

    public string GetTileEffect()
    {
        return this.tileEffect;
    }

}