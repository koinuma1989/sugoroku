using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public static int DiceRoll()
    {
        return Random.Range(0, 6);
    }
}
