using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerate : MonoBehaviour
{
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
