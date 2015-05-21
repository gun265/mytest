using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharactorControl : MonoBehaviour {

    public float moveSpeed = 1.0f;
    public float rotationSpeed = 1.0f;

    Vector3 moveVector;
    Vector3 rotateVector;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
        //float amtMove = moveSpeed * Time.smoothDeltaTime;
        //float amtRotaion = rotationSpeed * Time.smoothDeltaTime;

        // 전방벡터 변수
        //Vector3 forwardVec = transform.forward;
        

        // 키보드 조작 부분
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.Translate(forwardVec * amtMove, Space.World);
        //    this.animation.CrossFade("Hero_Walk");
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //transform.Translate(-1 * Vector3.right * amtMove, Space.World);
        //    transform.Rotate(Vector3.up, -amtRotaion);
        //    this.animation.CrossFade("Hero_Walk");
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.Translate(-1 * forwardVec * amtMove, Space.World);
        //    this.animation.CrossFade("Hero_Walk");
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    //transform.Translate(Vector3.right * amtMove, Space.World);
        //    transform.Rotate(Vector3.up, amtRotaion);
        //    this.animation.CrossFade("Hero_Walk");
        //}
        float horizontalkey = Input.GetAxis("Horizontal");
        float verticalkey = Input.GetAxis("Vertical");

        playerMove(horizontalkey, verticalkey);   

        if (Input.GetKey(KeyCode.Space))
        {
            this.animation.CrossFade("Hero_Attack");
        }
	}

    void playerMove(float h, float v)
    {
        this.transform.Translate(this.transform.forward * v * moveSpeed * Time.smoothDeltaTime, Space.World);
        this.transform.Rotate(transform.up, h * rotationSpeed * Time.smoothDeltaTime);
        this.animation.CrossFade("Hero_Walk");
    }

    void onAniEnd()
    {
        this.animation.CrossFade("Hero_Wait");
    }
}
