using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Basic Gun controller
/// </summary>

[RequireComponent(typeof(AudioSource))]
public class GunController : MonoBehaviour, IGun
{
    public enum FireMode { Single, Auto }

    public float Damage { get => damage; set => damage = value; }
    public float damage;
    public FireMode fireMode;
    [Tooltip("Bullets per second")]
    public float rateOfFire;

    public int Ammo { get; set; }
    int ammoRemaining;

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

    /// <summary>
    /// Shoot the gun forward from it's current position.
    /// Used for player aimed shooting
    /// </summary>
    public void Shoot()
    {
        // if ammoRemaining > 0
        Vector3 targetDirection = barrelPoint.transform.forward;
        ShootAtTarget(targetDirection);
        // if ammoRemaining < 1
        // DryFire()
        ShootEffect();
    }

    /// <summary>
    /// Shoot the gun at a specific target.
    /// Used for NPC aimed shooting
    /// </summary>
    public void Shoot(Vector3 targetDirection)
    {
        ShootAtTarget(targetDirection);
        ShootEffect();
    }

    public void Reload()
    {
        ammoRemaining = Ammo;
    }

    private void ShootAtTarget(Vector3 targetDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(barrelPoint.transform.position, targetDirection, out hit))
        {
            IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(Damage, hit);
            }
        }
    }

    void ShootEffect()
    {
        if (gunAnimator != null) gunAnimator.Play("Shoot");
        gunAudio?.PlayOneShot(gunShotSound);
        muzzleFlash?.Play();
    }
}
