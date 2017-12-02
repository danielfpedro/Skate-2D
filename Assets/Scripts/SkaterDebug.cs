using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkaterDebug : MonoBehaviour {

    public float velocityX;
    public float velocityY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        velocityX = GetComponent<SkaterController>().rb.velocity.x;
        velocityY = GetComponent<SkaterController>().rb.velocity.y;
    }
}
