using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cop : MonoBehaviour, IDamageable, IKillable
{
    public int Health { get; set; }
    public bool IsAlive { get; set; }

    [SerializeField] float turnSpeed;


    Rigidbody[] ragdollRB;
    Animator animator;
    CharacterController characterController;
    [SerializeField] Transform playerTransform;

    [SerializeField] GunController gun;
    [SerializeField] float shootTime;
    float shootTimer = 0;

    [SerializeField] float impactForce;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject BloodAttach;
    [SerializeField] GameObject[] BloodFX;
    [SerializeField] Light DirLight;

    void Awake()
    {
        playerTransform = GameObject.Find("Main Camera").transform;
        ragdollRB = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        SetRagdoll(false);
        IsAlive = true;
        DirLight = GameObject.Find("Directional Light").GetComponent<Light>();
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

    public void Damage(RaycastHit hit, float damage)
    {
        Health--;
        if (Health < 1) Die(hit);
        BloodEffects(hit);
    }

    public void Die(RaycastHit hit)
    {
        gun.gameObject.transform.SetParent(null);
        IsAlive = false;
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

    int effectIdx;
    public Vector3 direction;
    private void BloodEffects(RaycastHit hit)
    {
        // Instantiate blood effect
        var effectIdx = Random.Range(0, BloodFX.Length);
        var bloodInstance = Instantiate(BloodFX[effectIdx], hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(bloodInstance, 5f);

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
