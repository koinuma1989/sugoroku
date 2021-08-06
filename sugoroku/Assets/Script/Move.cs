using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move : MonoBehaviour
{

    int diceNum; //ダイスの目
    public GameObject[] panels; //双六のマス
    private Vector3 currentPosition; //駒の現在の位置
    private Vector3 beforePanelPosition;//駒の直前の位置
    public GameObject arrow;//進める方向の矢印オブジェクト

    void Start()
    {
        currentPosition = this.transform.position; //駒の最初の位置をcurrentPositionに
        beforePanelPosition = new Vector3(0.1f, 0.1f, 0.1f); //最初は来た道はないので存在しない座標を初期値に

        diceNum = 7;
        MovePlayer(diceNum);
    }

    private void MovePlayer(int diceNum)
    {
        StartCoroutine(MoveCoroutine());
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

    /// <summary>
    /// 行ける方向を返す。現在の位置からx+1,x-1,z+1,z-1の座標のパネルがないかパネル配列を捜査する。
    /// あったら現在の位置から相対的にどこにあるか判定する
    /// また来た道は候補に入れないためbeforePanelPositionは捜査対象外とする。
    /// </summary>
    /// <param name="currentPosition"></param>
    /// <param name="beforePanelPosition"></param>
    /// <returns>進める方向を1~4の数字で返却</returns>
    private List<int> rootCheck(Vector3 currentPosition, Vector3 beforePanelPosition)
    {
        //返却用方向格納List
        var passable = new List<int>();

        //自身から1ずれた座標に合致するマスを全マスを対象に捜査する。
        foreach (GameObject panel in panels)
        {
            //確認対象のパネルの座標を取得
            Vector3 panelPosition = panel.transform.position;

            //beforePanelPositionと座標が同じpanelは捜査対象外
            if (beforePanelPosition == panelPosition)
            {
                continue;
            }

            //現在の場所からx+1に存在するマス
            if (panelPosition.x == currentPosition.x + 1 && panelPosition.z == currentPosition.z)
            {
                passable.Add(1);
            }
            //現在の場所からx-1に存在するマス
            else if (panelPosition.x == currentPosition.x - 1 && panelPosition.z == currentPosition.z)
            {
                passable.Add(2);
            }
            //現在の場所からz+1に存在するマス
            else if (panelPosition.z == currentPosition.z + 1 && panelPosition.x == currentPosition.x)
            {
                passable.Add(3);
            }
            //現在の場所からz-1に存在するマス
            else if (panelPosition.z == currentPosition.z - 1 && panelPosition.x == currentPosition.x)
            {
                passable.Add(4);
            }
        }

        return passable;
    }

    private void instantArrow(int i, Vector3 pos)
    {
        //矢印生成用rotation
        Quaternion rote;

        //1~4の当てはまる方向に矢印生成
        switch (i)
        {
            case 1:
                rote = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
                pos.x += 1;
                Instantiate(arrow, pos, rote);
                break;
            case 2:
                rote = new Quaternion(90.0f, 0.0f, 0.0f, 1.0f);
                pos.x -= 1;
                Instantiate(arrow, pos, rote);
                break;
            case 3:
                rote = new Quaternion(90.0f, 0.0f, 0.0f, 1.0f);
                pos.z += 1;
                Instantiate(arrow, pos, rote);
                break;
            case 4:
                rote = new Quaternion(90.0f, 0.0f, 0.0f, 1.0f);
                pos.z -= 1;
                Instantiate(arrow, pos, rote);
                break;
            default:
                break;
        }
    }

    private Vector3 getGoWay(int passableRoot)
    {
        switch (passableRoot)
        {
            case 1:
                currentPosition.x += 1;
                break;
            case 2:
                currentPosition.x -= 1;
                break;
            case 3:
                currentPosition.z += 1;
                break;
            case 4:
                currentPosition.z -= 1;
                break;
            default:
                break;
        }
        return currentPosition;
    }
}
