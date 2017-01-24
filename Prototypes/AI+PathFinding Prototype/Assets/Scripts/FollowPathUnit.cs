using UnityEngine;
using System.Collections;

public class FollowPathUnit : MonoBehaviour {

    public bool pathLoop = false;

    public bool reversePath = false;

    public LinePath path;

    private SteeringBasics steeringBasics;
    private FollowPath followPath;
    private AStarPathfinder pathFinder;

    // Use this for initialization
    void Start () {
        path.calcDistances();

        steeringBasics = GetComponent<SteeringBasics>();
        followPath = GetComponent<FollowPath>();
        pathFinder = GetComponent<AStarPathfinder>();
    }
	
	// Update is called once per frame
	void Update () {
        path = new LinePath(pathFinder.GetPath().ToArray());

        path.draw();

        if (reversePath && isAtEndOfPath())
        {
            path.reversePath();
        }

        Vector3 accel = followPath.getSteering(path, pathLoop);

        steeringBasics.steer(accel);
        steeringBasics.lookWhereYoureGoing();
    }

    public bool isAtEndOfPath()
    {
        return Vector3.Distance(path.endNode, transform.position) < followPath.stopRadius;
    }
}
