using UnityEngine;
using UnitySampleAssets.CrossPlatformInput; // CrossPlatformInputManager を含む名前空間

public class VirtualJoystick : MonoBehaviour {

    float x;
    float y;

    GUIStyle style;

    void Awake() {
        style = new GUIStyle();
        style.fontSize = 30;
    }

    void Update() {
        // バーチャルジョイスティック(Joystick.cs)が CrossPlatformInputManager に投げた水平、垂直値を取得する
        x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        y = CrossPlatformInputManager.GetAxisRaw("Vertical");
    }

    void OnGUI() {
        GUI.Label(new Rect(400, 300, 200, 50), "x : " + x, style);
        GUI.Label(new Rect(400, 350, 200, 50), "y : " + y, style);
    }
}