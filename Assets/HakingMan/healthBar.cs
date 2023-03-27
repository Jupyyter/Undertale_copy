using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    private Slider slider;
    public int HP;
    [HideInInspector]public int firstHP;
    private void Awake() {
        firstHP=HP;
    }
    void Start()
    {
        slider=GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value=HP;
    }
}
