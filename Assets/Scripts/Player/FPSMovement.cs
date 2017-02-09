﻿using UnityEngine;
using System.Collections;
using MassiveNet;

public class FPSMovement : MonoBehaviour {
	
    public float speed = 5f;
    public float jumpForce = 5f;

    public Animator anim;
    private CharacterController cc;

    private Vector3 moveTo = Vector3.zero;
    private float velY = 0f;

    void Awake(){
        cc = transform.GetChild(0).GetComponent<CharacterController>();
    }
    void Update(){
        moveTo = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
        moveTo = Camera.main.transform.TransformDirection(moveTo);
        moveTo = moveTo.normalized*speed;

        if ( cc.isGrounded ){
            velY = -1;
            if ( Input.GetButtonDown("Jump") ){
                velY = jumpForce;
                anim.SetBool("jump",true);
            } else {
                anim.SetBool("jump",false);
            }
        }

        velY += Physics.gravity.y * Time.deltaTime;
        moveTo.y = velY;
        cc.transform.rotation = Quaternion.LookRotation(new Vector3(Camera.main.transform.forward.x,0f,Camera.main.transform.forward.z));

        anim.SetFloat("speed", Mathf.Abs(moveTo.normalized.x+moveTo.normalized.z));
        cc.Move(moveTo*Time.deltaTime);
    }
}