using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cone0 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Quaternion targetRotation;
    [HideInInspector] public Transform playerTransform;
    private float moveSpeed;
    private float lerpSpeedValue;
    private float turnSpeedValue;
    void Start()
    {
        lerpSpeedValue=GetComponent<projectileSpeed>().lerpSpeed;
        turnSpeedValue=GetComponent<projectileSpeed>().turnSpeed;
        moveSpeed=GetComponent<projectileSpeed>().speed;
        rb = GetComponent<Rigidbody2D>();
        playerTransform = FindObjectOfType<Player>().transform;
    }
    void FixedUpdate()
    {
        // Calculate the direction towards the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        //lerp in that direction
        Vector2 lerpDirection=Vector2.Lerp(rb.velocity.normalized,direction,lerpSpeedValue*Time.deltaTime);//low to be slow
        //lerp the speed
        Vector2 lerpSpeed=Vector2.Lerp(rb.velocity,lerpDirection*moveSpeed,turnSpeedValue*Time.deltaTime);//high to be fast
        // Move the object in the direction towards the player
        rb.velocity = lerpSpeed;
        // Calculate the angle of the velocity vector
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        // Set the cone rotation based on the angle
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 270);
    }
}
