using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiTextManager : MonoBehaviour
{
    private static UiTextManager instans;
    public static UiTextManager Instans { get { return instans; } }

    public TMP_Text HpText;
    public TMP_Text SoulText;
    // Start is called before the first frame update
    void Start()
    {
        if (instans == null)
        {
            instans = this;
        }
        else
        {
            Destroy(instans);
        }

        SoundManager.Instance.PlaySound(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSoulText(string text)
    {
        SoulText.text = text;
    }

    public void SetHpText(string text)
    {
        HpText.text = text;
    }
}
