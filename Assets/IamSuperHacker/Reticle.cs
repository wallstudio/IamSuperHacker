using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Reticle : MonoBehaviour {

    private GameObject ng, po;
    private Text frontName;
    private Hand hand;

    // Use this for initialization
    void Start () {
        ng = transform.Find("ReticleNegative").gameObject;
        po = transform.Find("ReticlePositive").gameObject;
        frontName = po.transform.Find("Name").GetComponent<Text>();
        hand = GameObject.Find("MainCamera").GetComponent<Hand>();

    }
	
	// Update is called once per frame
	void Update () {
        if(hand.front != null) {
            frontName.text = Scenario.UITextConvert( hand.front.name);
        }
	}

    public void Swich(bool b) {
        ng.SetActive(!b);
        po.SetActive(b);
    }
    public void Disappear () {
        ng.SetActive(false);
        po.SetActive(false);
    }
}
