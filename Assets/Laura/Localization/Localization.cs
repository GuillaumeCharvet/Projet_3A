using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[CreateAssetMenu(menuName = "Localization", fileName = "New Localization", order = 0)]

public class Localization : ScriptableObject
{
    [SerializeField]
    private List<LocalizedString> localizedSentence;
    [SerializeField]
    private string[]  IDList;

    
    public string GetLocalizedContent(string id, Language language)
    {
        foreach (var sentence in localizedSentence)
        {
            if (sentence.ID == id)
            {
                return sentence.GetLocalizedContent(language);
            }
        }

        return string.Empty;
    }

    public static Localization GetLocalization()
    {
        return Resources.Load<Localization>("Localization");
    }

    public void GenerateList()
    {
        IDList = new string[localizedSentence.Count];
        for (int i = 0; i < localizedSentence.Count; i++)
        {
            IDList[i] = localizedSentence[i].ID;
        }
    }

    public string[] GetIDList()
    {
        return IDList;
    }
}
