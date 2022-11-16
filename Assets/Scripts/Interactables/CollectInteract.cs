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
    [SerializeField] Vector3 StartingPosition;
    [SerializeField] float bobHeight = 5;
    [SerializeField] float bobFrequency = 1;
    [SerializeField] bool ShouldGotoNextLevel;
    [SerializeField] int buildIndex;

    public new void Start()
    {
        base.Start();
        StartingPosition = transform.position;
    }

    public override void OnSuccessfulInteract()
    {
        isInteractable = false;
        aquiredScreen = GameObject.Instantiate(
            Resources.Load<GameObject>("Effects/ArtifactAquired"),
            GameObject.Find("Canvas").transform
        );
        aquiredScreen.transform.GetChild(2).GetComponent<Image>().sprite = Texture;
        aquiredScreen.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = $"{ArtifactName} aquired!";
        int rand = Random.Range(0, 10);
        if(shouldDelete) {
            GetComponent<Renderer>().enabled = false;
        }
        aquiredScreen.GetComponent<Animation>().Play(rand == 1 ? "RareCollectableGet" : "CollectableGet");
    }

    private new void Update() {
        base.Update();
        if(aquiredScreen) {
            if(aquiredScreen.GetComponent<FlagHandler>().AnimationComplete){
                Destroy(aquiredScreen);
                if(shouldDelete) {
                    Interacter.selectedInteract = null;
                    Destroy(gameObject);
                    onInteracted.Invoke();
                    if (ShouldGotoNextLevel)
                    {
                        RTConsole.Singleton.GetConVar<int>("mapid").value = buildIndex;
                    }
                }
            }
        }
        transform.position = new Vector3(StartingPosition.x, StartingPosition.y + Mathf.Sin(Time.realtimeSinceStartup * bobFrequency) * bobHeight, StartingPosition.z);
    }
}
