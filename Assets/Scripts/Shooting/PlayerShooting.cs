﻿using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public GameObject arrowComponent;
    private PlayerAnimation animationComponent;
    public GameObject arrowPrefab;

    bool isShooting = false;
    float timeSinceLastShot = 0f;

    public float rotateSpeed = 10f;

    public float fireRate = 0.1f;
    public float arrowSpeed = 30f;
    public int numberOfArrows = 3;

    public bool IsShooting
    {
        get
        {
            return isShooting;
        }
    }

    void Awake()
    {
        arrowComponent.SetActive(false);
        animationComponent = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    public void AddArrows(int arrowsToAdd)
    {
        //TODO: Data validation?
        numberOfArrows += arrowsToAdd;
    }

    public void Aim(float aimHorizontal, float aimVertical)
    {
        Vector3 toAim = new Vector3(aimHorizontal, aimVertical, 0);
        //TODO: add aiming with left stick
        //if (toAim == Vector3.zero)
        //{
        //    aimHorizontal = gamepad.GetAxis(AxisCode.GamepadAxisLeftX);
        //    aimVertical = gamepad.GetAxis(AxisCode.GamepadAxisLeftY);
        //    toAim = new Vector3(aimHorizontal, aimVertical, 0);
        //}

        toAim.Normalize();

        if (toAim != Vector3.zero)
        {
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newAim = Vector3.RotateTowards(transform.forward, toAim, step, 0.0F);
            //TODO: Comment on how below line works
            arrowComponent.transform.rotation = Quaternion.LookRotation(Vector3.forward, newAim) * Quaternion.Euler(0, 0, 90);
        }
        animationComponent.AimAnimation(aimHorizontal, aimVertical, Quaternion.identity);
    }

    public void Draw()
    {
        if (timeSinceLastShot >= fireRate)
        {
            Logger.Log("Drawing!", this, LogLevel.Log);
            isShooting = true;
            arrowComponent.SetActive(true);

            //hide arrow if you don't have any!
            arrowComponent.GetComponent<SpriteRenderer>().enabled = numberOfArrows > 0;

            animationComponent.ShootAnimation(true);
        }
    }

    public void Shoot()
    {
        if (isShooting)
        {
            if (numberOfArrows > 0)
            {
                timeSinceLastShot = 0;
                numberOfArrows--;

                GameObject arrow = Instantiate(arrowPrefab, arrowComponent.transform.position, arrowComponent.transform.rotation);
                arrow.GetComponent<Arrow>().Shoot(arrowSpeed, GetComponent<Player>());

                Logger.Log("Shooting!", this, LogLevel.Log);
            }
            isShooting = false;
            arrowComponent.SetActive(false);
            animationComponent.ShootAnimation(false);
        }
    }
}
