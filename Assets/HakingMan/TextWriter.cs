using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class TextWriter : MonoBehaviour
{
    private float delay = .05f;
    private string text;
    private string currentText = "";
    private AudioSource voice;
    private Regex regex;
    // Use this for initialization
    void Awake()
    {
        //this.regex = new Regex("^[a-zA-Z0-9]*$");
        text = GetComponent<TextMeshPro>().text;
        voice = GetComponent<AudioSource>();
        //StartCoroutine(ShowText());
    }
    void OnEnable()
    {
        StartCoroutine(ShowText());
    }
    void OnDisable()
    {
        currentText = "";
    }
    IEnumerator ShowText()
    {
        for (int i = 0; i < text.Length; i++) {
            string currentLetter = text[i].ToString();
            SpeakText(currentLetter);
            if(i==0){
                GetComponent<TextMeshPro>().text="";
                yield return new WaitForSeconds(0.5f);
            }
            GetComponent<TextMeshPro>().text = currentText += currentLetter;
            yield return new WaitForSeconds(delay);
        }
    }
    void SpeakText(string currentLetter)
    {
        if (voice && regex.IsMatch(currentLetter)) {
            voice.Play();
        }
    }
}

