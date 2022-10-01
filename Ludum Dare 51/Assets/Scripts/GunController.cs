using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GunBase currentGun;
    public GunBase[] guns;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchGuns());
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

    private GunBase GetNewGun()
    {
        return guns[Random.Range(0, guns.Length - 1)];
    }
}
