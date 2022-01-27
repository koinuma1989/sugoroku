using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dice : MonoBehaviour
{
    public GameObject uiManegerObject;
    private UIManeger uiManegerScript;

    public GameObject gameManeger;

    public GameObject diceStartBtn;
    public GameObject diceStopBtn;

    public GameObject diceGamen;

    // ダイス目
    public Text diceNumText;


    private void Start()
    {
        uiManegerScript = uiManegerObject.GetComponent<UIManeger>();
        
    }

    public void DiceRoll(bool isRandum)
    {
        if (isRandum){ //サイコロスタート
            StartCoroutine(Roll(isRandum));
            diceGamen.SetActive(true);
            diceStartBtn.SetActive(false);
            diceStopBtn.GetComponent<Button>().interactable = true;
            diceStopBtn.SetActive(true);
        }else {// サイコロストップ
            StopAllCoroutines();
            diceStopBtn.GetComponent<Button>().interactable = false;

            //決定した数字を表示
            int diceNum = Random.Range(1, 6);
            diceNumText.text = diceNum.ToString();

            //決定した数値分プレイヤーを歩かせる
            int currentTurnPlayerIndex = gameManeger.GetComponent<GameManeger>().currentTurnPlayerIndex;
            gameManeger.GetComponent<GameManeger>().playerList[currentTurnPlayerIndex].GetComponent<Player>().MoveStart(diceNum);

        }
    }

    private IEnumerator Roll(bool isRandum)
    {
        while (isRandum)
        {
            yield return null;
            diceNumText.text = Random.Range(1, 6).ToString();
        }
    }
}
