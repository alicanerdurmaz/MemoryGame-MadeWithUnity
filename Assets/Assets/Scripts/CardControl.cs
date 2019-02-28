using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardControl : MonoBehaviour {

    [SerializeField]
    private string _cardName; // tıklanan kartın ismini tutan değişken
    public Animator anim; // kart animasyonunu kontrol eden komponent
    private bool _cardOpened = false; // kart açık/ kapalı olduğunu kontrol eden bool anahtarı

    public Sprite _backSideCardSprite; // kartın arkayüzünü tutan grafik değişkeni
    [SerializeField]
    public Sprite _frontSideCardSprite; //kartınön yüzünü tutan grafik değişkeni
    private Image _spriteRenderer;  // Kartın grafiklerini kontrol eden komponent

    public Button cardButton; // Karta buton işlevi veren komponent

    private Button btn;
    private GameManager _gameManager; // Oyun yöneticisi komponenti
    
    

    public string CardName
    {
        get
        {
            return _cardName;
        }

        set
        {
            _cardName = value;
        }
    }

    public Image SpriteRenderer
    {
        get
        {
            return _spriteRenderer;
        }

        set
        {
            _spriteRenderer = value;
        }
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        btn = cardButton.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        
        SpriteRenderer = GetComponent<Image>();
       
        _backSideCardSprite = SpriteRenderer.sprite;
        anim = GetComponent<Animator>();
    }

    private void onClick() // Kartın tıklanılmasını dinleyen fonksiyon
    {
        if (!_cardOpened && _gameManager.CanFlip == true)
        {
            btn.interactable = false;
            // _gameManager.CanFlip = false;
            AudioManager.instance.Play("FlipSound"); // Kartın ön yüzünün açılış sesini aktive eder
            _gameManager.AddCard(gameObject);
            StartCoroutine(FlipAnimPlay());         // animasyonu başlatır.
            //_gameManager.AddCard(gameObject); // Bu fonksiyonlar artık kullanılmıyor 
            // ChangeSide();                       ama her ihtimale karşı silinmedi
        }
    }
    public void ChangeSide()
    {
        if (!_cardOpened)
        {
            _cardOpened = true;
            btn.interactable = true;
            SpriteRenderer.sprite = _frontSideCardSprite;
            
        }
        else
        {
            SpriteRenderer.sprite = _backSideCardSprite;
            _cardOpened = false;
            
        }
    }
  

    IEnumerator FlipAnimPlay()
    {
        anim.SetTrigger("CardFlip");
        yield return new WaitForSeconds(0.3f);
        ChangeSide();
       // _gameManager.CanFlip = true;
    }
  
}
