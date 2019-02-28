using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour {

    public TextMeshProUGUI starText;

    private void Awake()
    {
        Screen.SetResolution(540, 960, true);
    }
    private void Start()
    {

    }

    private void Update()
    {
        starText.text = "" + PlayerPrefs.GetInt("Star");
    }

    public void LevelEasy()
    {
        SceneManager.LoadScene(1);
        
    }
    public void LevelMedium()
    {
        SceneManager.LoadScene(2);
        
    }
    public void LevelHard()
    {
        SceneManager.LoadScene(3);
       
    }
}
