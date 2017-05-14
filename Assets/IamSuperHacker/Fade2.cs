using UnityEngine;
using UnityEngine.UI;

public class Fade2 : MonoBehaviour {
    
    private Image image;
    private Color color;
    public float fadeTime = 5.0f;
    public float waitTime = 0.5f;
    private bool complete = false;

    public bool fadeOut = false;

    // Use this for initialization
    void Start() {

        image = GetComponent<Image>();
        color = image.color;
        color.a = 0;

    }

    // Update is called once per frame
    void Update() {
        image.color = color;
        if (waitTime <= 0) {
            if (!complete && color.a < 0.9) {
                color.a += Time.deltaTime / fadeTime;
            } else { complete = true; }
        } else { waitTime -= Time.deltaTime; }
        if (fadeOut && complete && color.a > 0.1) {
            color.a -= Time.deltaTime / fadeTime;
        }

    }
}
