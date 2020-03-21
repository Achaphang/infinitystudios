using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;

namespace Valve.VR.InteractionSystem {
    //-----------------------------------------------------------------------------
    public class Locomotion : MonoBehaviour {
        CharacterController parent;

        public SteamVR_ActionSet actions;
        public float speed = 1f;
        public SteamVR_Action_Vector2 locomotionAction = SteamVR_Input.GetVector2Action("thumbstickVersion", "Locomotion");

        float gravity = -9.81f;
        Vector3 velocity;

        private void Start() {
            parent = transform.parent.GetComponent<CharacterController>();
            actions.Activate(SteamVR_Input_Sources.LeftHand);
        }

        private void FixedUpdate() {
            MovePlayer(locomotionAction.GetAxis(SteamVR_Input_Sources.LeftHand));
        }


        public void MovePlayer(Vector2 v) {
            Vector3 move = transform.right * v.x + transform.forward * v.y;
            move.Normalize();

            velocity.y += gravity * Time.deltaTime;
            parent.Move(move * speed * Time.deltaTime);
        }
    }
}