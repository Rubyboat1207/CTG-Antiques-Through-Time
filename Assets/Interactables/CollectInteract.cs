using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectInteract : ToggleableInteractable
{
    [Header("Collect Interactable")]
    [SerializeField] Sprite Texture;
    //TODO: Implement once we have a save data system
    [SerializeField] Sprite PreviouslyAquiredTexture;
    [SerializeField] string ArtifactName;

    GameObject aquiredScreen;

    [SerializeField] bool shouldDelete;

    public override void OnSuccessfulInteract()
    {
        isInteractable = false;
        aquiredScreen = GameObject.Instantiate(
            Resources.Load<GameObject>("Effects/ArtifactAquired"),
            GameObject.Find("Canvas").transform
        );
        aquiredScreen.transform.GetChild(2).GetComponent<Image>().sprite = Texture;
        aquiredScreen.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"{ArtifactName} aquired!";
        aquiredScreen.GetComponent<Animation>().Play("CollectableGet");
        if(shouldDelete) {
            Interacter.selectedInteract = null;
            Destroy(gameObject);
        }
    }

    private new void Update() {
        base.Update();
        if(aquiredScreen) {
            if(aquiredScreen.GetComponent<FlagHandler>().AnimationComplete){
                Destroy(aquiredScreen);
            }
        }
    }
}
