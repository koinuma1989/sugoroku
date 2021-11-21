using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentakuSareruKoma : MonoBehaviour
{
    public int selected;


    // Start is called before the first frame update
    void Start()
    {
        selected = 0;
    }

    [SerializeField]
    private Vector3 speed;
    void Update()
    {
        if (selected == 1)
        {

            transform.Rotate(speed, Space.Self);
        }
    }
}
