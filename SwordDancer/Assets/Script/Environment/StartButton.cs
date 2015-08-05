using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {
    Camera          UICam           = null;

	void Awake () 
    {
        UICam = GameObject.Find("UI Root/UICamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetComponent<BoxCollider2D>().size = GetComponent<UISprite>().localSize;

        Ray ray = UICam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D rayhit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (rayhit.collider == this.GetComponent<Collider2D>())
        {
            Application.LoadLevel(1);
        }
	}

    void OnClick()
    {
        Application.LoadLevel("PlayeScene");
    }
}
