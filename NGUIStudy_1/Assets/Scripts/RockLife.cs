using UnityEngine;
using System.Collections;

public class RockLife : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if( transform.position.x > 10)
        {
            Destroy(this.gameObject);
        }
	}
}
