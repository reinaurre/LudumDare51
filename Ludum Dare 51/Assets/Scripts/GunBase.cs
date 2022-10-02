using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunBase : MonoBehaviour
{
    public float fireRate;
    public GameObject projectile;
    public AudioClip gunfireClip;
    public AudioClip gunNameClip;

    public AudioSource gunNameAudioSource;

    private void Start()
    {
        this.gunNameAudioSource = this.gameObject.GetComponent<AudioSource>();
        this.gunNameAudioSource.clip = gunNameClip;
    }
}
