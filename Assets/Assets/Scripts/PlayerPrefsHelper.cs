using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsHelper : MonoBehaviour {

    public static PlayerPrefsHelper instance;
    

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

    

    public void IncInt(string key, int count = 1)
    {
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + count);
    }
    public void DecInt(string key, int count = 1)
    {
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) - count);
    }





}
