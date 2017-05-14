using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class CamCtler : MonoBehaviour {

    public float xRate = 1f;
    public float YRate = 1f;
    public GameObject player;
    public Walk walk;

    // Use this for initialization
    void Start () {
        player = player = GameObject.Find("Player"); ;
        walk = player.GetComponent<Walk>();
    }

    // Update is called once per frame
    void Update() {
        if (walk.nowState == Walk.state.WALK) {
            transform.Rotate(new Vector3(-CrossPlatformInputManager.GetAxis("Mouse Y"), 0, 0) * xRate);
        }
    }
}
