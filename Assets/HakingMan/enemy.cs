using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public healthBar healthbar;
    private float duration = 1.0f;
    private float timeElapsed = 0.0f;
    private int HPLeft;
    private int initialHP;
    private bool doLearp = false;
    [HideInInspector] public bool phase0;
    [HideInInspector] public bool phase1;
    [HideInInspector] public bool phase2;
    [HideInInspector] public bool alive = true;
    private SpriteRenderer sprt;
    [SerializeField] private Sprite deadSprite;
    private void Awake()
    {
        phase0 = true;
        phase1 = false;
        phase2 = false;
        initialHP = healthbar.firstHP;
    }
    private void Start()
    {
        sprt = GetComponent<SpriteRenderer>();
    }
    public void takeDmg(int DMG)
    {
        HPLeft = healthbar.HP - DMG;
        initialHP = healthbar.firstHP;
        doLearp = true;
        if (HPLeft <= 0)
        {
            sprt.sprite = deadSprite;
            alive = false;
        }
        else if (HPLeft < initialHP /2)
        {
            phase0 = false;
            phase1 = true;
        }
        else
        {
            phase0 = true;
        }
    }
    private IEnumerator deactivateHealthBar(float l)
    {
        yield return new WaitForSeconds(l);
        healthbar.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (doLearp)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);
            healthbar.HP = (int)Mathf.RoundToInt(Mathf.Lerp(healthbar.HP, HPLeft, t));
            if (HPLeft.ToString().Substring(0, 3) == healthbar.HP.ToString().Substring(0, 3))
            {//lerp hp bar
                doLearp = false;
                timeElapsed = 0.0f;
                StartCoroutine(deactivateHealthBar(0.25f));
            }
        }
    }
}
