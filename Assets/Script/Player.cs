using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;



public class Player : MonoBehaviour
{
    public int currentMasuListIndex; // 今現在いる場所。マス配列のindex番号

    public string playerName;

    public GameObject koma;

    public int junban;

    public int money;

    private GameObject eventManegerObj;
    private GameObject gameManegerObj;

    public Text eventText;
    public int status;


    private void Start()
    {
        currentMasuListIndex = 0; // 初期位置

        eventManegerObj = GameObject.Find("EventManeger");
        gameManegerObj = GameObject.Find("GameManeger");

        status = 0;
    }

    public void MoveStart(int diceNum)
    {
        StartCoroutine(Move(diceNum));
    }

    public IEnumerator Move(int diceNum)
    {
        // 現在の場所からダイスの数だけマス配列を一つずつ進める
        for (int diceAddIndex = currentMasuListIndex + diceNum; diceAddIndex - currentMasuListIndex >= 1;)
        {
            // 1マス進める
            currentMasuListIndex++;

            //勝利判定
            bool isVictory = false;
            if (currentMasuListIndex == 16)
            {
                currentMasuListIndex = 0;
                isVictory = true;
            }
            //// 16マスなのでindexが15を超える場合の処理
            //if (currentMasuListIndex == 16)
            //{
            //    diceAddIndex = 15 - diceAddIndex;
            //    currentMasuListIndex = 0;
            //}

            // indexから紐づくposをget
            Vector3 currentPos = MapGenerate.mapVector3Array[currentMasuListIndex];

            // 移動アニメーション、0.3fかけてcurrentPositionに移動
            koma.transform.DOLocalMove(currentPos, 0.3f);

            if (isVictory)
            {
                gameManegerObj.GetComponent<GameManeger>().Victory(playerName);
                yield break;
            }



            // 駒の移動が速すぎるので0.7f待機
            yield return new WaitForSeconds(0.7f);

        }

        //止まったマスのイベントが発生
        bool isEventStop = eventManegerObj.GetComponent<EventManager>().EventStart(currentMasuListIndex);
        gameManegerObj.GetComponent<GameManeger>().playerGaOnajiMasuNotokiIchiChosei();


        if (isEventStop)
        {
            //イベント処理終了後にちょっと待ってから次のプレイヤーターン
            yield return new WaitForSeconds(1.5f);
            NextPlayer();
        }



    }

    public void MinusMoveStart(int diceNum)
    {
        StartCoroutine(MinusMove(diceNum));
    }

    public IEnumerator MinusMove(int diceNum)
    {
        // 現在の場所からダイスの数だけマス配列を一つずつ戻る
        for (int diceAddIndex = currentMasuListIndex - diceNum; diceAddIndex - currentMasuListIndex != 0;)
        {
            // 1マス戻る
            currentMasuListIndex--;

            // indexから紐づくposをget
            Vector3 currentPos = MapGenerate.mapVector3Array[currentMasuListIndex];

            // 移動アニメーション、0.3fかけてcurrentPositionに移動
            koma.transform.DOLocalMove(currentPos, 0.3f);

            // 駒の移動が速すぎるので0.7f待機
            yield return new WaitForSeconds(0.7f);
        }

        //止まったマスのイベントが発生
        bool isEventStop = eventManegerObj.GetComponent<EventManager>().EventStart(currentMasuListIndex);
        gameManegerObj.GetComponent<GameManeger>().playerGaOnajiMasuNotokiIchiChosei();


        if (isEventStop)
        {
            //イベント処理終了後にちょっと待ってから次のプレイヤーターン
            yield return new WaitForSeconds(1.5f);
            NextPlayer();
        }

    }

    private void NextPlayer()
    {
        int nextPlayer = junban + 1;
        eventManegerObj.GetComponent<EventManager>().eventTextAnouce.SetActive(false);
        Debug.Log(junban);
        Debug.Log(nextPlayer);


        if (nextPlayer >= 4)
        {
            nextPlayer = 0;
        }
        gameManegerObj.GetComponent<GameManeger>().TurnGet(nextPlayer);
    }
}