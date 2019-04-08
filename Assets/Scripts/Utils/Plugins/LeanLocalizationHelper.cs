using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Lean.Localization;

public class LeanLocalizationHelper : MonoBehaviour
{
    LeanLocalization _localization;
    LeanLocalization Localization {
        get {
            if (_localization == null) { _localization = GetComponent<LeanLocalization>(); }
            return _localization;
        }
    }

    LeanLocalizationMultiLoader _localizationLoader;
    LeanLocalizationMultiLoader LocalizationLoader {
        get {
            if (_localizationLoader == null) { _localizationLoader = GetComponent<LeanLocalizationMultiLoader>(); }
            return _localizationLoader;
        }
    }
    [Button]
    void ClearLocalization() {
        Localization.Cultures = null;
        Localization.Languages = null;
        Localization.Phrases = null;
    }
    [Button]
    void ReloadLocalization() {
        ClearLocalization();
        LocalizationLoader.LoadFromSource();
    }
}
