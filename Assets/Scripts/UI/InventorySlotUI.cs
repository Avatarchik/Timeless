﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InventorySlotUI : EventTrigger {

    private Vector3 orgLocalPos;
    private bool canDrag = false;
    private PlayerOwner player;

    void Awake(){
        orgLocalPos = transform.localPosition;
        player = GameObject.FindWithTag("Player") ? GameObject.FindWithTag("Player").GetComponent<PlayerOwner>() : null;
    }
    void OnEnable(){
        if ( player == null ){
            player = GameObject.FindWithTag("Player") ? GameObject.FindWithTag("Player").GetComponent<PlayerOwner>() : null;
        }
    }

    public override void OnBeginDrag(PointerEventData eventData){
        if ( player != null && int.Parse(name) < player.inventory.items.Count ){
            transform.SetAsLastSibling();
            canDrag = true;
        }
    }
    public override void OnDrag(PointerEventData eventData){
        if ( canDrag ){
            Vector3 newPos = Input.mousePosition;
            newPos.z = transform.position.z;
            transform.position = Input.mousePosition;
        }
    }
    public override void OnEndDrag(PointerEventData eventData){
        transform.SetSiblingIndex(int.Parse(name));
        transform.localPosition = orgLocalPos;
        canDrag = false;
    }

}
