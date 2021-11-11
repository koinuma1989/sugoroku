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


    private Vector3[] onajiMasuPlayerPos;// プレイヤーが同じマスの時の居場所


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

        // プレイヤーが同じマスの時の居場所
        onajiMasuPlayerPos = new Vector3[4] {
            new Vector3(-0.2f, 0, 0.2f),
            new Vector3(0.2f, 0, 0.2f),
            new Vector3(-0.2f, 0, -0.2f),
            new Vector3(0.2f, 0, -0.2f)
        };

    }




    public void playerGaOnajiMasuNotokiIchiChosei()
    {
        int[] playerIndexList = new int[] {
            playerList[0].GetComponent<Player>().currentMasuListIndex,
            playerList[1].GetComponent<Player>().currentMasuListIndex,
            playerList[2].GetComponent<Player>().currentMasuListIndex,
            playerList[3].GetComponent<Player>().currentMasuListIndex,
        };

        int count = 0;
        List<int> onajiMasuPlayerIndex = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (playerIndexList[i] == playerIndexList[j])
                {
                    count++;
                    onajiMasuPlayerIndex.Add(j); //一緒のマスにいるプレイヤーNoを追加（自分含む）
                }
            }

            // この時点で全員同じ場所なら即終了
            if (count == 4)
            {
                for (int playerNo = 0; playerNo < 4; playerNo++)
                {
                    playerList[playerNo].transform.position = playerList[playerNo].transform.position + onajiMasuPlayerPos[playerNo];
                }
                return;
            }

            // この時点で3人同じ場所でも即終了
            if (count == 3)
            {
                foreach (int playerNo in onajiMasuPlayerIndex)
                {
                    playerList[playerNo].transform.position = playerList[playerNo].transform.position + onajiMasuPlayerPos[playerNo];
                }
                return;
            }

            // ２人一緒の時はもう一組あるかもしれないので終わらない
            if (count == 2)
            {
                foreach (int playerNo in onajiMasuPlayerIndex)
                {
                    playerList[playerNo].transform.position = playerList[playerNo].transform.position + onajiMasuPlayerPos[playerNo];
                }
            }
        }
    }

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

        if (Input.GetKeyDown(KeyCode.K))
        {
            playerGaOnajiMasuNotokiIchiChosei();

        }



        //mainCamera.transform.position = player.transform.position + new Vector3(0, 5f, 0);
        //mainCamera.transform.LookAt(player.transform.position);

    }
}
