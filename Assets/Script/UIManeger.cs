using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManeger : MonoBehaviour
{
    public Text diceNumText;

    public void diceNumDisplay(int diceNum)
    {
        diceNumText.text = diceNum.ToString();
    }
}
