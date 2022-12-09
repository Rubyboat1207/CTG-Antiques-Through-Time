using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] List<string> Queue = new List<string>();
    public int QueueTooltip(string tooltip)
    {
        Queue.Add(tooltip);
        return Queue.Count - 2;
    }
    public void QueueTooltipFront(string tooltip)
    {
        Queue.Insert(0, tooltip);
    }
    public void OrderQueue(int index, int swap)
    {
        string move = Queue[index];
        Queue.RemoveAt(index);
        Queue.Insert(swap, move);
    }

    public void Update()
    {
        if(Queue.Count > 0)
        {
            if(!GetComponent<Animation>().isPlaying)
            {
                GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Queue[0];
                GetComponent<Animation>().Play("tooltip");
                Queue.RemoveAt(0);
            }
        }
    }
}
