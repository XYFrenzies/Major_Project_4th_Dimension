using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Animations.Rigging;


public class SwitchCam : MonoBehaviour
{
    public PlayerControllerCinemachineLook player;
    public ChainShootStartAgain chainShoot;
    public Rig aimRig;
    public Animator anim;
    public PlayerInput playerInput;
    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;
    private int priorityBoostAMount = 10;
    public float lerpDuration = 0.3f;
    public float startValue = 0;
    public float endValue = 1;
    float layerWeight = 0f;
    [HideInInspector]
    public bool isAimOn = false;
    [HideInInspector]
    public bool isStartShoot = false;

    // Start is called before the first frame update
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
        anim.SetLayerWeight(0, 1f);
        anim.SetLayerWeight(1, 0f);
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => StopAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => StopAim();
    }

    public void StartAim()
    {
        isAimOn = true;
        StartCoroutine(LerpLayerWeight());
        virtualCamera.Priority += priorityBoostAMount;
    }

    public void StopAim()
    {
        isAimOn = false;
        StartCoroutine(LerpLayerWeight());
        virtualCamera.Priority -= priorityBoostAMount;

    }

    public void StartShoot()
    {
        isAimOn = true;
        isStartShoot = true;
        StartCoroutine(LerpLayerWeight());
    }

    public void StopShoot()
    {
        isAimOn = false;
        isStartShoot = false;
        StartCoroutine(LerpLayerWeight());
    }

    IEnumerator LerpLayerWeight()
    {
        if (chainShoot.currentHookShotState == ChainShootStartAgain.HookShotState.Pull)
            yield break;
        float timeElapsed = 0f;

        while (timeElapsed < lerpDuration)
        {
            layerWeight = Mathf.Lerp(isAimOn ? startValue : endValue, isAimOn ? endValue : startValue, timeElapsed / lerpDuration);
            //anim.SetLayerWeight(1, layerWeight);
            aimRig.weight = layerWeight;
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        layerWeight = isAimOn ? endValue : startValue;
        //anim.SetLayerWeight(1, layerWeight);
        aimRig.weight = layerWeight;
        if (isStartShoot)
        {
            player.currentState = PlayerControllerCinemachineLook.State.HookShotThrown;
            if (!chainShoot.isObjectHeld)
                chainShoot.currentHookShotState = ChainShootStartAgain.HookShotState.Throw;
            //else if(chainShoot.isObjectHeld)
            //    chainShoot.currentHookShotState = ChainShootStartAgain.HookShotState.ThrowObject;
            else
                chainShoot.currentHookShotState = ChainShootStartAgain.HookShotState.Place;

            isAimOn = false;
        }
    }

}
