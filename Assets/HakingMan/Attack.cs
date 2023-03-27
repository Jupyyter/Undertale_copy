using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator anim;
    [SerializeField]private enemy bingChilling;
    public BattleManagementScript battleManager;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))//attack animation 1 time
        {
            bingChilling.healthbar.gameObject.SetActive(true);
            battleManager.AttackFinished=true;
            bingChilling.takeDmg(9000);
            this.gameObject.SetActive(false);
        }
    }
}
