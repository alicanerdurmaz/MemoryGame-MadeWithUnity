using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject NotEnoughStar;
    public GameObject HintWarningScreen;
    public GameObject WarningScreen;
    private GameObject _firstCard = null;
    private GameObject _secondCard = null;
    public GameObject CardsTable;
    private Color color = Color.white;
    public Slider slider;

    [SerializeField]
    public GameObject GameOverScreen;
    [SerializeField]
    private GameObject WinScreen;

    public float remainingTime = 120f;
    public int timeOnScreen = 120;

    [SerializeField]
    private TextMeshProUGUI time;
    public TextMeshProUGUI levelText;
    private int level = 1;

    public Text starCount;
    public int _cardsLeft;

    private bool _canFlip = true;
    public bool onTime = true;

    [SerializeField]
    private float _timeBetweenFlips = 1.75f;

    public Score score;
    public CardSpawner cardSpawner;
    

    public bool CanFlip
    {
        get
        {
            return _canFlip;
        }

        set
        {
            _canFlip = value;
        }
    }

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

    private void Start()
    {
        color.a = 0;
        slider.maxValue = timeOnScreen;
        starCount.text = "" + PlayerPrefs.GetInt("Star");
    }

    private void Update()
    {
        if (remainingTime <= timeOnScreen && remainingTime > 0)
        {
            time.text =""+timeOnScreen;
            timeOnScreen--;
            slider.value = timeOnScreen;
        }
        if (remainingTime < 2)
        {
            GameOver();
        }
        if(onTime)
        remainingTime -= Time.deltaTime;
    }

    public void AddCard(GameObject card)
    {
        if(_firstCard == null)
        {
            _firstCard = card;
        }
        else
        {
            _secondCard = card;
            _canFlip = false;

            if (CheckIfMatch())
            {
                AudioManager.instance.Play("Score");
                _firstCard.GetComponent<Animator>().SetTrigger("CardDestroy");
                _secondCard.GetComponent<Animator>().SetTrigger("CardDestroy");
                score.UpdateScore();
                DecreaseCardCount();
                StartCoroutine(DeactivateCards());
            }
            else
            {
                StartCoroutine(FlipCards());
            }
        }
    }

    IEnumerator DeactivateCards()
    {
        yield return new WaitForSeconds(_timeBetweenFlips);

        _firstCard.GetComponent<CardControl>().cardButton.interactable = false;
        _secondCard.GetComponent<CardControl>().cardButton.interactable = false;
        _firstCard.GetComponent<Image>().color = color;
        _secondCard.GetComponent<Image>().color = color;
        Reset();

        //Destroy(_firstCard);
        //Destroy(_secondCard);
        // _firstCard.SetActive(false);
        // _secondCard.SetActive(false);

    }
    IEnumerator FlipCards()
    {
        yield return new WaitForSeconds(_timeBetweenFlips); 
        _firstCard.GetComponent<CardControl>().ChangeSide();
        _secondCard.GetComponent<CardControl>().ChangeSide();
        Reset();
    }
    public void Reset()
    {
        _firstCard = null;
        _secondCard = null;
        _canFlip = true;
    }

    public void DecreaseCardCount()
    {
        _cardsLeft -= 2;
       
        if (_cardsLeft <= 0)
        {
            AudioManager.instance.Play("LevelUpSound");
            onTime = false;
            StartCoroutine("Win");

        }
    }
    bool CheckIfMatch()
    {
        if (_firstCard.GetComponent<CardControl>().CardName == _secondCard.GetComponent<CardControl>().CardName)
        {
            return true;
        }

        return false;
    }

    private void GameOver()
    {
        
        GameOverScreen.SetActive(true);
        onTime = false;
        /* foreach (Transform child in CardsTable.transform)
         {
             GameObject.Destroy(child.gameObject);

         }*/

    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(1f);
        //PlayerPrefs.SetInt("Scores", score.scoreCount);
        foreach (Transform child in CardsTable.transform)
        {
            GameObject.Destroy(child.gameObject);
            
        }
        WinScreen.SetActive(true);
    }

    public void GoNextLevel()
    {
        cardSpawner.ChooseSpawner();
        WinScreen.SetActive(false);
        _cardsLeft = CardsTable.transform.childCount;
        onTime = true;
        level++;
        levelText.text ="" + level;

    } 
    
    public void BackButton()
    {
        Time.timeScale = 0f;
        WarningScreen.SetActive(true);
    }
    
    public void YesButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void NoButton()
    {
        Time.timeScale = 1;
        WarningScreen.SetActive(false);
    }

    public void ShowHint()
    {
        starCount.text = "" + PlayerPrefs.GetInt("Star");
        if (PlayerPrefs.GetInt("Star") > 0)
        {
            Time.timeScale = 0;
            HintWarningScreen.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Star") <= 0)
        {
            Time.timeScale = 0;
            NotEnoughStar.SetActive(true);
        }
    }
    public void ShowHintYes()
    {
        HintWarningScreen.SetActive(false);
        Time.timeScale = 1;
        PlayerPrefsHelper.instance.DecInt("Star");
        CanFlip = false;
        StartCoroutine("FlipAnimPlay");
    }
    public void ShowHintNo()
    {
        Time.timeScale = 1;
        HintWarningScreen.SetActive(false);
        NotEnoughStar.SetActive(false);
    }

    public void ShowVideo()
    {
        RewardStar.instance.ShowAd();
        NotEnoughStar.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator FlipAnimPlay()
    {
        foreach (Transform child in CardsTable.transform)
        {
            child.GetComponent<Animator>().SetTrigger("CardFlip");
            yield return new WaitForSeconds(0.3f);
            child.GetComponent<Image>().sprite = child.GetComponent<CardControl>()._frontSideCardSprite;
        }
        StartCoroutine("DisableHint");
    }
    IEnumerator DisableHint()
    {
        yield return new WaitForSeconds(2f);
        foreach (Transform child in CardsTable.transform)
        {
            
            child.GetComponent<Image>().sprite = child.GetComponent<CardControl>()._backSideCardSprite;
        }
        CanFlip = true;
    }

    public void WatchVideoAndAddTime()
    {
        RewardStar.instance.ShowAdForTime();
    }


}
