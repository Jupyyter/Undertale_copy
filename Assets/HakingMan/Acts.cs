using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
public class Acts : MonoBehaviour
{
    public theAct[] allActs;
    [HideInInspector] public GameObject[] actsMenu;
    [HideInInspector]public bool actText=false;
    private int indexOfActText;
    void Awake()
    {
        actsMenu = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            actsMenu[i] = transform.GetChild(i).gameObject;//fill up the items
        }
    }
    void Update()
    {
        if (actText)
        {//show the act text
            for (int i = 0; i < actsMenu.Length; i++)
            {
                if(i==0){
                    actsMenu[0].GetComponent<TextMeshPro>().text = allActs[indexOfActText].actText;
                }
                else{
                    actsMenu[i].GetComponent<TextMeshPro>().text = "";
                }
            }
        }
        else
        {//show all acts
            for (int i = 0; i < actsMenu.Length; i++)
            {
                actsMenu[i].GetComponent<TextMeshPro>().text = allActs[i].actName;//update the menus fo the items
            }
        }
    }
    public void showActText(int index){
        actText=true;
        indexOfActText=index;
    }
}
