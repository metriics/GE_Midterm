using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndMenu : MonoBehaviour
{
    // references
    [SerializeField]
    public GameObject mainMenuButton;
    public TMP_Text winText;
    public TMP_Text trt;

    public TMP_Text rtbc1;
    public TMP_Text rtbc2;
    public TMP_Text rtbc3;
    public TMP_Text rtbc4;
    public TMP_Text rtbc5;
    public TMP_Text rtbc6;

    List<TMP_Text> RTBClist = new List<TMP_Text>();

    // Start is called before the first frame update
    void Start()
    {
        RTBClist.Add(rtbc1);
        RTBClist.Add(rtbc2);
        RTBClist.Add(rtbc3);
        RTBClist.Add(rtbc4);
        RTBClist.Add(rtbc5);
        RTBClist.Add(rtbc6);

        // we can use this later to load in stats from previous sessions
        Cursor.lockState = CursorLockMode.None;

        winText.text = PlayerPrefs.GetString("win");
        trt.text = trt.text + PlayerPrefs.GetFloat("TRT").ToString();

        int numActive = PlayerPrefs.GetInt("NumCheckpoints");

        for (int i = 0; i < RTBClist.Count; i++)
        {
            if (i >= numActive)
            {
                RTBClist[i].text = RTBClist[i].text + "N/A";
            }
            else
            {
                string name = "RTBC" + i.ToString();
                RTBClist[i].text = RTBClist[i].text + PlayerPrefs.GetFloat(name).ToString();
            }
        }
    }

    public void sceneMenu()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
