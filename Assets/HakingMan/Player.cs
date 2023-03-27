using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private healthBar healthbar;
    [SerializeField] private TextMeshPro HPNR;
    private int speed = 5;
    private float vertical = 0;
    private float horizontal = 0;
    private bool invincibilityFrames = false;
    private Camera cam;
    [HideInInspector] public bool canMove = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = FindAnyObjectByType<Camera>();
    }
    void Update()
    {
        HPNR.text = healthbar.HP.ToString();
        this.horizontal = 0;
        this.vertical = 0;
        if (canMove)
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
    public void StopMoving()
    {
        this.horizontal = 0;
        this.vertical = 0;
        this.canMove = false;
    }
    public void takeDmg(int DMG)
    {
        if (!invincibilityFrames)
        {
            StartCoroutine(cam.GetComponent<Cam>().shakeCam());
            invincibilityFrames = true;
            healthbar.HP -= DMG;
            if (healthbar.HP <= 0)
            {
                Application.Quit();
            }
            StartCoroutine(stopInvincibilityFrames());
        }
    }
    public void heal(int theHeal)
    {
        healthbar.HP += theHeal;
        if (healthbar.HP > 100)
        {
            healthbar.HP = 100;
        }
    }
    private IEnumerator stopInvincibilityFrames()
    {
        yield return new WaitForSeconds(0.5f);
        invincibilityFrames = false;
    }
}
