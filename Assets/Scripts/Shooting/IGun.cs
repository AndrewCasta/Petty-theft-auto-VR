using UnityEngine;

public interface IGun
{
    float Damage { get; set; }
    int Ammo { get; set; }

    void Shoot();
    void Reload();

}
