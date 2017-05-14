using UnityEngine;
using System.Collections;

public class SearchLight : MonoBehaviour {

    public bool isOn = false;
    public GameObject child;
    public Light point = new Light();
    public Light spot = new Light();

    // Use this for initialization
    void Start () {
        child = transform.Find("Cube").gameObject;
        point = child.transform.Find("PointLight").gameObject.GetComponent<Light>();
        spot = child.transform.Find("SpotLight").gameObject.GetComponent<Light>();

        point.enabled = isOn;
        spot.enabled = isOn;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnOff() {
        if (!isOn) { isOn = true; }else { isOn = false; }
        point.enabled = isOn;
        spot.enabled = isOn;
    }
}
