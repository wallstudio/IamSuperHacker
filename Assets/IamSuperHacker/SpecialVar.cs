using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SpecialVar : MonoBehaviour {

    public static int hight = 480;
    private Text  hi;

    public static int GetHight() {
        return hight;
    }

    // Use this for initialization
    void Start () {
        hi = GameObject.Find("Hi").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        hight=Int32.Parse( hi.text);
        Debug.Log(hi.text);
	}
}
