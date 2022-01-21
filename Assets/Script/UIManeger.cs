
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManeger : MonoBehaviour
{
    public GameObject gameManeger;
    private GameManeger gameManegerScript;

    public GameObject komaSelectObj;
    public KomaSentaku komaSelectScript;

    public KomaSentaku komaSentaku;

    //プレイヤー名バリデーション用text
    public Text minyuryokuText;

    //プレイヤー名設定パネル
    public GameObject playerNameInputPanel;

    // ダイス目
    public Text diceNumText;

    // 〇〇のターンです
    public Text turnPlayerAnounceText;

    // 駒選択パネル
    public GameObject komaSentakuPanel;

    //選択される駒
    public GameObject sentakuKomaObj;

    //順番シャッフル画面
    public GameObject junbanGamen;

    //順番画面プレイヤーテキスト
    public Text[] junbanPlayer = new Text[4];

    //順番シャッフル承諾画面
    public GameObject junbanShodakuGamen;

    // Playerの個人用ステータスパネル
    public GameObject playerKojinStatusPanel;

    // Playerの資産表示用text
    public Text playerMoney;


    private void Start()
    {
        minyuryokuText.enabled = false;
        gameManegerScript = gameManeger.GetComponent<GameManeger>();
        komaSentakuPanel.SetActive(false);
        sentakuKomaObj.SetActive(false);

        komaSelectScript = komaSelectObj.GetComponent<KomaSentaku>();


    }

    // ダイス表示
    public void DiceNumDisplay(int diceNum)
    {
        diceNumText.text = diceNum.ToString();
    }

    // ターンプレイヤー表示
    public void TurnPlayerDisplay(int turnPlayerIndex)
    {
        string playerName = gameManegerScript.playerNameList[turnPlayerIndex];
        turnPlayerAnounceText.text = playerName + "　さんのターンです";

    }

    // プレイヤー名入力画面バリデーション
    public bool ValidatePlayerNameInput(string[] playerNameList)
    {
        // 未入力validate
        foreach (string playerName in playerNameList)
        {
            if (playerName == "")
            {
                minyuryokuText.enabled = true;
                return false;
            }
            minyuryokuText.enabled = false;
        }
        return true;
    }

    //プレイヤーネーム記入画面非表示
    public void ClosePlayerNameInputPanel()
    {
        playerNameInputPanel.SetActive(false);
    }

    //駒セレクト画面表示
    public void OpenKomaSelectPanel()
    {
        junbanGamen.SetActive(false);

        komaSentakuPanel.SetActive(true);
        sentakuKomaObj.SetActive(true);
        komaSelectScript.SentakutyuPlayer();
    }

    //駒セレクト画面非表示
    public void CloseKomaSelectPanel(){
        komaSentakuPanel.SetActive(false);
        sentakuKomaObj.SetActive(false);

        gameManegerScript.StartMainGame();
    }

    //順番シャッフル承諾画面表示
    public void OpenJunbanShuffleShodaku()
    {
        junbanShodakuGamen.SetActive(true);
    }

    //順番シャッフル画面表示
    public void OpenJunbanShuffle()
    {
        junbanShodakuGamen.SetActive(false);
        junbanGamen.SetActive(true);
        JunbanPlayerNameRender();
    }

    //順番シャッフル画面プレイヤー名表示
    public void JunbanPlayerNameRender()
    {
        gameManegerScript.PlayerShuffle();
        int count = 0;
        foreach (GameObject playerObj in gameManegerScript.playerList)
        {
            junbanPlayer[count].text = playerObj.GetComponent<Player>().playerName + "さん";
            count++;
        }
    }

    //ゲーム開始時の各プレイヤーのUI生成
    public void GameStartPlayerUIRender()
    {
        playerKojinStatusPanel
    }
}
