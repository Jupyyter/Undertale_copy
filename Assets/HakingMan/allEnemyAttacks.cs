using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allEnemyAttacks : MonoBehaviour
{
    public enemyAttack[] allAttacks;
    private GameObject[][] allProjectiles;
    void Start()
    {
        allProjectiles = new GameObject[allAttacks.Length][];
        for (int i = 0; i < allAttacks.Length; i++)
        {//allProjectiles[3][5] is the projectile 5 from the attack 3
            allProjectiles[i] = new GameObject[allAttacks[i].projectileNumber];
        }
    }
    public IEnumerator activateAttack(int attackIndex)
    {
        enemyAttack theAttack=allAttacks[attackIndex];
        for (int i = 0; i < theAttack.projectileNumber; i++)
        {
            yield return new WaitForSeconds(theAttack.fireRate);//wait seconds
            //instantiate projectile
            allProjectiles[attackIndex][i] = Instantiate(theAttack.projectile, transform.position, new Quaternion(0, 0, 0, 0));
            allProjectiles[attackIndex][i].GetComponent<projectileSpeed>().speed=theAttack.speed;
            allProjectiles[attackIndex][i].GetComponent<projectileSpeed>().lerpSpeed=theAttack.lerpSpeed;
            allProjectiles[attackIndex][i].GetComponent<projectileSpeed>().turnSpeed=theAttack.turnSpeed;
            int randomAngle = Random.Range(theAttack.minAngle, theAttack.maxAngle);
            if (i % 2 == 0)//add random force to projectile
            {
                allProjectiles[attackIndex][i].GetComponent<Rigidbody2D>().velocity = new Vector2(theAttack.initialBurst, randomAngle);
            }
            else
            {
                allProjectiles[attackIndex][i].GetComponent<Rigidbody2D>().velocity = new Vector2(-theAttack.initialBurst, randomAngle);
            }
        }
    }
    public void endAttack(int attackIndex)
    {//destroy all projectiles
        for (int i = 0; i < allAttacks[attackIndex].projectileNumber; i++)
        {
            Destroy(allProjectiles[attackIndex][i].gameObject);
        }
    }
}
