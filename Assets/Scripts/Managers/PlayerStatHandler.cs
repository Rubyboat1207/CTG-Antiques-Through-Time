using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    public enum PlayerType {
        Cassie = 0,
        Alfred = 1,
        Brady = 2,
    }
    public PlayerType type;

    [System.Serializable]
    public struct PlayerStat {
        public PlayerType id;
        public float speed;
        public float jumpheight;
    }

    public PlayerStat[] stats;


    // Start is called before the first frame update
    void Start()
    {
        LoadStat(stats[((int)type)]);
    }

    void Update() {
        if(GetComponent<Animator>().GetInteger("Player") != ((int)type)) {
            LoadStat(stats[((int)type)]);
        }
        type = (PlayerType) RTConsole.Singleton.GetConVar<int>("pl_model").Value;
    }
    

    public void LoadStat(PlayerStat stat) {
        GetComponent<PlayerMove>().speed = stat.speed;
        GetComponent<PlayerMove>().jumpForce = stat.jumpheight;
        GetComponent<Animator>().SetInteger("Player", ((int)stat.id));
    }
}
