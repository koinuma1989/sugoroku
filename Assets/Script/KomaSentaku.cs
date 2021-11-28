using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KomaSentaku : MonoBehaviour
{
    public GameObject gameManeger;
    private GameManeger gameManegerScript;
    GameObject clickedGameObject;

    public GameObject namePlate;
    public Text sentakuTitle;
    private string nowPlayerName;


    private int sentakuBanCount = -1;

    public GameObject[] komaObjList = new GameObject[7];


    private void Start()
    {
        gameManegerScript = gameManeger.GetComponent<GameManeger>();
    }

    //選択中の駒をくるくるさせる
    public void KomaKuru()
    {
        clickedGameObject = null;

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

    //選択する番を進める
    public void NowPlayerName()
    {
        sentakuBanCount = sentakuBanCount + 1;
        Debug.Log(sentakuBanCount);

        nowPlayerName = gameManegerScript.playerNameList[sentakuBanCount];
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
        namePlate.transform.position = pos;
    }

    void Update()
    {
        TitleView();

        if (Input.GetMouseButtonDown(0))
        {
            TitleView();
            KomaKuru();
            
        }
    }
}
