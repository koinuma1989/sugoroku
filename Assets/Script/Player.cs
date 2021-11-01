using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Player : MonoBehaviour
{
    public int currentIndex; // 今現在いる場所。マス配列のindex番号

    private void Start()
    {
        currentIndex = 0; // 初期位置
    }

    public void Move(int diceNum)
    {
        // 現在の場所からダイスの数だけマス配列を一つずつ進める
        int resultIndex = currentIndex + diceNum;
        for (int index; index < d)
        {

        }

        // 16マスなのでindexが15を超えた場合は0に戻る
        if (currentIndex + diceNum > 15)
        {
            currentIndex = 0;
        }
    }
    private IEnumerator MoveCoroutine()
    {
        for (int i = 0; i < diceNum; i++)
        {
            //進める方向がどの方向にいくつあるかチェック
            var passableRoot = rootCheck(currentPosition, beforePanelPosition);

            //進める道が複数ある場合
            if (passableRoot.Count > 1)
            {
                foreach (int v in passableRoot)
                {
                    instantArrow(v, currentPosition);
                }

                //道を選択するまで待機
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            }

            //今いる場所を来た場所として記録、マスの座標とずれてしまうので高さは0に
            beforePanelPosition = new Vector3(currentPosition.x, 0, currentPosition.z);

            //データが一つしかないはず
            currentPosition = getGoWay(passableRoot[0]);

            //移動アニメーション、0.3かけてcurrentPositionに移動
            transform.DOLocalMove(currentPosition, 0.3f);

            //駒の移動が速すぎるので0.7f待機
            yield return new WaitForSeconds(0.7f);
        }

    }
