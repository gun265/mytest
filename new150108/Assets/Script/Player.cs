using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    Vector3 mTargetPos = Vector3.zero;
    float mSpeed = 0.05f;
    //float mY = 0.5f;
    bool bMove = false;
    bool bIsMovable = true;
    public List<Vector3> MyPath = new List<Vector3>();
    Animator Ani;
    Camera Cam;

    void Awake()
    {
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

	// Use this for initialization
	void Start () 
    {
        Ani = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if( Input.GetMouseButtonDown(1))
        {
            Debug.Log("Enter the RayCast");
            RaycastHit rayhit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            mTargetPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
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
                {
                    bIsMovable = false;
                    Ani.SetBool("Move", false);
                    return;
                }
                mTargetPos = rayhit.point;
                bIsMovable = bMove = true;
                Ani.SetBool("Move", true);
            }
            //if (mTargetPos != new Vector3(float.MaxValue, float.MaxValue, float.MaxValue))
              //  astar.Inst.GetStartPos(transform.position);
        }
        if (bMove)
        {
            Vector3 vDir = (mTargetPos - this.transform.position).normalized;
            this.transform.position += vDir * mSpeed;
            if (bIsMovable)
                this.transform.LookAt(mTargetPos);
            //Cam.transform.position += vDir * mSpeed; // 카메라 이동
            Debug.DrawLine(transform.position, mTargetPos, Color.black);
            if (Vector3.Distance(this.transform.position, mTargetPos) <= 0.1f)
            {
                bMove = false;
                Ani.SetBool("Move", false);
            }
                
        }
	}

    void FindPath(Vector3 vEndPos)
    {
        MyPath.Add(vEndPos);

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("충돌감지");
        bMove = false;
        Ani.SetBool("Move", false);
    }
}
