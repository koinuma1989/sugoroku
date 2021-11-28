﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManeger : MonoBehaviour
{
    public GameObject gameManeger;
    private GameManeger gameManegerScript;

    public GameObject komaSelectObj;
    public KomaSentaku komaSelectScript;


    //プレイヤー名バリデーション用text
    public Text minyuryokuText;

    //プレイヤー名設定パネル
    public GameObject playerNameInputPanel;


    // ダイス目
    public Text diceNumText;

    // 〇〇のターンです
    public Text turnPlayerAnounceText;

    // 駒選択パネル
    public GameObject komaSentakuPanel;

    //選択される駒
    public GameObject sentakuKomaObj;


    private void Start()
    {
        minyuryokuText.enabled = false;
        gameManegerScript = gameManeger.GetComponent<GameManeger>();
        komaSentakuPanel.SetActive(false);
        sentakuKomaObj.SetActive(false);

        komaSelectScript = komaSelectObj.GetComponent<KomaSentaku>();


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

    // プレイヤー名入力画面バリデーション
    public bool ValidatePlayerNameInput(string[] playerNameList)
    {
        // 未入力validate
        foreach (string playerName in playerNameList)
        {
            if (playerName == "")
            {
                minyuryokuText.enabled = true;
                return false;
            }
            minyuryokuText.enabled = false;
        }
        return true;
    }
    

    public void ClosePlayerNameInputPanel()
    {
        playerNameInputPanel.SetActive(false);
    }

    public void OpenKomaSelectPanel()
    {
        komaSentakuPanel.SetActive(true);
        sentakuKomaObj.SetActive(true);
        komaSelectScript.NowPlayerName();
        komaSelectScript.TitleView();
    }

}
