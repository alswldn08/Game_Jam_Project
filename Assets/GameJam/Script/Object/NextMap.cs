using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour
{
    public GameObject fade;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int Soul = 80;
        if(collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;

            if (collision.gameObject.GetComponent<Player>().Soul > Soul)
            {
                fade.SetActive(true);
                fade.GetComponent<fade>().fadeStart();
            }
            else
            {
                fade.SetActive(true);
                fade.GetComponent<fade>().fadeStart();
            }
        }
    }

    IEnumerator next()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Stage2");
    }
}
