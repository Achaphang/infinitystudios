// Copyright (c) Valve Corporation, All rights reserved. ======================================================================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem {
    //-----------------------------------------------------------------------------
    public class SmoothTurn : MonoBehaviour {
        public SteamVR_ActionSet actions;
        public float speed = 1f;
        public SteamVR_Action_Vector2 smoothTurnAction = SteamVR_Input.GetVector2Action("thumbstickVersion", "SmoothTurn");

        private void Start() {
            actions.Activate(SteamVR_Input_Sources.RightHand);
        }

        private void Update() {
            RotatePlayer(smoothTurnAction.GetAxis(SteamVR_Input_Sources.RightHand));
        }


        public void RotatePlayer(Vector2 v) {
            Player player = Player.instance;

            Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
            player.trackingOriginTransform.position -= playerFeetOffset;
            player.transform.Rotate(Vector3.up, v.x * speed);
            playerFeetOffset = Quaternion.Euler(0.0f, v.x * speed, 0.0f) * playerFeetOffset;
            player.trackingOriginTransform.position += playerFeetOffset;
        }
    }
}