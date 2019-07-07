using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public GameObject projectile;
    public Transform source;

    public void Shoot()
    {
        Instantiate(projectile, source.position, Quaternion.identity);
    }
}
