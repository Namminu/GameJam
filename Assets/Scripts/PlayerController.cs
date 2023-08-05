using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
	}


    private void Update()
    {
        Move();
        Dash();
    }


    private void Move()
    {
        float h = 0;
        float v = 0;
        if(Input.GetButton("Horizontal")) h = Input.GetAxisRaw("Horizontal");
        if(Input.GetButton("Vertical")) v = Input.GetAxisRaw("Vertical");

        playerMovement.Move(h, v);
    }

    private void Dash()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
			//StartCoroutine(playerMovement.DashWhileJump());
		}
    }
}
