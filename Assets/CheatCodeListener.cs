using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Events;

public class CheatCodeListener : MonoBehaviour
{
    [SerializeField] GameObject MarioPrefab;
    [SerializeField] GameObject player;
    [SerializeField] int progress = 0; //max : 10
    KeyCode[] kcl = { KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A };

    // Update is called once per frame
    void Update()
    {
        if(progress == -1)
        {
            return;
        }
        if(progress == kcl.Length)
        {
            progress = -1;
            var mario = Instantiate(MarioPrefab, player.transform.position, Quaternion.identity, null);
            mario.GetComponent<ExampleInputProvider>().cameraObject = Camera.main.gameObject;
            player.transform.SetParent(mario.transform);
            player.GetComponent<PlayerMove>().canMove = false;
            player.GetComponent<Renderer>().enabled = false;
            player.GetComponent<HumanoidAnimator>().enabled = false;
            return;
        }
        if(Input.GetKeyDown(kcl[progress]))
        {
            progress++;
            return;
        }
        if(Input.anyKeyDown)
        {
            progress = 0;
        }
    }
}
