using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    // Mapのマスprefab



    // 1辺11マスのモノポリーマップの情報[マスのpos,マスの種類,誰の所有物か,抵当かどうか,家が何件立ってるか、ホテルが何件立ってるか]
    public static Masu[] mapVector3Array = new Masu[40] {
        new Masu{pos = new Vector3(0, 0, 0),  },
        new Vector3( -1f, 0f, 0f ),
        new Vector3( -2f, 0f, 0f ),
        new Vector3( -3f, 0f, 0f ),
        new Vector3( -4f, 0f, 0f ),
        new Vector3( -4f, 0f, 1f ),
        new Vector3( -4f, 0f, 2f ),
        new Vector3( -4f, 0f, 3f ),
        new Vector3( -4f, 0f, 4f ),
        new Vector3( -3f, 0f, 4f ),
        new Vector3( -2f, 0f, 4f ),
        new Vector3( -1f, 0f, 4f ),
        new Vector3( 0f, 0f, 4f ),
        new Vector3( 0f, 0f, 3f ),
        new Vector3( 0f, 0f, 2f ),
        new Vector3( 0f, 0f, 1f )
    };

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
