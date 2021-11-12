using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManeger : MonoBehaviour
{
    public GameObject gameManeger;
    private GameManeger gameManegerScript;


    // ダイス目
    public Text diceNumText;

    // 〇〇のターンです
    public Text turnPlayerAnounceText;


    private void Start()
    {
        gameManegerScript = gameManeger.GetComponent<GameManeger>();
    }

    // ダイス表示
    public void DiceNumDisplay(int diceNum)
    {
        diceNumText.text = diceNum.ToString();
    }

    // ターンプレイヤー表示
    public void TurnPlayerDisplay(int turnPlayerIndex)
    {
        string playerName = gameManegerScript.playerNameList[turnPlayerIndex];
        turnPlayerAnounceText.text = playerName + "　さんのターンです";

    }

}
