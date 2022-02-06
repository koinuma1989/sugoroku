using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masu : MonoBehaviour
{
    // モノポリーの1マスの情報[マスのpos,カラーグループ,マスの名前,販売価格,レンタル料,誰の所有物か,抵当かどうか,家が何件立ってるか、ホテルが何件立ってるか]

    public Vector3 pos;
    public string color;
    public string masuName;
    public int price;
    public int rent;
    public string playerName;
    public bool isTeito;
    public int house;
    public int hotel;
}
