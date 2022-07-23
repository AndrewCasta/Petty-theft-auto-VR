using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour, IDamageable, IKillable
{
    public int Health { get; set; }
    public bool IsAlive { get; set; }

    public Rigidbody[] ragdollRB;
    public Animator animator;
    public CharacterController characterController;

    [SerializeField] float impactForce;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject BloodAttach;
    [SerializeField] GameObject[] BloodFX;
    public Light DirLight;


    public void Damage(RaycastHit hit, float damage)
    {
        Health--;
        if (Health < 1) Die(hit);
        BloodEffects(hit);
    }

    public virtual void Die(RaycastHit hit)
    {
        IsAlive = false;
        SetRagdoll(true);
        hit.rigidbody.AddForceAtPosition(impactForce * -hit.normal, hit.point, ForceMode.Impulse);
    }

    public void SetRagdoll(bool state)
    {
        foreach (Rigidbody rb in ragdollRB)
        {
            rb.isKinematic = !state;
        }
        animator.enabled = !state;
        characterController.enabled = !state;
    }

    int effectIdx;
    public Vector3 direction;
    private void BloodEffects(RaycastHit hit)
    {
        // Instantiate blood effect
        var effectIdx = Random.Range(0, BloodFX.Length);
        var bloodInstance = Instantiate(BloodFX[effectIdx], hit.point, Quaternion.LookRotation(hit.normal));

        var settings = bloodInstance.GetComponent<BFX_BloodSettings>();
        //settings.FreezeDecalDisappearance = InfiniteDecal;
        settings.LightIntensityMultiplier = DirLight.intensity;

        // Create blood decal
        var nearestBone = GetNearestObject(hit.transform.root, hit.point);
        if (nearestBone != null)
        {
            var attachBloodInstance = Instantiate(BloodAttach);
            var bloodT = attachBloodInstance.transform;
            bloodT.position = hit.point;
            bloodT.localRotation = Quaternion.identity;
            bloodT.localScale = Vector3.one * Random.Range(0.75f, 1.2f);
            bloodT.LookAt(hit.point + hit.normal, direction);
            bloodT.Rotate(90, 0, 0);
            bloodT.transform.parent = nearestBone;
            //Destroy(attachBloodInstance, 20);
        }

        // if (!InfiniteDecal) Destroy(instance, 20);
    }

    Transform GetNearestObject(Transform hit, Vector3 hitPos)
    {
        var closestPos = 100f;
        Transform closestBone = null;
        var childs = hit.GetComponentsInChildren<Transform>();

        foreach (var child in childs)
        {
            var dist = Vector3.Distance(child.position, hitPos);
            if (dist < closestPos)
            {
                closestPos = dist;
                closestBone = child;
            }
        }

        var distRoot = Vector3.Distance(hit.position, hitPos);
        if (distRoot < closestPos)
        {
            closestPos = distRoot;
            closestBone = hit;
        }
        return closestBone;
    }
}
