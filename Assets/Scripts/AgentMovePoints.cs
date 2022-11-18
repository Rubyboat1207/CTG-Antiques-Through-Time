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
        print(agent.SetDestination(points[currentPoint].position));
    }

    public void Update()
    {
        if (Mathf.Abs((transform.position - points[currentPoint].position).magnitude) < 0.5)
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
