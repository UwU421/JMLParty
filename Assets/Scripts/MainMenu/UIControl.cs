using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    public InputField secretInput;
    public Text secretText;

    string[,] secrets = {{"jamal","jamal"},{"chemie","gecko"},{"davidek :)","dada"},{"B)","coolJamal"},{"*tips fedora*","mladyJamal"}};

    private string text;
    private bool found = false;

    public void CheckSecret()
    {
        text = secretInput.text;
        for (int i = 0; i < secrets.Length/2; i++)
        {
            Debug.Log(i);
            if (secrets[i,0] == text)
            {
                found = true;
                PlayerPrefs.SetString("skin", secrets[i,1]);
                PlayerPrefs.Save();
                break;
            }
        }
        if (found == true)
        {
            secretText.text = "Skin nastaven na " + PlayerPrefs.GetString("skin").ToString();
            Debug.Log(PlayerPrefs.GetString("skin"));
        }
        else
        {
            secretText.text = "Secret nenalezen :(";
        }
    }
}
