using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {

    // Use this for initialization
    void Start () {
        transform.Translate(Vector3.forward * 30);
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void Met() {
        transform.Translate(Vector3.forward * -30);
    }
}
