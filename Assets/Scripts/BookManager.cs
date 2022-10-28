using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookManager : MonoBehaviour
{
    [System.Serializable]
    public struct Page {
        public string Title;
        public string Content;
    }
    public Page[] pages;
    public int page;
    [SerializeField] Button Left;
    [SerializeField] Button Right;
    [SerializeField] TextMeshProUGUI Title;
    [SerializeField] TextMeshProUGUI Content;


    // Start is called before the first frame update
    void Start()
    {
        SetPage(page);
    }

    void SetPage(int index) {
        Title.text = pages[index].Title;
        Content.text = pages[index].Content;
        page = index;
        SetButtonActivity();
    }
    public void IncrementPage(int value) {
        SetPage(page + value);
    }

    void SetButtonActivity() {
        if(page == 0) {
            Left.interactable = false;
        }else{
            Left.interactable = true;
        }

        if(page == pages.Length - 1) {
            Right.interactable = false;
        }else{
            Right.interactable = true;
        }
    }
}
