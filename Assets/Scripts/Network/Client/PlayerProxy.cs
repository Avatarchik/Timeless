﻿// MIT License (MIT) - Copyright (c) 2014 jakevn - Please see included LICENSE file
using MassiveNet;
using UnityEngine;

namespace Massive.Examples.NetAdvanced {

    public class PlayerProxy : MonoBehaviour {

        private NetView view;

        public string PlayerName { get; private set; }

        void Awake() {
            view = GetComponent<NetView>();

            // Note: Always register OnReadInstantiateData delegate in Awake
            // OnReadInstantiateData is called immediately after a View is created, so registering
            // in Start instead of Awake means you might miss out on the instantiate data.
            view.OnReadInstantiateData += Instantiate;
        }

        void Instantiate(NetStream stream) {
            PlayerName = stream.ReadString();
            transform.position = stream.ReadVector3();
        }

    }

}
