using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovePoints : MonoBehaviour
{
    [SerializeField] Transform[] points;
    NavMeshAgent agent;
    [SerializeField] int currentPoint = 0;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(points[currentPoint].position);
    }

    public void Update()
    {
        Vector3 pos = transform.position;
        pos.y = 0;
        Vector3 tpos = points[currentPoint].position;
        tpos.y = 0;
        if (Mathf.Abs((pos - tpos).magnitude) < 0.5)
        {
            
            currentPoint = wrap(currentPoint + 1, 0, points.Length - 1);
            agent.SetDestination(points[currentPoint].position);
        }
    }

    public int wrap(int number, int minNum, int maxNum)
    {
        return number > maxNum ? minNum : number < minNum ? maxNum : number;
    }
}
