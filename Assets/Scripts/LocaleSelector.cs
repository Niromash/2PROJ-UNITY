using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Localization.Settings;
public class Local : MonoBehaviour
{
    private bool active = false;
    
    private void Start()
    {
        int ID = PlayerPrefs.GetInt("localeID", 0);
        ChangeLocale(ID);
    }
    
    public void ChangeLocale(int _localeID)
    {
        if(active==true)
            return;
        StartCoroutine(SetLocale(_localeID));
    }
    
    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("localeID", _localeID);
        active = false;
    }
}
