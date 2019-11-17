﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TurretScope : MonoBehaviour
{
    public SteamVR_Action_Boolean triggerPulled;

    public SteamVR_Input_Sources hand;

    private bool isShooting;

    public float fireRate = 15f;

    public float nextTimeToFire = 0f;

    void Start()
    {
        triggerPulled.AddOnStateDownListener(TriggerDown, hand);
        triggerPulled.AddOnStateUpListener(TriggerUp, hand);
    }

    void FixedUpdate() 
    {
        print(GameManager.getInstance.squidMainHealth);
        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Vector3 forward = transform.TransformDirection(Vector3.back) * 100;
            Debug.DrawRay(transform.position, forward, Color.green);
            RaycastHit hit;
            bool raycasthit = Physics.Raycast(transform.position, forward, out hit, 100);
            if (raycasthit)
            {
                bool colliderhit = hit.collider.gameObject.CompareTag("Squid");
                if (colliderhit)
                {
                    GameManager.getInstance.squidMainHealth -= (Time.deltaTime * 10);
                }
            }

        }
    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isShooting = false;
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isShooting = true;
    }
}
