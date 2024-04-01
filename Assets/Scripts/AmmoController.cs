using System.Collections.Generic;
using UnityEngine;

public abstract class AmmoController : MonoBehaviour
{
    public abstract void ShootAmmo(Ammo ammo, Vector3 direction);
}