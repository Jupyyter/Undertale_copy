using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightAreaLine : MonoBehaviour
{
    private float speed = 1f;

    private Vector3 start;
    private Vector3 end;
    private float fraction = 0;
    private bool barStop = false;
    private Vector2 initialPosition;
    [SerializeField] private GameObject Attack;
    [SerializeField] private GameObject fightArea;

    void Start()
    {
        Vector2 fightAreaSize = fightArea.GetComponent<SpriteRenderer>().bounds.size;
        initialPosition = transform.position;
        start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        end = new Vector3(transform.position.x + fightAreaSize.x, transform.position.y, transform.position.z);
    }
    void OnDisable()
    {
        fraction = 0;
        transform.position = initialPosition;
        barStop = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            barStop = true;
            Attack.SetActive(true);
        }

        if (fraction < 1 && !barStop)
        {
            fraction += Time.deltaTime * speed;//should be 1 after 1 second
            transform.position = Vector3.Lerp(start, end, fraction);//move the line
        }
        else if(!barStop){
            Attack.GetComponent<Attack>().battleManager.AttackFinished=true;
        }
    }
}
