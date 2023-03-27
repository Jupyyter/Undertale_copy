using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePlayer : MonoBehaviour
{
    [SerializeField]private int dealDmg;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()!=null){
            other.GetComponent<Player>().takeDmg(dealDmg);
        }
    }
}
