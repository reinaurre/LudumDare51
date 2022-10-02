using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Obstacle
{
    // for debugging
    public string Name;

    public GameObject prefab;
    [Range(0f, 100f)]
    public float Chance = 100f;

    [HideInInspector]
    public double _weight;
}

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public GameObject startScreen;
    public GameObject endScreen;
    public GameObject statsContainer;
    public GameObject bloodSplatterPrefab;
    public GameObject playerPrefab;
    public GameObject yetiPrefab;
    public GameObject[] yetiSpawnLocations;
    public Obstacle[] WeightedObstacles;

    public int yetiLevel;
    public bool isPlayerActive;

    public float elapsedAcceleration;

    public TMP_Text yetisKilledText;
    public TMP_Text distanceTraveledText;
    public float distanceTraveled;

    public AudioClip skiFreevengeClip;
    public AudioClip gameOverClip;
    public AudioClip playerDeathClip;

    [Space]
    public bool isDevMode;
    public bool noDeathMode;

    private int yetisKilled;
    private AudioSource audioSource;

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
        this.isPlayerActive = false;
        this.audioSource = this.gameObject.GetComponent<AudioSource>();

        this.audioSource.clip = this.skiFreevengeClip;
        this.audioSource.Play();
    }

    private void FixedUpdate()
    {
        if (this.isPlayerActive)
        {
            elapsedAcceleration += Time.fixedDeltaTime;
            this.distanceTraveledText.text = $"Distance Traveled: {this.distanceTraveled.ToString("F0")}m";
        }
    }

    public void StartGame()
    {
        this.yetisKilled = 0;
        this.yetisKilledText.text = $"Yetis Killed: {this.yetisKilled}";
        this.distanceTraveled = 0;
        this.startScreen.SetActive(false);
        this.statsContainer.SetActive(true);
        this.yetiLevel = 0;
        this.isPlayerActive = true;
        this.elapsedAcceleration = 0;
        Instantiate(this.playerPrefab);
        StartCoroutine(SpawnYeti());
    }

    public void BoostSpeed()
    {
        this.elapsedAcceleration += 4;
    }

    public void SlowPlayer(int severity)
    {
        this.elapsedAcceleration -= severity * 8;
        if (this.elapsedAcceleration <= 0)
        {
            this.elapsedAcceleration = 0;
        }
    }

    public void KillPlayer()
    {
        // kill the player;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject splatter = Instantiate(this.bloodSplatterPrefab);
        splatter.transform.position = player.transform.position;
        splatter.GetComponent<ExplosionController>().explosionClip = this.playerDeathClip;

        elapsedAcceleration = 0;
        this.isPlayerActive = false;
        Destroy(player);
        this.endScreen.SetActive(true);
        StartCoroutine(PlayGameOverAudio());
    }

    public IEnumerator PlayGameOverAudio()
    {
        yield return new WaitForSeconds(1.5f);
        this.audioSource.clip = this.gameOverClip;
        this.audioSource.Play();
    }

    public void ResetGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("Yeti"));
        this.endScreen.SetActive(false);
        this.statsContainer.SetActive(false);
        this.startScreen.SetActive(true);
        this.audioSource.clip = this.skiFreevengeClip;
        this.audioSource.Play();
    }

    public void SpawnNewYeti()
    {
        this.yetisKilled++;
        this.yetisKilledText.text = $"Yetis Killed: {this.yetisKilled}";
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
