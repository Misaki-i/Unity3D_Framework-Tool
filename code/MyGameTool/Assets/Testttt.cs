/****************************************************
    文件：Testttt.cs
	功能：Todo
*****************************************************/
using System.Collections.Generic;
using UnityEngine;

public class Testttt : MonoBehaviour
{
    List<float> values = new List<float>();

    private void Start()
    {
        values.Add(11.2f);
        values.Add(22f);
        values.Add(333.2f);
        values.Add(444.2f);
        values.Add(234.2f);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {

        }
    }


}
