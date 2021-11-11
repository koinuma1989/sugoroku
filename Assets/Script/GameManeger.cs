using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public GameObject panelPrefab;
    public GameObject playerPrefab;
    public GameObject[] playerList = new GameObject[4];
    public string[] playerNameList;
    public Camera mainCamera;

    public GameObject diceObject;
    private Dice diceScript;

    private int currentTurnPlayer;


    void Start()
    {
        // map生成
        foreach (Vector3 pos in MapGenerate.Square5())
        {
            Instantiate(panelPrefab, pos, Quaternion.identity);
        }

        playerNameList = new string[] { "あ", "い", "う", "え" };

        // 4人対戦
        // player生成
        for (int index = 0; index < 4; index++)
        {

            playerList[index] = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            playerList[index].GetComponent<Player>().playerName = playerNameList[index];
        }

        Debug.Log(playerList[2].GetComponent<Player>().playerName);

        //playerScript = player.GetComponent<Player>();

        mainCamera = Camera.main;

        diceScript = diceObject.GetComponent<Dice>();

    }


    //public void playerGaOnajiMasuNotokiIchiChosei()
    //{aa
    //    int[] playerIndexList = new int[] {
    //        playerList[0].GetComponent<Player>().currentMasuListIndex,
    //        playerList[1].GetComponent<Player>().currentMasuListIndex,
    //        playerList[2].GetComponent<Player>().currentMasuListIndex,
    //        playerList[3].GetComponent<Player>().currentMasuListIndex,
    //    };

    //}

    // 指定したプレイヤーにターンを回す
    public void turnGet(Player player)
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            diceScript.DiceRoll();
            //StartCoroutine(playerScript.Move(Dice.DiceRoll()));
        }


        //mainCamera.transform.position = player.transform.position + new Vector3(0, 5f, 0);
        //mainCamera.transform.LookAt(player.transform.position);

    }
}
