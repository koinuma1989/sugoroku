using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomaSentaku : MonoBehaviour
{
    public GameObject gameManeger;
    private GameManeger gameManegerScript;
    GameObject clickedGameObject;

    public GameObject[] komaObjList = new GameObject[7];


    private void Start()
    {
        //gameManegerScript = gameManeger.GetComponent<GameManeger>();
    }

    //選択中の駒をくるくるさせる
    public void KomaKuru()
    {

    }
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            clickedGameObject = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                clickedGameObject = hit.collider.gameObject;
                Debug.Log(clickedGameObject);

                foreach (GameObject koma in komaObjList)
                {
                    koma.GetComponent<SentakuSareruKoma>().selected = 0; //全部の駒の回転フラグをオフ
                    koma.transform.rotation = Quaternion.Euler(-90f, 180f, 0f);
                }
                clickedGameObject.GetComponent<SentakuSareruKoma>().selected = 1; //選択された駒の回転フラグをオン
            }
            
        }
    }
}
