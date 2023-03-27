using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class Items : MonoBehaviour
{
    public theItem[] allItems;
    [HideInInspector] public GameObject[] itemsMenu;
    [HideInInspector] public int page = 0;
    void Awake()
    {
        itemsMenu = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            itemsMenu[i] = transform.GetChild(i).gameObject;//fill up the items
        }
    }
    void Update()
    {
        for (int i = 0; i < itemsMenu.Length; i++)
        {
            itemsMenu[i].GetComponent<TextMeshPro>().text = allItems[i + 4 * page].name;//update the menus fo the items
        }
    }
    public void changePage()//change the page of items
    {
        page=(page+1)%2;
    }
}
