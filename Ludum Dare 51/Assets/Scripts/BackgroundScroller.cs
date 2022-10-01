using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;

    public Sprite[] backgroundTiles;

    private float height = 0.8f;
    private List<GameObject> activeTiles = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = -1; i < 14; i++)
        {
            CreateBgTile(i);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject destroyedTile = null;

        foreach(GameObject bgTile in this.activeTiles)
        {
            if (bgTile.transform.position.y > 10.8f)
            {
                destroyedTile = bgTile;
                continue;
            }
        }

        if (destroyedTile)
        {
            Destroy(this.activeTiles[this.activeTiles.FindIndex(x => x == destroyedTile)]);
            this.activeTiles.Remove(destroyedTile);
            CreateBgTile();
        }
    }

    private void CreateBgTile(int index = -1)
    {
        GameObject bgTile = Instantiate(new GameObject(), this.transform);
        bgTile.AddComponent<SpriteRenderer>().sprite = this.backgroundTiles[Random.Range(0, 9)];
        bgTile.AddComponent<Rigidbody2D>().gravityScale = 0;
        bgTile.transform.position = new Vector2(0, height * index);
        bgTile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, scrollSpeed);
        bgTile.transform.parent = this.transform;
        this.activeTiles.Add(bgTile);
    }
}
