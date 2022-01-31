using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KomaSentaku : MonoBehaviour
{
    public GameObject gameManeger;
    private GameManeger gameManegerScript;

    public GameObject UIManeger;
    GameObject clickedGameObject;

    public GameObject namePlate;
    public Text sentakuTitle;
    private string nowPlayerName;
    public Player nowPlayer;

    public GameObject komaBottle;
    public GameObject komaCabinet;
    public GameObject komaChair;
    public GameObject komaCoffee;
    public GameObject komaLamp;
    public GameObject komaMicro;
    public GameObject komaSofer;




    private int sentakuBanCount = 0;

    public GameObject[] komaObjList = new GameObject[7];


    private void Start()
    {

    }

    //選択中の駒をくるくるさせる
    public void KomaKuru()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit))
        {
            clickedGameObject = hit.collider.gameObject;

            NamePlateView(clickedGameObject.transform.position);


            foreach (GameObject koma in komaObjList)
            {
                koma.GetComponent<SentakuSareruKoma>().selected = 0; //全部の駒の回転フラグをオフ
                koma.transform.rotation = Quaternion.Euler(-90f, 180f, 0f);
            }
            clickedGameObject.GetComponent<SentakuSareruKoma>().selected = 1; //選択された駒の回転フラグをオン
        }
    }

    //駒決定
    public void KomaKettei(GameObject koma)
    {
        switch (koma.name)
        {
            case "bottleSelect":
                koma = komaBottle;
                break;
            case "cabinetSelect":
                koma = komaCabinet;
                break;
            case "chairSelect":
                koma = komaChair;
                break;
            case "coffee-potSelect":
                koma = komaCoffee;
                break;
            case "lampSelect":
                koma = komaLamp;
                break;
            case "microwaveSelect":
                koma = komaMicro;
                break;
            case "soferSelect":
                koma = komaSofer;
                break;
        }
        nowPlayer.koma = koma;
    }

    //選択する番を進める
    public void SentakutyuPlayer()
    {
        gameManegerScript = gameManeger.GetComponent<GameManeger>();

        nowPlayer = gameManegerScript.playerList[sentakuBanCount].GetComponent<Player>();
        nowPlayerName = nowPlayer.playerName;
        TitleView();

        if (clickedGameObject != null)
        {
            KomaKettei(clickedGameObject);
            clickedGameObject.SetActive(false);
            namePlate.GetComponent<Text>().text = "";

            sentakuBanCount++;
            if (sentakuBanCount < 4)
            {
                nowPlayer = gameManegerScript.playerList[sentakuBanCount].GetComponent<Player>();
                nowPlayerName = nowPlayer.playerName;
                TitleView();
            }
            clickedGameObject = null;
        }

        //最後の人だったら駒選択画面終了
        if (sentakuBanCount > 3)
        {
            UIManeger UIManegerScript = UIManeger.GetComponent<UIManeger>();
            UIManegerScript.CloseKomaSelectPanel();
        }
    }

    //タイトル
    public void TitleView()
    {
        sentakuTitle.text = nowPlayerName + "さんの駒を選択してください";
    }

    //現在の駒選択者の名前を選択した駒の下に表示
    public void NamePlateView(Vector3 pos)
    {
        namePlate.GetComponent<Text>().text = nowPlayerName;

        namePlate.GetComponent<RectTransform>().position = RectTransformUtility.WorldToScreenPoint(Camera.main, pos);
        namePlate.GetComponent<RectTransform>().localPosition = namePlate.GetComponent<RectTransform>().localPosition - new Vector3(0, 20.0f, 0);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            KomaKuru();

        }
    }
}
