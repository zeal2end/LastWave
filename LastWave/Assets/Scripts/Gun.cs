using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Bullet projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    float nextShootTime;

    public void Shoot()
    {
        if (Time.time > nextShootTime)
        {
            nextShootTime = Time.time + msBetweenShots / 1000;

            Bullet newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Bullet;
            newProjectile.SetSpeed(muzzleVelocity);
            
            

        }
    }
}
