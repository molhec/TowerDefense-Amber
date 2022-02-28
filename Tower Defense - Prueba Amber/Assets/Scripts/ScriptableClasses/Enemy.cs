using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy")]
public class Enemy : ScriptableObject
{
    public string name;
    public float speed;
    public float health;
}
