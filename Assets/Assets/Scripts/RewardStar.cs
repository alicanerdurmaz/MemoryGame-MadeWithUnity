using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardStar : MonoBehaviour {

    public static RewardStar instance;

    Button m_Button;
    private string gameId = "1682030";
    public string placementId = "rewardedVideo";
    private GameObject button;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    // Use this for initialization
    void Start () {

        gameId = "1682030";
        m_Button = GetComponent<Button>();
        if (m_Button) m_Button.onClick.AddListener(ShowAd);

       
        Advertisement.Initialize(gameId, true);
    }
	
	// Update is called once per frame
	void Update () {

        if (m_Button) m_Button.interactable = Advertisement.IsReady(placementId);
               
    }
    public void ShowAd()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show(placementId, options);
    }
    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PlayerPrefsHelper.instance.IncInt("Star");
        }
        else if (result == ShowResult.Skipped)
        {
            

        }
        else if (result == ShowResult.Failed)
        {
            
        }
    }
    public void ShowAdForTime()
    {
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult2;

        Advertisement.Show(placementId, options);
    }
    void HandleShowResult2(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            GameManager.instance.onTime = true;
            GameManager.instance.remainingTime += 60f;
            GameManager.instance.timeOnScreen += 60;
            GameManager.instance.GameOverScreen.SetActive(false);
            
        }
        else if (result == ShowResult.Skipped)
        {
            GameManager.instance.GameOverScreen.SetActive(true);
        }
        else if (result == ShowResult.Failed)
        {
            ShowAdForTime();
        }
    }


}
