using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EventManager : MonoBehaviour
{
    public GameObject eventTextAnouce;
    public Text eventText;

    public GameObject gameManegerObj;

    public GameObject diceManagerObj;

    public bool EventStart(int currentMasuListIndex)
    {
        int eventId = MapGenerate.masuEventList[currentMasuListIndex];
        eventTextAnouce.SetActive(true);
        int currentPlayerId = gameManegerObj.GetComponent<GameManeger>().currentTurnPlayerIndex;

        switch (eventId)
        {
            case 0://普通マス
                eventText.text = MapGenerate.eventList[eventId];
                return true;

            case 1: //一回休み
                eventText.text = MapGenerate.eventList[eventId];
                gameManegerObj.GetComponent<GameManeger>().playerList[currentPlayerId].GetComponent<Player>().status = 1;
                return true;

            case 2: //ダイスもう一回
                eventText.text = MapGenerate.eventList[eventId];
                OneMoreDicePlus();
                return false;

            case 3: //ダイス振って戻る
                eventText.text = MapGenerate.eventList[eventId];
                OneMoreDiceMinus();
                return false;

            default:
                return false;
        }
    }

    private void OneMoreDicePlus()
    {
        diceManagerObj.GetComponent<Dice>().DiceRoll(true);
    }

    private void OneMoreDiceMinus()
    {
        diceManagerObj.GetComponent<Dice>().MinusDiceRoll(true);
    }

}
