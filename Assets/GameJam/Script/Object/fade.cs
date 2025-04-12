using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fade : MonoBehaviour
{
    CanvasGroup cg;
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void fadeStart()
    {
        cg.alpha = 0;

        StartCoroutine(fad());

    }

    IEnumerator fad()
    {
        for (int i = 0; 1 < 100; i++)
        {
            cg.alpha = i / 100;
            yield return null;  
        }
    }
}
