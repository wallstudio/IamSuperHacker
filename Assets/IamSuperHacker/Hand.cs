using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

public class Hand : MonoBehaviour {

    private Collider col;
    public GameObject player;
    public Walk walk;
    public Scenario master;
    private bool touch;
    public Texture reticlePositiv, reticleNegativ;
    public GameObject front;
    private GameObject reticle;
    public AudioSource catchSE;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        //Debug.Log(GameObject.Find("Scenario"));
        master = GameObject.Find("Scenario").GetComponent<Scenario>();
        walk = player.GetComponent<Walk>();
        //Debug.Log(player);
        reticle = GameObject.Find("Reticle");
        catchSE = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonDown("Cancel")) {
            player.SendMessage("ChangeState");
        }
        if (walk.nowState == Walk.state.WALK) {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            //Debug.Log(ray);
            RaycastHit hit = new RaycastHit();
            touch = false;
            if (Physics.Raycast(ray, out hit, 1.5f)) {
                front = hit.collider.gameObject;
                if (hit.collider.tag == "Item" || hit.collider.tag == "Static" || hit.collider.tag == "Scanner") {
                    touch = true;
                    if (hit.collider.tag == "Item") {
                        if (CrossPlatformInputManager.GetButtonDown("Fire1")) {

                            //Debug.Log("break");
                            master.inventory.Add(hit.collider.gameObject.name);
                            Destroy(hit.collider.gameObject);
                            PlayCatchSE();
                            front = null;
                        }
                    }
                }
            }
            Debug.DrawRay(ray.origin-Vector3.down*0.0001f, ray.direction*1.5f, Color.blue, 1, true);
        }
        if (walk.nowState == Walk.state.WALK) {
            reticle.SendMessage("Swich", touch);
        }else {
            reticle.SendMessage("Disappear");
        }
    }
    /*
    void OnGUI() {
        if (walk.nowState == Walk.state.WALK) {
            Texture reticle; 
            if (touch) { reticle = reticlePositiv; } else { reticle = reticleNegativ; } //Debug.Log(reticle);
            GUI.Label(new Rect(Screen.width / 2 - 4, Screen.height / 2 - 4, 18, 18), new GUIContent(reticle));
        }
    }
    */
    public void PlayCatchSE() {
        catchSE.PlayOneShot(catchSE.clip);
    }
}
