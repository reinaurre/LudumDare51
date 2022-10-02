using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunController : MonoBehaviour
{
    public GunBase currentGun;
    public GunBase[] guns;

    private float elapsedTime;
    private int currentIndex = -1;
    private AudioSource gunfireAudioSource;

    private int gunNumber;

    // Start is called before the first frame update
    void Start()
    {
        this.gunfireAudioSource = this.gameObject.GetComponent<AudioSource>();
        this.elapsedTime = 0;
        this.gunNumber = 0;

        if (!GameManager.instance.isDevMode)
        {
            currentGun = guns[0];
            this.gunfireAudioSource.clip = currentGun.gunfireClip;
            StartCoroutine(SwitchGuns());
        }
        else
        {
            DevSwitchGuns();
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.elapsedTime += Time.deltaTime;

        if (this.elapsedTime >= this.currentGun.fireRate)
        {
            this.gunfireAudioSource.Play();
            GameObject projectile = GameObject.Instantiate(this.currentGun.projectile);
            projectile.transform.position = this.transform.position;
            this.elapsedTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.isDevMode)
        {
            this.DevSwitchGuns();
        }
    }

    private IEnumerator SwitchGuns()
    {
        yield return new WaitForSeconds(10);
        GunBase previous = currentGun;
        while (currentGun == previous || (currentGun.name == "TacticalNuke" && this.gunNumber < 3))
        {
            currentGun = GetNewGun();
        }

        this.gunNumber++;
        this.gunfireAudioSource.clip = currentGun.gunfireClip;
        currentGun.gunNameAudioSource.Play();

        StartCoroutine(SwitchGuns());
    }

    private void DevSwitchGuns()
    {
        if (currentIndex == guns.Length - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
        currentGun = guns[currentIndex];

        this.gunfireAudioSource.clip = currentGun.gunfireClip;
        currentGun.gunNameAudioSource.Play();
    }

    private GunBase GetNewGun()
    {
        this.currentIndex = Random.Range(0, guns.Length);
        return guns[this.currentIndex];
    }
}
