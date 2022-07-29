using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cop : NPC
{
    [SerializeField] float turnSpeed;

    Transform playerTransform;

    [SerializeField] GunController gun;
    [SerializeField] float shootTime;
    float shootTimer = 0;

    public override void Awake()
    {
        base.Awake();
        playerTransform = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        if (IsAlive) AliveBehaviour();
        shootTimer += Time.deltaTime;
    }

    void AliveBehaviour()
    {
        // Turning
        Quaternion toRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        toRotation.x = 0;
        toRotation.z = 0;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);

        // Shooting
        Vector3 aimDirection = playerTransform.position - gun.gameObject.transform.Find("BarrelPoint").position;
        if (gun != null && shootTimer > shootTime)
        {
            gun.Shoot(aimDirection);
            shootTimer = 0;
        }
    }

    public override void Die(RaycastHit hit)
    {
        base.Die(hit);
        gun.gameObject.transform.SetParent(null);
    }


}
