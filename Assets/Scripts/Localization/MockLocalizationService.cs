using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockLocalizationService : ILocalizationService
{
    public string GetLocalizedText(string key)
    {
        return key;
    }
}
