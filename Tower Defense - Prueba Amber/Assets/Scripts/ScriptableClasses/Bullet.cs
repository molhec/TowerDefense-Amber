using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Bullet")]
public class Bullet : ScriptableObject
{
    public string name;
    public float headshotDamage;
    public float bodyDamage;
    public float damageDistanceMultiplier;
}
