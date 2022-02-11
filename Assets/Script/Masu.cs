using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masu : MonoBehaviour
{
    /*
     * 前提情報
     */

    //特殊マスかどうか
    public bool isSpecial;

    //マスの名称
    public string masuName;


    /*
     * 購入可能土地の情報
     */

    //カラーグループ
    public string color;


    /*
     * 金額関連
     */

    //販売価格
    public int price;

    //レンタル料
    public int rent;

    //家１件の時　鉄道の場合は所有枚数
    public int house1Price;

    //家２件の時
    public int house2Price;

    //家３件の時
    public int house3Price;

    //家４件の時
    public int house4Price;

    //ホテルの時
    public int hotelPrice;

    //家の建設価格
    public int houseBuildPrice;


    /*
     * ゲーム中に変更される要素
     */

    //所有者がいるか
    public bool isHave;

    //所有プレイヤー
    public string playerName;

    //抵当に入っているか
    public bool isTeito;

    //家が何件立っているか
    public int houseBuildNum;

    //ホテルが立っているか
    public bool isHotel;


    /*
     メモ：
     独占時はレンタル料２倍
     抵当に入れて手に入る金額は購入価格の半分
     抵当から戻すときは抵当で得た金額の1.1倍
     電力、水道は片方だったら出目×4、両方持ってたら出目×10
     */
     
}
