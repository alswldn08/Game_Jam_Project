using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimetion : MonoBehaviour
{
    RectTransform rt;
    public float Speed;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        SoundManager.Instance.PlaySound(0);
        rt = GetComponent<RectTransform>();
        for (float i = 0; i < 1; i += Time.deltaTime / Speed)
        {
            float pos = -650;
            rt.anchoredPosition = Vector2.Lerp(new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y), new Vector2(pos, rt.anchoredPosition.y), i);
            yield return null;
        }
    }
}
