using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput;

public class Scenario : MonoBehaviour {

    public enum Section {
        READY,  
        //ゲームの準備できた

        CONNECT,
        //オンラインを確立しろ
            //『ルーティングテーブル』を見つけ、ルーターにアクセス

        INFO,
        //目標の接続情報を入手しろ
            //【DNS】に向かい『接続情報』を入手

        HOLE,
        //目標のセキュリティホールを探せ
            //《ポートスキャナ》を作動させ【オープンポート】を見つける

        PROGRAM,
        //バックドアプログラムを探せ
            //どこかにある『バックドアプログラム』を入手する

        ENTER,
        //目標へ侵入しろ
            //【オープンポート】から目標内に入る

        BACKDOOR,
        //時間内にバックドアを仕掛けろ
            //タイマースタート、【root】にバックドアを使用

        STEAL,
        //電子通貨データを盗み出せ
            //『コイン』を見つけ出す

        CREAR,
        //クリア

        EXEPTION
        //例外
    }
    public Section nowSection = Section.READY;
    public List<string> inventory;
    private GameObject front;
    private GameObject player;
    private Text missionPanel;
    private Text inventoryPanel;
    private Hand hand;
    private GameObject path;
    public SearchLight[] scanners; 
    public int holeNo;

    void Awake() {
        int m = SpecialVar.GetHight();
        int h = m % 1000;int w = (m - h)/1000;
        Screen.SetResolution( w    , h, true, 60);
        Debug.Log("w" + w + "h" + h);
        //Screen.SetResolution((int)(h/Screen.height *Screen.width ),h, true, 60);
        //Debug.Log("w=" + h / Screen.height * Screen.width + ",h=" + h + "(w=" + Screen.width+",h="+Screen.height);
    }
    // Use this for initialization
    void Start () {

        nowSection = Section.CONNECT;
        List<string> inventory = new List<string>();
        hand = GameObject.Find("MainCamera").GetComponent<Hand>();
        player = GameObject.Find("Player");
        path = GameObject.Find("Path");
        //Debug.Log(path);
        holeNo = Random.Range((int)0, (int)5);
        scanners = new SearchLight[5];
        for (int i = 0; i < 5; i++) {
            string name = "Scanner (" + (i + 1) + ")";
            GameObject _obj = GameObject.Find(name);
            scanners[i] = _obj.GetComponent<SearchLight>();
            //Debug.Log(name); Debug.Log(_obj); Debug.Log(scanners[i]);
        }
        //Debug.Log(scanners[0]); Debug.Log(scanners[1]); Debug.Log(scanners[2]); Debug.Log(scanners[3]);
        //Debug.Log(scanners[4]); Debug.Log(holeNo); Debug.Log(scanners[holeNo].isOn);
        missionPanel = GameObject.Find("Mission").GetComponent<Text>();
        inventoryPanel = GameObject.Find("Inventory").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {

        if(player.transform.position.y < -20) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("GameOver");
        }



        missionPanel.text = "<b><size=80>Mision</size>\n  " + UITextConvert(nowSection.ToString()) + "</b>";
        string _buff = "";
        foreach (string s in inventory) {
            _buff += UITextConvert(s) + "\n  ";
        }
        //Debug.Log(missionPanel.text);
        inventoryPanel.text = "<b><size=80>Inventory</size>\n  " + _buff + "</b>";


        switch (nowSection) {
            case Section.READY:
                nowSection = Section.CONNECT;
                break;
            case Section.CONNECT:
                if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
                    if (inventory.Contains("RootTable")&& hand.front != null && hand.front.name == "Rooter") {
                        nowSection = Section.INFO;
                        hand.gameObject.SendMessage("PlayCatchSE");
                        inventory.Remove("RootTable");
                        path.SendMessage("Met");
                    }
                }
                break;
            case Section.INFO:
                if (inventory.Contains("IP")) {
                    nowSection = Section.HOLE;
                }
                break;
            case Section.HOLE:
                if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
                    if (inventory.Contains("IP") && hand.front != null && hand.front.tag == "Scanner") {
                        //Debug.Log("break");
                        hand.front.SendMessage("OnOff");
                        hand.gameObject.SendMessage("PlayCatchSE");
                    }
                }
                //Debug.Log(scanners[holeNo]);
                if (scanners[holeNo].isOn) {
                    nowSection = Section.PROGRAM;
                    GameObject _obj = scanners[holeNo].gameObject.transform.parent.Find("Barrier").gameObject;
                    //Debug.Log(_obj);
                    Destroy(_obj);
                }
                break;
            case Section.PROGRAM:
                if (inventory.Contains("Virus")) {
                    nowSection = Section.ENTER;
                }
                break;
            case Section.ENTER:
                nowSection = Section.BACKDOOR;
                break;
            case Section.BACKDOOR:
                if (CrossPlatformInputManager.GetButtonDown("Fire1")) {
                    if (inventory.Contains("Virus") && hand.front != null && hand.front.name == "Intra") {
                        nowSection = Section.STEAL;
                        inventory.Remove("Virus");
                        hand.gameObject.SendMessage("PlayCatchSE");
                    }
                }
                break;
            case Section.STEAL:
                if (inventory.Contains("Coin")) {
                    nowSection = Section.CREAR;
                }
                break;
            case Section.CREAR:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("GameClear");
                break;
            case Section.EXEPTION:
                break;
        }


    }
    /*
    void OnGUI() {
        DebugShowInventory(inventory);
        GUI.TextArea(new Rect(10, 160, 150, 50), nowSection.ToString());
    }
    */

    void DebugShowInventory(IEnumerable ie) {
        string buff = "";
        foreach (string s in ie) {
            buff += s + "\n";
        }
        GUI.TextArea(new Rect(10, 10, 150, 150), buff);
    }

    public static string UITextConvert(string s) {
        switch (s) {
            case "READY":
                return "ロード中";
            case "CONNECT":
                return "オンラインを確立しろ";
            //『ルーティングテーブル』を見つけ、ルーターにアクセス
            case "INFO":
                return "目標の接続情報を入手しろ";
            //【DNS】に向かい『接続情報』を入手
            case "HOLE":
                return "<size=48>目標のセキュリティホールを探せ</size>";
            //《ポートスキャナ》を作動させ【オープンポート】を見つける
            case "PROGRAM":
                return "バックドアプログラムを探せ";
            //どこかにある『バックドアプログラム』を入手する
            case "ENTER":
                return "目標へ侵入しろ";
            //【オープンポート】から目標内に入る
            case "BACKDOOR":
                return "時間内にバックドアを仕掛けろ";
            //タイマースタート、【root】にバックドアを使用
            case "STEAL":
                return "電子通貨データを盗み出せ";
            //『コイン』を見つけ出す
            case "CREAR":
                return "";
            //クリア
            case "EXEPTION":
                return "";
            //例外
            case "RootTable":
                return "Rooting Table";
            case "IP":
                return "接続情報";
            case "Virus":
                return "バックドア";
            case "Coin":
                return "BTCデータ";
            case "Scanner (1)":
                return "ポートスキャナA";
            case "Scanner (2)":
                return "ポートスキャナB";
            case "Scanner (3)":
                return "ポートスキャナC";
            case "Scanner (4)":
                return "ポートスキャナD";
            case "Scanner (5)":
                return "ポートスキャナE";
            case "Intra":
                return "管理者権限層";
            default:
                return s;
        }
    }
}
