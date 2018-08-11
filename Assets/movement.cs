using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    public float speed = 500f;
    public float jumpforce = 50;
    
    
    void Start ()
    {
		
	}
	
	void Update ()
    {
        Vector3 direction = GetComponent<Rigidbody>().velocity;

        direction.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction.y = jumpforce;
        }

        GetComponent<Rigidbody>().velocity = new Vector3(direction.x, direction.y);


    }
}
