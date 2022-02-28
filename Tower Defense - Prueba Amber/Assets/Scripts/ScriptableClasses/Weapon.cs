using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    public string name;
    public Bullet bulletToUse;
    public float fireRate;
    public float maxDistanceToRegisterDamage;
}
