﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextScene() {
        SceneManager.LoadScene("Main_mob");
        Debug.Log("Bresk");
    }
}
