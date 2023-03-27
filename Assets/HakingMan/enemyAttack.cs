using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class enemyAttack
{
    public GameObject projectile;
    public int projectileNumber;
    public int minAngle;
    public int maxAngle;
    public float fireRate;
    public int initialBurst;
    public float speed;
    public float lerpSpeed;
    public float turnSpeed;

}
