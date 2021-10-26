using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class ArmEffects : MonoBehaviour
{
    ArmStateManager arm;
    PlayerInput playerInput;
    InputAction shootAction;
    public bool isShooting = false;
    AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        arm = GetComponent<ArmStateManager>();
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["HookShot"];
        source = GetComponent<AudioSource>();
    }
    public void OnEnable()
    {
        shootAction.performed += ShootingArm;
        shootAction.canceled += UnShootingArm;
    }

    public void OnDisable()
    {
        shootAction.performed -= ShootingArm;
        shootAction.canceled -= UnShootingArm;
    }


    // Update is called once per frame
    void Update()
    {
        if ((isShooting && arm.currentState != arm.pauseState) || arm.currentState == arm.grappleState)
        {
            DrawLineRenderer();
            if (!source.isPlaying)
            {
                SoundPlayer.Instance.PlaySoundEffect("FireArm", source);
            }
        }
        else
        {
            StopDrawingLineRenderer();
            if (source.isPlaying)
            {
                source.Stop();
            }
        }

    }

    private void ShootingArm(InputAction.CallbackContext obj)
    {

        if (arm.currentState != arm.pauseState)
        {
            //isShooting = true;
        }
    }

    private void UnShootingArm(InputAction.CallbackContext obj)
    {
        if (arm.currentState != arm.grappleState)
        {
            isShooting = false;
            arm.lineRenderer.enabled = false;
            arm.realisticBlackHole.SetActive(false);
            arm.blackHoleCentre.transform.localScale = arm.startSize;
            arm.scaleModifier = 1f;
            arm.blackHoleCentre.SetActive(false);
        }
    }


    public void DrawLineRenderer()
    {
        //arm.lineRenderer.enabled = true;
        //arm.blackHoleCentre.SetActive(true);
        //arm.realisticBlackHole.SetActive(true);
        arm.lineRenderer.positionCount = 2;
        arm.lineRenderer.SetPosition(0, arm.shootPoint.position);
        arm.blackHoleCentre.transform.position = arm.shootPoint.position;
        EffectSizeChange();
        if (arm.isObjectHeld)
        {
            arm.lineRenderer.SetPosition(1, arm.hitObject.transform.position);
            arm.realisticBlackHole.transform.position = arm.hitObject.transform.position;
        }
        else
        {
            arm.lineRenderer.SetPosition(1, arm.hitPoint);
            arm.realisticBlackHole.transform.position = arm.hitPoint;

        }
    }

    public void StopDrawingLineRenderer()
    {
        isShooting = false;
        arm.lineRenderer.enabled = false;
        arm.realisticBlackHole.SetActive(false);
        arm.blackHoleCentre.transform.localScale = arm.startSize;
        arm.scaleModifier = 1f;
        arm.blackHoleCentre.SetActive(false);

    }

    public void DrawObjectHoldingEffect()
    {
        //arm.blackHoleDistortion.SetActive(true);
        //arm.blackHoleDistortion.transform.SetParent(arm.hitObject.transform);
        //arm.blackHoleDistortion.transform.position = Vector3.zero;
    }

    public void StopDrawingObjectHoldingEffect()
    {
        //arm.blackHoleDistortion.transform.SetParent(null);
        //arm.blackHoleDistortion.SetActive(false);
    }

    public void EffectSizeChange()
    {
        if (arm.scaleModifier < 4f)
        {
            arm.blackHoleCentre.transform.localScale = arm.startSize * arm.scaleModifier;
            arm.scaleModifier += Time.deltaTime * arm.modifier; // need to find best place to set scalemodifier back to 0
        }
    }
}
