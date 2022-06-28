﻿using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive;
    public Transform rightHandObj;
    public Transform leftHandObj;
    public Transform rightFootObj;
    public Transform leftFootObj;
    public Transform lookObj;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {
                // Set the look target position, if one has been assigned

                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    SetIKTransform(AvatarIKGoal.RightHand, rightHandObj);
                }

                // Set the left hand target position and rotation, if one has been assigned
                if (leftHandObj != null)
                {
                    SetIKTransform(AvatarIKGoal.LeftHand, leftHandObj);
                }

                // Set the right foot target position and rotation, if one has been assigned
                if (rightFootObj != null)
                {
                    SetIKTransform(AvatarIKGoal.RightFoot, rightFootObj);
                }

                // Set the left foot target position and rotation, if one has been assigned
                if (leftFootObj != null)
                {
                    SetIKTransform(AvatarIKGoal.LeftFoot, leftFootObj);
                }
            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }

    void SetIKTransform(AvatarIKGoal a, Transform t)
    {
        animator.SetIKPositionWeight(a, 1);
        animator.SetIKRotationWeight(a, 1);
        animator.SetIKPosition(a, t.position);
        animator.SetIKRotation(a, t.rotation);
    }
}