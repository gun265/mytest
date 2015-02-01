using UnityEngine;
using System.Collections;

public class Minions : MonoBehaviour 
{
    public int nSide = 0;
    public int nHp = 100;
    public float fAttackSpeed = 1.0f;
    public float fDamage = 10.0f;

	// Use this for initialization
	void Start () 
    {
        //Material mt = Resources.Load("Material\\Blue", typeof(Material)) as Material;
        //this.renderer.material = mt;
        //switch (nSide)
        //{
        //    case 1:
        //        {
        //            this.renderer.material.color = Color.red;
        //            break;
        //        }
                
        //    case 0:
        //    default:
        //        {
        //            this.renderer.material.color = Color.blue;
        //            break;
        //        }                
        //}
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Vector3 vTargetPos;
        //switch(nSide)
        //{
        //    case 1:
        //        {
        //            vTargetPos = GetComponent("BTemple").transform.position;
        //            break;
        //        }

        //    case 0:
        //    default:
        //        {
        //            vTargetPos = GetComponent("RTemple").transform.position; 
        //            break;
        //        }
        //}
        //Vector3 vPosition = this.transform.position;
        //vPosition = vPosition + (vTargetPos - vPosition).normalized * 2.0f;
	}
}
