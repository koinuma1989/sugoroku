using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    private int currentTurnPlayerIndex;


    private Vector3[] onajiMasuPlayerPos;// プレイヤーが同じマスの時の居場所

    public InputField[] playerNameInputFieldList = new InputField[4];

    void Start()
    {
        UIManegerScript = UIManeger.GetComponent<UIManeger>();

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

        // 最初のプレイヤー
        //TurnGet(0);


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
            playerNameInputFieldList[0].text,
            playerNameInputFieldList[1].text,
            playerNameInputFieldList[2].text,
            playerNameInputFieldList[3].text
        };

        // 未入力validate
        if (UIManegerScript.ValidatePlayerNameInput(playerNameList))
        {
            UIManegerScript.ClosePlayerNameInputPanel();
            UIManegerScript.OpenKomaSelectPanel();
        }

    }

    // player生成
    public void CreatePlayer()
    {
        // 4人対戦限定
        for (int index = 0; index < 4; index++)
        {
            playerList[index] = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            playerList[index].GetComponent<Player>().playerName = playerNameList[index];
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
            playerList[playerNo].transform.position = MapGenerate.Square5()[playerList[playerNo].GetComponent<Player>().currentMasuListIndex];
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

                    // 確定したプレイヤーは除外リスト
                    jogaiList.Add(onajiMasuPlayerIndex[playerNo]);
                }
            }
        }
    }

    // 指定したプレイヤーにターンを回す
    public void TurnGet(int getTurnPlayerIndex)
    {

        currentTurnPlayerIndex = getTurnPlayerIndex;


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
            Player playerScript = playerList[currentTurnPlayerIndex].GetComponent<Player>();
            StartCoroutine(playerScript.Move(diceScript.DiceRoll()));
            TurnEnd();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            playerGaOnajiMasuNotokiIchiChosei();

        }

        //mainCamera.transform.LookAt(playerList[currentTurnPlayerIndex].transform.position);
        //mainCamera.transform.position = playerList[currentTurnPlayerIndex].transform.position + new Vector3(0, 5f, 0);

    }
}
