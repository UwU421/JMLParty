using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JMLControl : MonoBehaviour
{

    public RawImage spriteRenderer;
    public Texture jamal;
    public Texture gecko;
    public Texture dada;
    public Texture coolJamal;
    public Texture mladyJamal;
    public Texture uwu;
    public Texture catboyJamal;

    private string skinData;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<RawImage>();
        ChangeSprite();
    }

    void ChangeSprite()
    {
        skinData = PlayerPrefs.GetString("skin");
        if (skinData == "jamal")
        {
            spriteRenderer.texture = jamal; 
        }
        else if (skinData == "gecko")
        {
            spriteRenderer.texture = gecko; 
        }
        else if (skinData == "dada")
        {
            spriteRenderer.texture = dada; 
        }
        else if (skinData == "coolJamal")
        {
            spriteRenderer.texture = coolJamal; 
        }
        else if (skinData == "mladyJamal")
        {
            spriteRenderer.texture = mladyJamal; 
        }
        else if (skinData == "uwu")
        {
            spriteRenderer.texture = uwu; 
        }
        else if (skinData == "catboyJamal")
        {
            spriteRenderer.texture = catboyJamal; 
        }
        else
        {
            spriteRenderer.texture = jamal; 
        }
    }
}
