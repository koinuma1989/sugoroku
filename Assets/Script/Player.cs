using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Player : MonoBehaviour
{
    public int currentMasuListIndex; // 今現在いる場所。マス配列のindex番号

    public string playerName;

    private void Start()
    {
        currentMasuListIndex = 0; // 初期位置
    }

    public IEnumerator Move(int diceNum)
    {
        // 現在の場所からダイスの数だけマス配列を一つずつ進める
        for (int diceAddIndex = currentMasuListIndex + diceNum; diceAddIndex - currentMasuListIndex >= 1;)
        {
            // 1マス進める
            currentMasuListIndex++;

            // 16マスなのでindexが15を超える場合の処理
            if (currentMasuListIndex == 16)
            {
                diceAddIndex = 15 - diceAddIndex;
                currentMasuListIndex = 0;
            }

            // indexから紐づくposをget
            Vector3 currentPos = MapGenerate.Square5()[currentMasuListIndex];


            // 移動アニメーション、0.3fかけてcurrentPositionに移動
            transform.DOLocalMove(currentPos, 0.3f);

            // 駒の移動が速すぎるので0.7f待機
            yield return new WaitForSeconds(0.7f);
        }
    }
}
