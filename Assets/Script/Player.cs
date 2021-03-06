using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;



public class Player : MonoBehaviour
{
    // プレイヤーの現在のマス番号
    public int currentMasu;

    //　プレイヤーのステータス関連
    public string playerName; 
    public GameObject koma;
    public int turn;
    public int money;
    public int[] assets = new int[30]; //資産、各土地と釈放権を数字で表現
    


    private void Start()
    {
        currentMasu = 0; // 初期位置
        money = 1500;
    }

    public void MoveStart(int diceNum)
    {
        StartCoroutine(Move(diceNum));
    }

    public IEnumerator Move(int diceNum)
    {
        // 現在の場所からダイスの数だけマス配列を一つずつ進める
        for (int diceAddIndex = currentMasu + diceNum; diceAddIndex - currentMasu >= 1;)
        {
            // 1マス進める
            currentMasu++;

            //勝利判定
            bool isVictory = false;
            if (currentMasu == 16)
            {
                currentMasu = 0;
                isVictory = true;
            }
            //// 16マスなのでindexが15を超える場合の処理
            //if (currentMasu == 16)
            //{
            //    diceAddIndex = 15 - diceAddIndex;
            //    currentMasu = 0;
            //}

            // indexから紐づくposをget
            Vector3 currentPos = new Vector3(0, 0, 0);
                //MapGenerate.mapVector3Array[currentMasu];

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
        bool isEventStop = eventManegerObj.GetComponent<EventManager>().EventStart(currentMasu);
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
        for (int diceAddIndex = currentMasu - diceNum; diceAddIndex - currentMasu != 0;)
        {
            // 1マス戻る
            currentMasu--;

            // indexから紐づくposをget
            Vector3 currentPos = new Vector3(0, 0, 0);
                //MapGenerate.mapVector3Array[currentMasu];

            // 移動アニメーション、0.3fかけてcurrentPositionに移動
            koma.transform.DOLocalMove(currentPos, 0.3f);

            // 駒の移動が速すぎるので0.7f待機
            yield return new WaitForSeconds(0.7f);
        }

        //止まったマスのイベントが発生
        bool isEventStop = eventManegerObj.GetComponent<EventManager>().EventStart(currentMasu);
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