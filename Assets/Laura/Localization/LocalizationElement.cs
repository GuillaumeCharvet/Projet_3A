using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Language
{
    French,
    English,
}

[System.Serializable]
public class LocalizedString
{
    public string ID;
    [SerializeField]
    private LocalizedCollection _localizedCollection;

    public void SetLocalizedContent(Language language, string newValue)
    {
        _localizedCollection.contents.Find((c)=>c.language==language).content = newValue;
    }
    
    public string GetLocalizedContent(Language language)
    {
        var r = _localizedCollection.contents.Find((c) => c.language == language).content;

        return r;
    }
}

[System.Serializable]
public class LocalizedStringRef
{
    public string ID;

    public string GetValue(Language language)
    {
        return Localization.GetLocalization().GetLocalizedContent(ID,language);
    }
}


[System.Serializable]
public class LocalizedCollection
{
    public List<LocalizedContent> contents;
    public int DictionarySize => contents == null ? -1 : contents.Count;
    public void InitializeList()
    {
        contents = new List<LocalizedContent>();
        foreach (var language in Enum.GetValues(typeof(Language)))
        {
            contents.Add(new LocalizedContent((Language)language));
        }
    }

}

[System.Serializable]
public class LocalizedContent
{
    public string content;
    public Language language;

    public LocalizedContent(Language language)
    {
        content = String.Empty;
        this.language = language;
    }
}
