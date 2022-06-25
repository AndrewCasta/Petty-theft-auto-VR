using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    GameObject barrelPoint;


    Animator gunAnimator;
    AudioSource gunAudio;
    [SerializeField] AudioClip gunShotSound;
    [SerializeField] ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = GetComponent<Animator>();
        gunAudio = GetComponent<AudioSource>();
        barrelPoint = transform.Find("BarrelPoint").gameObject;
    }

    public void Shoot ()
    {
        RaycastHit hit;
        if (Physics.Raycast(barrelPoint.transform.position, barrelPoint.transform.forward, out hit))
        {
            try
            {
                hit.transform.gameObject.GetComponent<HitHandler>().CallHitMethod();

            }
            catch (Exception e)
            {
                Debug.Log(e);
            } 
        }


        gunAnimator.Play("Shoot");
        gunAudio.PlayOneShot(gunShotSound);
        muzzleFlash.Play();
    }
}
