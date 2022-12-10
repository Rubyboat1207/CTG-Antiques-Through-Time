using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsController : MonoBehaviour
{
    public GameObject confetti;
    public TextMeshProUGUI bestTime;
    public TextMeshProUGUI curTime;
    public GameObject TextContainer;

    private void Start()
    {
        if(!ConVar.Get<bool>("speedrunTimer").value)
        {
            GetComponent<Animation>().Play("Credits");
        }
    }

    // Start is called before the first frame update
    public void ShowTime(SpeedrunTimerConsoleExtension speedTimer)
    {
        TextContainer.SetActive(true);
        if(ConVar.Get<float>("sp_time").value < speedTimer.bestTime)
        {
            confetti.SetActive(true);
            StartCoroutine(deactivateConfetti());
        }
        bestTime.text = SpeedrunTimerConsoleExtension.toMinSec(ConVar.Get<float>("sp_time").value);
        curTime.text = SpeedrunTimerConsoleExtension.toMinSec(speedTimer.bestTime);
    }

    public IEnumerator deactivateConfetti()
    {
        yield return new WaitForSeconds(2);
        confetti.SetActive(false);
    }

}
