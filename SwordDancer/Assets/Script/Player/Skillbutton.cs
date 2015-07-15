using UnityEngine;
using System.Collections;

public class Skillbutton : MonoBehaviour {

    bool ispress = false;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(ispress)
        {
            GameObject.Find("PLAYER/Cha_Knight").GetComponent<CharactorControl>().Whirlwind();
        }
	}

    void OnPress(bool isDown)
    {
        ispress = isDown;        
    }
}
