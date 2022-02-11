using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManeger : MonoBehaviour
{
    /*
     * -役割-
     * プレイヤーの生成
     * プレイヤーの所持資産（金、土地、釈放券）
     * プレイヤーの順番の管理
     * プレイヤーの生死
     * プレイヤーの現在の場所
     * 
     */

    // prefab
    public GameObject playerPrefab;

    public List<GameObject> playerList = new List<GameObject>();

    //プレイヤー数分回して生成
    public void CreatePlayer(int playerNum)
    {
        for (int i = 0; i < playerNum; i++)
        {
            playerList.Add(Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity));
        }
    }
}
