using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        // map生成
        foreach (Vector3 pos in MapGenerate.Square5())
        {
            Instantiate(panel, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
