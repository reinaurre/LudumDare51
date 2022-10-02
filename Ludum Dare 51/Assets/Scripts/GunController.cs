using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GunBase currentGun;
    public GunBase[] guns;

    private float elapsedTime;
    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.instance.isDevMode)
        {
            StartCoroutine(SwitchGuns());
        }
        else
        {
            DevSwitchGuns();
        }

        this.elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.elapsedTime += Time.deltaTime;

        if (this.elapsedTime >= this.currentGun.fireRate)
        {
            GameObject projectile = GameObject.Instantiate(this.currentGun.projectile);
            projectile.transform.position = this.transform.position;
            this.elapsedTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.DevSwitchGuns();
        }
    }

    private IEnumerator SwitchGuns()
    {
        yield return new WaitForSeconds(10);
        GunBase previous = currentGun;
        while (currentGun == previous)
        {
            currentGun = GetNewGun();
        }

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

    }

    private GunBase GetNewGun()
    {
        this.currentIndex = Random.Range(0, guns.Length);
        return guns[this.currentIndex];
    }
}
