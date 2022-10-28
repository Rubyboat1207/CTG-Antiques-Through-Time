using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown dd;
    private void Start() {
        dd.value = QualitySettings.GetQualityLevel();
    }

    public void SetGraphicsQualtiy(int Index) {
        QualitySettings.SetQualityLevel(Index);
        print($"Quality Level Set to: {QualitySettings.GetQualityLevel()}");
    }
}
