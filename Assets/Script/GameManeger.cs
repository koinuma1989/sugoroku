using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public GameObject panelPrefab;
    public GameObject playerPrefab;
    public GameObject player;
    public Player playerScript;
    public Camera mainCamera;

    void Start()
    {
        // map生成
        foreach (Vector3 pos in MapGenerate.Square5())
        {
            Instantiate(panelPrefab, pos, Quaternion.identity);
        }

        // player生成
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        playerScript = player.GetComponent<Player>();

        mainCamera = Camera.main;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(playerScript.Move(Dice.DiceRoll()));
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            mainCamera.transform.LookAt(new Vector3(1f, 1f, 1f));
        }
    }
}
