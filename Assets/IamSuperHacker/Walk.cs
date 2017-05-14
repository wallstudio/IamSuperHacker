using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput;

public class Walk : MonoBehaviour {

    public float speed = 0.025f;
    public float YRate = 1f;
    public float jumpPower = 300f;
    private bool isGround = false;

    public enum state {WALK,STOP}
    public state nowState = state.STOP;
    private bool endFirstState = false;
    private GameObject flotUI;
    public AudioSource jumpSE;

    // Use this for initialization
    void Start () {

        flotUI = GameObject.Find("FlotUI");
        jumpSE = transform.Find("SE").gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if(!endFirstState&& Input.anyKeyDown) {
            ChangeState();
            flotUI.transform.Find("Text").gameObject.GetComponent<Text>().text 
                = "<size=150>Pause</size>\nEscでコンテニュー";
            endFirstState = true;
        }

        if (nowState == state.WALK) {
            float speed2 = speed;
            if (CrossPlatformInputManager.GetButton("Fire3")) { speed2 *= 2; }
            //if (!isGround) { speed2 /= 3; }
            transform.Rotate(new Vector3(0, CrossPlatformInputManager.GetAxis("Mouse X") * YRate, 0), Space.World);
            transform.Translate(new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical")) * speed2, Space.Self);
            //Debug.Log(speed2);
            if (isGround) {
                if (CrossPlatformInputManager.GetButtonDown("Jump")) {
                    GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    jumpSE.PlayOneShot(jumpSE.clip);
                    isGround = false;
                }
            }
            //Debug.Log(isGround);
        }
    }

    void OnCollisionEnter() {
        isGround = true;
    }

    public void ChangeState() {
        switch (nowState) {
            case state.STOP:
                nowState = state.WALK;
#if UNITY_STANDALONE
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
#endif
                flotUI.SetActive(false);
                break;
            case state.WALK:
                nowState = state.STOP;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                flotUI.SetActive(true);
                break;
        }
    }

    public state Answer() {
        return nowState;
    }

    /*
    void OnGUI() {
        if (!endFirstState) {
            GUI.TextArea(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 5, 100, 20), "Press Any Key");
        }
    }
    */
}
