﻿using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using MassiveNet;

public class ItemDatabase {

    private List<Item> items = new List<Item>(){
        new Item("Sword", "sword-0", "This is a sword.", null)
    }; 

    private static ItemDatabase _instance;
    public static ItemDatabase instance {
        get {
            if ( _instance == null ){
                _instance = new ItemDatabase();
            }
            return _instance;
        }
    }

    public ItemDatabase(){
        
    }

    public Item GetItem(string id){
        return items.Where(i => i.id == id).FirstOrDefault<Item>() ?? null;
    }
}
