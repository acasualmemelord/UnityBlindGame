using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

//adapted from https://github.com/mdotstrange/MyUnityScripts/blob/master/GenerateNavLinks.cs
public class GenerateNavLinks : MonoBehaviour {
    public float linkWidth;
    public bool bidirectionalLinks = true;
    public float linkCompenstationAmount;
    public bool[] aStatus = new bool[4];
    public bool[] bStatus = new bool[4];
    public string[] aCoords;
    public string[] bCoords;
    public int direction;
    public List<BoxCollider> floors = new();
    public bool debugLines = false;
    public float wallConnectThreshold;

    public void DoGenerateLinks() {
        ConnectThemAll();
    }

    public void ConnectThemAll() {
        IfDistanceOkThenConnect(floors, floors);
    }

    public void IfDistanceOkThenConnect(List<BoxCollider> aList, List<BoxCollider> bList) {
        for (int index = 0; index < aList.Count; index++) {
            var i = aList[index];

            for (int index1 = index; index1 < bList.Count; index1++) {
                var ii = bList[index1];

                if (IsObjectCloseEnough(i, ii)) {
                    ConnectTheLinks(i);
                }

            }
        }

    }

    public bool IsObjectCloseEnough(Collider a, Collider b) {
        string aName = a.transform.parent.parent.name;
        string aName2 = aName[(aName.IndexOf('-') - 1)..];
        aCoords = aName2.Split('-');
        string bName = b.transform.parent.parent.name;
        string bName2 = bName[(bName.IndexOf('-') - 1)..];
        bCoords = bName2.Split('-');
        int diff = System.Math.Abs(int.Parse(aCoords[0]) - int.Parse(bCoords[0])) + System.Math.Abs(int.Parse(aCoords[1]) - int.Parse(bCoords[1]));

        if (diff != 1) return false;

        GetStatuses(a, b);
        direction = GetDirection();

        var boxCenter = a.GetComponent<BoxCollider>().center;
        var aCenter = a.transform.TransformPoint(boxCenter);

        var closestFromAToB = a.ClosestPoint(b.ClosestPoint(aCenter));
        var closestFromBToA = b.ClosestPoint(closestFromAToB);
        var distance = Vector3.Distance(closestFromAToB, closestFromBToA);

        if (aStatus[direction] && distance <= wallConnectThreshold) {
            return true;
        }
        else {
            return false;
        }

    }

    public void ConnectTheLinks(Collider a) {
        var link = CreateLinkOnCollider(a);
        SetNavMeshLinkData(link);
    }

    public void GetStatuses(Collider a, Collider b) {
        aStatus = a.transform.parent.parent.GetComponent<RoomBehavior>().status;
        bStatus = b.transform.parent.parent.GetComponent<RoomBehavior>().status;
    }

    public int GetDirection() {
        int aX = int.Parse(aCoords[0]);
        int aY = int.Parse(aCoords[1]);
        int bX = int.Parse(bCoords[0]);
        int bY = int.Parse(bCoords[1]);
        if (aY - bY == 1) return 0; //south
        if (aY - bY == -1) return 1; //north
        if (aX - bX == 1) return 2; //west
        if (aX - bX == -1) return 3; //east
        return -1; //how
    }

    public NavMeshLink CreateLinkOnCollider(Collider coll) {
        return coll.gameObject.AddComponent<NavMeshLink>();
    }

    public void SetNavMeshLinkData(NavMeshLink link) {
        switch (direction) {
            case 0:
                link.startPoint = new Vector3(0, 0, 10);
                link.endPoint = new Vector3(0, 0, 20);
                break;
            case 1:
                link.startPoint = new Vector3(0, 0, -10);
                link.endPoint = new Vector3(0, 0, -20);
                break;
            case 2:
                link.startPoint = new Vector3(-20, 0, 0);
                link.endPoint = new Vector3(-10, 0, 0);
                break;
            default:
                link.startPoint = new Vector3(20, 0, 0);
                link.endPoint = new Vector3(10, 0, 0);
                break;
        }
        link.bidirectional = bidirectionalLinks;
        link.width = linkWidth;
    }

    public Vector3 GetBoxCenterPosition(Collider coll, Transform trans) {
        var box = coll.GetComponent<BoxCollider>().center;
        return trans.transform.TransformPoint(box);
    }

}