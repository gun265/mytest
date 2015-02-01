using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    Vector3 mTargetPos = Vector3.zero;
    float mSpeed = 0.05f;
    //float mY = 0.5f;
    bool bMove = false;
    public List<Vector3> MyPath = new List<Vector3>();


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if( Input.GetMouseButtonDown(1))
        {
            Debug.Log("Enter the RayCast");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayhit;
            
            //GameObject plane = GameObject.Find("Ground");
            //// 레이캐스팅이 아직 안됨
            ////string str = "x : " + ray.x + ", y : " + ray.y + ", z : " + ray.z;
            //string str = "x : " + ray.origin.x + ", y : " + ray.origin.y + ", z : " + ray.origin.z;
            //Debug.Log(str);
            //this.transform.position += (ray.origin - this.transform.position).normalized * mSpeed;
            //mTargetPos = ray.origin;
            //새로운레이
            //Plane plane = new Plane(Vector3.up, Vector3.zero);
            //float d;
            //if(plane.Raycast(ray, out d))
            //{
            //    mTargetPos = ray.origin + ray.direction * d;
            //    mTargetPos.y = this.transform.position.y;
            //    string str = mTargetPos.x + ", " + mTargetPos.y + ", " + mTargetPos.z;
            //    Debug.Log(str);
            //    bMove = true;
            //}
            //물리 레이캐스트
            //LayerMask ground = LayerMask.GetMask("grounds");
            //Debug.Log(ground.ToString());
            //if (Physics.Raycast(ray, out rayhit, Mathf.Infinity, ground))
            if (Physics.Raycast(ray, out rayhit, Mathf.Infinity))
            {
                if (rayhit.transform.tag != "grounds")
                    return;
                mTargetPos = rayhit.point;
                bMove = true;
            }
            astar.Inst.GetStartPos(transform.position);
        }
        if (bMove)
        {
            Vector3 vDir = (mTargetPos - this.transform.position).normalized;
            this.transform.position += vDir * mSpeed;
            this.transform.LookAt(mTargetPos);
            Debug.DrawLine(transform.position, mTargetPos, Color.black);
            if (Vector3.Distance(this.transform.position, mTargetPos) <= 0.1f)
                bMove = false;
        }
	}

    void FindPath(Vector3 vEndPos)
    {
        MyPath.Add(vEndPos);

    }
}
