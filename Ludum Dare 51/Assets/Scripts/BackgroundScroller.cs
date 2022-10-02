using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float baseScrollSpeed;
    public float accelerationRate;
    public GameObject mapTileObject;

    public float currentSpeed;

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
            if (GameManager.instance.isPlayerActive)
            {
                this.currentSpeed = baseScrollSpeed + (accelerationRate * GameManager.instance.elapsedAcceleration);
                bgTile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.currentSpeed);
                GameManager.instance.distanceTraveled += (this.currentSpeed / 2) * Time.fixedDeltaTime;
            }
            else
            {
                this.currentSpeed = 0;
                bgTile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            

            if (bgTile.transform.position.y > 10.8f)
            {
                destroyedTile = bgTile;
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
        GameObject bgTile = Instantiate(mapTileObject, this.transform);
        bgTile.AddComponent<Rigidbody2D>().gravityScale = 0;
        bgTile.transform.position = new Vector2(0, height * index);
        bgTile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, baseScrollSpeed + (accelerationRate * GameManager.instance.elapsedAcceleration));
        bgTile.transform.parent = this.transform;
        this.activeTiles.Add(bgTile);
    }
}
