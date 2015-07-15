using UnityEngine;
using System.Collections;

public class VI_Pad : MonoBehaviour 
{
    public      Camera          UICam               = null;
    public      Transform       InnerPad            = null;
                Vector3         dir                 = new Vector3(0,0,0);
                float           distance            = 0.0f;
                float           distanceMax         = 0.0f;
    public      CharactorMove   CharMove            = null;
                bool            IsPress             = false;

    void Awake ()
    {
        // InnerPad가 null일 경우 스스로 찾음
        if (!InnerPad)
        {
            InnerPad = GameObject.Find("VI_Pad_Inner").transform;
        }

        // CharMove이 null 일 경우 스스로 찾음
        if (!CharMove)
        {
            CharMove = GameObject.Find("PLAYER").GetComponent<CharactorMove>();
        }

        // 최대 거리
        distanceMax = (transform.position - GameObject.Find("UI Root/UICam/UIPanel/VI_Pad/MaxXpos").transform.position).magnitude;
    }

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if ( IsPress)
        {
            Ray ray = UICam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayhit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            // Raycast를 통해 패드를 클릭하고 있는지 판단
            if (rayhit.collider == this.collider2D)
            {
                Vector3 mouse = Input.mousePosition;
                Vector3 padPos = UICam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 0));
                InnerPad.position = padPos;
            }
        }
        
        // 방향벡터 얻기
        Vector3 Temp = (InnerPad.position - transform.position).normalized;
        dir.x = Temp.x;
        dir.y = 0.0f;
        dir.z = Temp.y;

        // 거리 계산
        distance = (InnerPad.position - transform.position).magnitude;
        Debug.Log(distance + ", " + distanceMax);
        //Debug.Log(InnerPad.position);
        // 캐릭터 이동 회전 호출
        CharMove.playerMove(dir, distance / distanceMax);
    }

    void OnPress(bool _IsPress)
    {
        IsPress = _IsPress;
    }
}