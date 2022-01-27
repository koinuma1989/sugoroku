using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
    public static int[] masuEventList = new int[16] //マスのイベントID
    {
        1,
        2,
        2,
        2,
        2,
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
        1,
    };

    public static string[] eventList = new string[4]
    {
        "普通マスです",
        "1回休みです",
        "もう一回ダイスを振れます",
        "ダイスを振った分だけ戻ります"
    };

    // 1辺5マスの四角いマップの生成メソッド
    public static Vector3[] Square5()
    {
        Vector3[] mapVector3Array = new Vector3[16] {
            new Vector3( 0f, 0f, 0f ),
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

        return mapVector3Array;
    }
}
