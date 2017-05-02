﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Handles camera movement with user input
public class CameraControl : MonoBehaviour {

    [Header("Positions")]
    public Vector3 standPivotPos;
    public Vector3 crouchPivotPos;

    [Header("Speeds")]
    public float camSpd = 5f;
    public float zoomSpd = 5f;

    [Header("Settings")]
    public float sensitivity = 5f;
    public float minX = -75f, maxX = 90f;
    public float zoomFov = 30f;
    public float hideMeshThreshold = 1f;

    [Header("Object References")]
    public GameObject target;
    public Transform spine;
    public Transform eyes;

    private float rotX = 0f, rotY = 0f;
    private WeaponHandler weaponHandler;
    private bool aiming = false;
    private bool crouching = false;
    private bool restrictX = false;

    void Start(){
        weaponHandler = this.GetSelf().GetComponent<WeaponHandler>();
    }
    void Update(){
        //if ( weaponHandler.currentWeapon != null && !aiming ) Camera.main.transform.localPosition = weaponHandler.currentWeapon.camOffset;
        if ( !aiming && weaponHandler.currentWeapon != null ) Camera.main.transform.position = eyes.transform.position + weaponHandler.currentWeapon.camOffset;

        if ( !GameManager.instance.ignoreControlsInput ){
            rotY = Input.GetAxis("Mouse X") * sensitivity;

            rotX += Input.GetAxis("Mouse Y") * sensitivity;
            rotX = Mathf.Clamp(rotX, minX, maxX);
        
            target.transform.Rotate(Vector3.up * rotY);
            if ( !restrictX ) transform.localEulerAngles = Vector3.left * rotX;
        }
    }
    void LateUpdate(){
        if ( weaponHandler.currentWeapon != null && !restrictX ){
            spine.RotateAround(transform.position,-transform.parent.right, rotX);
            //spine.Rotate(weaponHandler.currentWeapon.spineRotOffset);
        }
    }

    // Aim with given firearm
    public void Aim(Firearm weapon, bool isAiming){
        aiming = isAiming;

        if ( aiming ){
            Camera.main.fieldOfView = Settings.instance.default_fov/2.00f;
            Camera.main.transform.SetParent(weapon.transform);
            Camera.main.transform.localPosition = weapon.aimPos;
            Camera.main.transform.localEulerAngles = weapon.aimRot;
        } else {
            Camera.main.fieldOfView = Settings.instance.default_fov;
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localEulerAngles = Vector3.zero;
        }
    }
    // Adjust camera pivot point
    public void Crouch(bool crouch){
        crouching = crouch;

        if ( crouching ){
            if ( weaponHandler.currentWeapon == null )
                transform.localPosition = crouchPivotPos;
            else
                transform.localPosition = weaponHandler.currentWeapon.crouchPivotPos;
        } else {
            transform.localPosition = standPivotPos;
        }
    }
    // Restrict the rotation along the X axis
    public void SetRestrictX(bool restrict){
        restrictX = restrict;
    }

}
