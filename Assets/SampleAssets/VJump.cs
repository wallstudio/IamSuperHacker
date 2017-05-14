
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput; // CrossPlatformInputManager を含む名前空間

public class VJump : MonoBehaviour {

    public bool isOn = false;
    public string type = "";
    public int pos; 

    GUIStyle style;

    void Awake() {
        style = new GUIStyle();
        style.fontSize = 30;
    }

    void Update() {
        //バーチャルジョイスティック(Joystick.cs)が CrossPlatformInputManager に投げた水平、垂直値を取得する
        isOn = CrossPlatformInputManager.GetButton(type);
    }

    void OnGUI() {
        GUI.Label(new Rect(pos, 300, 200, 50), type+"\n"+isOn.ToString(), style);
    }

}