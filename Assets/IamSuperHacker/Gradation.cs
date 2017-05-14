using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gradation : MonoBehaviour {

    private Image image;
    private Color color;
    public float time = 0.5f;
    private float tmpTime;

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        color = image.color;
        tmpTime = time;
    }
	
	// Update is called once per frame
	void Update () {
        if (tmpTime < 0) {
            tmpTime = time;
            color.r = Random.Range(0f, 1f);
            color.g = Random.Range(0f, 1f);
            color.b = Random.Range(0f, 1f);
        }
        Debug.Log(color);
        tmpTime -=Time.deltaTime;
        image.color = color;
	}
}
