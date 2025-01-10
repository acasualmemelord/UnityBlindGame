using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI; //important

public class RandomMovement : MonoBehaviour {
    public NavMeshAgent agent;
    public GameObject eyeLine;
    public float range = 10f; //radius of sphere
    public Animate animate;
    public float timeLimit = 1500f;
    public float maxTimeLimit = 1500f;

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animate = transform.GetComponent<Animate>();
        centrePoint = agent.transform;
    }

    void Update() {
        if (animate.GetStatus() != 2) {
            if (agent.remainingDistance <= agent.stoppingDistance || timeLimit == 0) {
                if (RandomPoint(centrePoint.position, range, out Vector3 point)) {
                    Debug.DrawRay(point, Vector3.up, UnityEngine.Color.blue, 1.0f); //so you can see with gizmos
                    animate.Patrol();

                    agent.SetDestination(point);
                    timeLimit = maxTimeLimit;
                }
            }
        }
        /*else if(animate.GetStatus() != 4) {
            agent.isStopped = true;
            agent.ResetPath();
            //animate.Reset();
        }*/

        if(timeLimit > 0) {
            StartCoroutine(WalkTime());
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result) {
        int tries = 10;
        while (tries-- > 0) {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
            Vector3 eyePoint = eyeLine.transform.position;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas) && !Physics.Raycast(eyePoint, (eyePoint - randomPoint), Vector3.Distance(eyePoint, randomPoint) - 0.1f)) {
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private IEnumerator WalkTime() {
        while (timeLimit > 0) {
            yield return null;
            timeLimit -= Time.deltaTime;
        }
        timeLimit = 0;
    }
}