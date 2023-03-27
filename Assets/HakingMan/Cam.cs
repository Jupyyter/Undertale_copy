using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private string currentState;
    private Animator anim;
    private const string IDLE_CAM = "idleCam";
    private const string SHAKE_CAM = "shakeCam";
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void ChangeCamAnimationState(string newState)
    {
        if (currentState == newState) return;//if the same animation plays
        anim.Play(newState);
        currentState = newState;
    }
    public IEnumerator shakeCam(){
        ChangeCamAnimationState(SHAKE_CAM);
        yield return new WaitForSeconds(0.5f);
        ChangeCamAnimationState(IDLE_CAM);
    }

}
