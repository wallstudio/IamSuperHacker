using UnityEngine;
using System.Collections;

public class MyDebug : MonoBehaviour {

    public bool onDebug = false;
    private Walk walk;
    private float defSpeed, defJumpPower;

	// Use this for initialization
	void Start () {

        walk = GameObject.Find("Player").GetComponent<Walk>();
        defSpeed = walk.speed;
        defJumpPower = walk.jumpPower;

    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown("o")) {
            swichDebug();
        }



	}
    void swichDebug() {
        onDebug = !onDebug;
        if (onDebug) {
            walk.speed = 0.1f;
            walk.jumpPower = 1500;
        }else {
            walk.speed = defSpeed;
            walk.jumpPower = defJumpPower;
        }
        transform.Find("PrintDebugMode").gameObject.SetActive(onDebug);
    }

}
