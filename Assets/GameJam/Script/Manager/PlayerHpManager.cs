using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpManager : MonoBehaviour
{
    private static PlayerHpManager instans;
    public static PlayerHpManager Instans { get { return instans; } }

    public Slider UpHP;
    public Slider DownHP;

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

        //UpHP.onValueChanged.AddListener(SetUpHpValue);
        //DownHP.onValueChanged.AddListener(SetDownHpValue);
    }


    public void SetUpHpValue(float value)
    {
        UpHP.value = value;
    }

    public void SetDownHpValue(float value)
    {
        DownHP.value = value;
    }
}
