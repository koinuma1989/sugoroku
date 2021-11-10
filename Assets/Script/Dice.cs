using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public GameObject uiManegerObject;
    private UIManeger uiManegerScript;

    private void Start()
    {
        uiManegerScript = uiManegerObject.GetComponent<UIManeger>();
        Debug.Log(uiManegerScript);
    }

    public int DiceRoll()
    {
        int diceNum = Random.Range(1, 6);
        uiManegerScript.diceNumDisplay(diceNum);
        return diceNum;
    }
}
