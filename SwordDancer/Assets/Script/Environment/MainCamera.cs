using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

    public GameObject Player;
    // XYZ 증가값
    public float plusX;
    public float plusY;
    public float plusZ;


	// Use this for initialization
	void Start () 
    {
        transform.position = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 TempVec = transform.position;

        // 실시간으로 쿼터뷰 카메라 위치 변경
        TempVec.x = Player.transform.position.x - plusX;
        TempVec.y = Player.transform.position.y + plusY;
        TempVec.z = Player.transform.position.z - plusZ;

        // 위치갱신
        transform.position = TempVec;

	}
}
