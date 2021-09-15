using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    [SerializeField] protected Bullet Bullet;

    public abstract void Shoot(Transform shootPoint);
}
