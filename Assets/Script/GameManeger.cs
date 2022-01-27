﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManeger : MonoBehaviour
{
    public GameObject panelPrefab;
    public GameObject playerPrefab;
    public GameObject[] playerList = new GameObject[4];
    public string[] playerNameList;
    public Camera mainCamera;

    public GameObject UIManeger;
    private UIManeger UIManegerScript;

    public GameObject diceObject;
    private Dice diceScript;

    public int currentTurnPlayerIndex;


    private Vector3[] onajiMasuPlayerPos;// プレイヤーが同じマスの時の居場所

    public GameObject[] playerNameInputFieldList = new GameObject[4];

    public GameObject komaSentakuObj;

    //テスト用
    public GameObject[] testKomaList = new GameObject[4];



    void Start()
    {
        UIManegerScript = UIManeger.GetComponent<UIManeger>();

        // 4人対戦限定
        CreatePlayer(4);

        mainCamera = Camera.main;

        diceScript = diceObject.GetComponent<Dice>();

        // プレイヤーが同じマスの時の居場所
        onajiMasuPlayerPos = new Vector3[4] {
            new Vector3(-0.2f, 0, 0.2f),
            new Vector3(0.2f, 0, 0.2f),
            new Vector3(-0.2f, 0, -0.2f),
            new Vector3(0.2f, 0, -0.2f)
        };


        //テスト用
        StartMainGame();
    }

    //メインのゲーム開始
    public void StartMainGame()
    {

        Debug.Log("start");


        //mapの生成
        mapCreate();

        //駒の配置
        // テスト用 最初に各プレイヤーが駒選択しないとだが、めんどくさいのでスタブデータ
        int testCount = 0;
        foreach (GameObject koma in testKomaList)
        {
            playerList[testCount].GetComponent<Player>().koma = Instantiate(koma, Vector3.zero, Quaternion.identity).gameObject; //テスト用駒
            playerList[testCount].GetComponent<Player>().playerName = testCount.ToString();
            playerList[testCount].GetComponent<Player>().junban = testCount;


            testCount++;
        }
        playerGaOnajiMasuNotokiIchiChosei();

        //foreach (GameObject player in playerList)
        //{

        //    //本番
        //    //Instantiate(player.GetComponent<Player>().koma, Vector3.zero, Quaternion.identity);
        //}

        //各プレイヤーのUI表示
        UIManegerScript.GameStartPlayerUIRender();

        // 最初のプレイヤー
        TurnGet(0);
        

    }

    // map生成
    private void mapCreate()
    {
        foreach (Vector3 pos in MapGenerate.Square5())
        {
            Instantiate(panelPrefab, pos, Quaternion.identity);
        }
    }

    // playerの名前をインサート
    public void InsertPlayerName()
    {
        playerNameList = new string[] {
            playerNameInputFieldList[0].transform.Find("Text Area/Text").GetComponent<TextMeshProUGUI>().text,
            playerNameInputFieldList[1].transform.Find("Text Area/Text").GetComponent<TextMeshProUGUI>().text,
            playerNameInputFieldList[2].transform.Find("Text Area/Text").GetComponent<TextMeshProUGUI>().text,
            playerNameInputFieldList[3].transform.Find("Text Area/Text").GetComponent<TextMeshProUGUI>().text
        };

        // 未入力validate
        if (UIManegerScript.ValidatePlayerNameInput(playerNameList))
        {
            // player名を各playerスクリプトに記録
            int i = 0;
            foreach (GameObject playerObj in playerList)
            {
                playerObj.GetComponent<Player>().playerName = playerNameList[i];
                i++;
            }
            i = 0;

            UIManegerScript.ClosePlayerNameInputPanel();
            UIManegerScript.OpenJunbanShuffleShodaku();
        }
    }

    // Playerシャッフル
    public void PlayerShuffle()
    {
        Shuffle(playerList);
    }


    // シャッフルメソッド 
    public void Shuffle(GameObject[] pObj)
    {
        for (int i = 0; i < pObj.Length; i++)
        {
            //（説明１）現在の要素を預けておく
            GameObject temp = pObj[i];
            //（説明２）入れ替える先をランダムに選ぶ
            int randomIndex = Random.Range(0, pObj.Length);
            //（説明３）現在の要素に上書き
            pObj[i] = pObj[randomIndex];
            //（説明４）入れ替え元に預けておいた要素を与える
            pObj[randomIndex] = temp;
        }
    }

    // player生成
    public void CreatePlayer(int playerNinzu)
    {
        for (int index = 0; index < playerNinzu; index++)
        {
            playerList[index] = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }


    // とりあえずこれ動かしとけば同じマスの時とか位置を調整できる
    public void playerGaOnajiMasuNotokiIchiChosei()
    {
        int[] playerIndexList = new int[] {
            playerList[0].GetComponent<Player>().currentMasuListIndex,
            playerList[1].GetComponent<Player>().currentMasuListIndex,
            playerList[2].GetComponent<Player>().currentMasuListIndex,
            playerList[3].GetComponent<Player>().currentMasuListIndex,
        };

        // 最初に全員の位置を現在いるマスの中心に戻す
        for (int playerNo = 0; playerNo < 4; playerNo++)
        {
            playerList[playerNo].GetComponent<Player>().koma.transform.position = MapGenerate.Square5()[playerList[playerNo].GetComponent<Player>().currentMasuListIndex];
        }

        List<int> jogaiList = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            // 除外リストに登録されてたらcontinue
            if (jogaiList.IndexOf(i) != -1)
            {
                continue;
            }
            int count = 0;
            List<int> onajiMasuPlayerIndex = new List<int>();
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
                    playerList[playerNo].GetComponent<Player>().koma.transform.position = playerList[playerNo].GetComponent<Player>().koma.transform.position + onajiMasuPlayerPos[playerNo];
                }
                return;
            }

            // この時点で3人同じ場所でも即終了
            if (count == 3)
            {
                foreach (int playerNo in onajiMasuPlayerIndex)
                {
                    playerList[playerNo].GetComponent<Player>().koma.transform.position = playerList[playerNo].GetComponent<Player>().koma.transform.position + onajiMasuPlayerPos[playerNo];
                }
                return;
            }

            // ２人一緒の時はもう一組あるかもしれないので終わらない
            if (count == 2)
            {
                foreach (int playerNo in onajiMasuPlayerIndex)
                {
                    playerList[playerNo].GetComponent<Player>().koma.transform.position = playerList[playerNo].GetComponent<Player>().koma.transform.position + onajiMasuPlayerPos[playerNo];

                    // 確定したプレイヤーは除外リスト
                    jogaiList.Add(onajiMasuPlayerIndex[playerNo]);
                }
            }
        }
    }

    // 指定したプレイヤーにターンを回す
    public void TurnGet(int junban)
    {
        if (playerList[currentTurnPlayerIndex].GetComponent<Player>().status == 1) //休み判定
        {
            Debug.Log("お休み中です");
            // 一回休みであることを表示して次のプレイヤーに
            //TurnGet(junban++);
        }
        currentTurnPlayerIndex = junban;


        // ターンプレイヤーの表示
        UIManegerScript.TurnPlayerDisplay(currentTurnPlayerIndex);

    }

    // ターン終了
    public void TurnEnd()
    {

        int tmpCurrentTurnPlayerIndex = currentTurnPlayerIndex + 1; //次のプレイヤー


        if (tmpCurrentTurnPlayerIndex > 3) //一周したら最初のプレイヤーに
        {
            tmpCurrentTurnPlayerIndex = 0;
        }
        TurnGet(tmpCurrentTurnPlayerIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Player playerScript = playerList[currentTurnPlayerIndex].GetComponent<Player>();
            //TurnEnd();
        }

        mainCamera.transform.LookAt(playerList[currentTurnPlayerIndex].GetComponent<Player>().koma.transform.position);
        mainCamera.transform.position = playerList[currentTurnPlayerIndex].GetComponent<Player>().koma.transform.position + new Vector3(1f, 3f, -1f);


    }
}
