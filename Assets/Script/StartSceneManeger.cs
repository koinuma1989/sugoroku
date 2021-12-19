using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManeger : MonoBehaviour
{
    //public
    public float speed = 1.0f;

    //private
    public Text text;

    private float time;

    public AudioSource startSE;
    public AudioClip startSeClip;
   
   

    private void Start()
    {
        text = text.gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnRetry();
        }

        text.color = GetAlphaColor(text.color);
    }
    public void OnRetry()
    {
        startSE.PlayOneShot(startSeClip);
        Invoke("LoadFire", 2f);
    }

    private void LoadFire()
    {
        SceneManager.LoadScene("MainScene");
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
