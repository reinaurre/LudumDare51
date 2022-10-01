using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public GameObject yetiPrefab;
    public GameObject[] yetiSpawnLocations;

    public int yetiLevel;
    public bool isPlayerActive;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.yetiLevel = 0;
        this.isPlayerActive = true;
        StartCoroutine(SpawnYeti());
    }

    public void KillPlayer()
    {
        // kill the player;

        this.isPlayerActive = false;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
    }

    public void SpawnNewYeti()
    {
        StartCoroutine(SpawnYeti());
    }

    private IEnumerator SpawnYeti()
    {
        yetiLevel++;
        yield return new WaitForSeconds(2);
        GameObject newYeti = Instantiate(yetiPrefab);
        newYeti.transform.position = yetiSpawnLocations[Random.Range(0, 4)].transform.position;
    }
}
