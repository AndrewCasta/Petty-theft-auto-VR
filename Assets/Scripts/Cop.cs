using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cop : MonoBehaviour, IDamageable
{
    Rigidbody[] ragdollRB;
    Animator animator;
    CharacterController characterController;

    [SerializeField] GameObject impactEffect;
    [SerializeField] float impactForce;

    [SerializeField] int health;


    void Awake()
    {
        ragdollRB = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        SetRagdoll(false);
    }

    void Update()
    {
        
    }

    public void Damage(RaycastHit hit, int damage)
    {
        GameObject impactParticle = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
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
