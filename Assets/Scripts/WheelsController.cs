﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Cantoneira")
        {
            // collision.gameObject.SetActive(false);
            GameObject skater = transform.parent.gameObject;
            SkaterController sc = skater.GetComponent<SkaterController>();
            /// bool askingForGrind = sc.askingForGrind;
            // Debug.Log("Asking wheel " + sc.askingForGrind);
            BoxCollider2D bc = collision.transform.parent.gameObject.GetComponent<BoxCollider2D>();
            if (sc.askingForGrind)
            {
                sc.grinding = true;
                bc.isTrigger = false;
            } else
            {
                sc.grinding = false;
                bc.isTrigger = true;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cantoneira")
        {
            SkaterController sc = transform.parent.gameObject.GetComponent<SkaterController>();
            BoxCollider2D bc = collision.transform.parent.gameObject.GetComponent<BoxCollider2D>();
            sc.grinding = false;
        }
    }
}
