using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    //マップ
    public GameObject[] mapList = new GameObject[40];

    public static int[] masuEventList = new int[16] //マスのイベントID
    {
        0,
        2,
        0,
        1,
        0,
        0,
        0,
        3,
        1,
        0,
        0,
        0,
        2,
        3,
        0,
        0,
    };

    public static string[] eventList = new string[4]
    {
        "普通マスです",
        "1回休みです",
        "もう一回ダイスを振れます",
        "ダイスを振った分だけ戻ります"
    };
}
