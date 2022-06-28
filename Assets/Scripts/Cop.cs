using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cop : MonoBehaviour, IDamageable
{
    bool isAlive;
    [SerializeField] int health;
    [SerializeField] float turnSpeed;


    Rigidbody[] ragdollRB;
    Animator animator;
    CharacterController characterController;
    [SerializeField] Transform playerTransform;

    [SerializeField] Gun gun;
    [SerializeField] float shootTime;
    float shootTimer = 0;

    [SerializeField] GameObject impactEffect;
    [SerializeField] float impactForce;




    void Awake()
    {
        playerTransform = GameObject.Find("Main Camera").transform;
        ragdollRB = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        SetRagdoll(false);
        isAlive = true;
    }

    void Update()
    {
        if (isAlive) AliveBehaviour();
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
            gun.NPCShoot(aimDirection);
            shootTimer = 0;
        }
    }

    public void Damage(RaycastHit hit, int damage)
    {
        GameObject impactParticle = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        impactParticle.transform.parent = hit.transform;
        Destroy(impactParticle, 1f);
        SetRagdoll(true);
        hit.rigidbody.AddForceAtPosition(impactForce * -hit.normal, hit.point, ForceMode.Impulse);
    }

    void SetRagdoll(bool state)
    {
        foreach (Rigidbody rb in ragdollRB)
        {
            rb.isKinematic = !state;
        }
        animator.enabled = !state;
        characterController.enabled = !state;
    }
}
