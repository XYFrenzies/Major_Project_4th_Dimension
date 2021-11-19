﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
public class ScannerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform m_scanLocation = null;
    [SerializeField] private Material material = null;
    [SerializeField] private Volume volume = null;
    [SerializeField] private Vector2 m_colourValueStart;
    [SerializeField] private Vector2 m_colourValueEnd;
    [SerializeField] private float m_speed = 40.0f;
    [SerializeField] private float m_timeToScan = 5.0f;
    [SerializeField] private bool m_scanGreyScale = false;
    private CustomPassVolume m_passVolume;
    private VolumeProfile profile;
    private TextureCurve originalTex;
    private TextureCurve newTex;
    private InputAction scannerAction;
    private float m_scanDistance;
    private bool m_scanning = false;
    private bool m_hasBeganScanning = false;
    private Camera m_camera;
    private List<string> alpha = new List<string>();//This is used to prevent code from being repeated.
    private readonly string[] alphabet = { "A", "B", "C", "D" };
    private readonly Keyframe[] originalKeyTex = { new Keyframe(0, 0.5f), new Keyframe(1, 0.5f) };
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        m_passVolume = GetComponent<CustomPassVolume>();
        scannerAction = playerInput.actions["Scanner"];
        profile = volume.sharedProfile;
        Keyframe[] frame = {new Keyframe(m_colourValueStart.x, m_colourValueStart.y),
            new Keyframe(m_colourValueEnd.x, m_colourValueEnd.y) };
        newTex = new TextureCurve(frame, 0, false, new Vector2(0, 1));
        m_camera = Camera.main;
        alpha.AddRange(alphabet);
        SetScanner(false);
    }

    private void OnEnable()
    {
        scannerAction.performed += _ => Scanner();
    }

    public void Scanner()
    {
        if (!m_scanning)
        {
            SoundPlayer.Instance.PlaySoundEffect("Scanner", source);
            SetScanner(true);
            Debug.Log("Scanning");
            m_scanning = true;
            m_scanDistance = 3;
            material.SetFloat("_ScanDistance", m_scanDistance);
        }
    }

    private void Update()
    {
        //If the scan button has been pressed.
        if (m_scanning)
        {
            //If it has began scanning (prevents from going through this loop more thank once).
            if (!m_hasBeganScanning)
            {
                StartCoroutine(ScanEffect());
                m_hasBeganScanning = true;
            }
            //Changes the colour to a greyscaled effect.
            if (profile.TryGet<ColorCurves>(out var col))
            {
                col.hueVsSat.overrideState = true;
                col.hueVsSat.Override(newTex);
            }
            //The distance of the wave increases.
            m_scanDistance += Time.deltaTime * m_speed;
            //This checks if all the indicators exist and to set their colour.
            if (Indicator.Instance != null)
            {
                IndicatorChangeLayor(Indicator.Instance.objCanMoveAround, false, "SeeThroughWallsObjects");
                IndicatorChangeLayor(Indicator.Instance.objCanHookTo, false, "SeeThroughWallsHook");
                IndicatorChangeLayor(Indicator.Instance.objPower, false, "Power");
                IndicatorChangeLayor(Indicator.Instance.objSwitch, false, "Switch");
            }
            material.SetFloat("_ScanDistance", m_scanDistance);
            material.SetVector("_WorldSpaceScannerPos", new Vector4(m_scanLocation.position.x, m_scanLocation.position.y, m_scanLocation.position.z, 0));
        }
        CameraFrustumCorners();
    }
    private void CameraFrustumCorners()
    {
        Vector3[] frustumCorners = new Vector3[4];
        Vector3[] frustumCornersWorldPos = new Vector3[4];
        m_camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), m_camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);
        for (int i = 0; i < 4; i++)
        {
            var worldSpaceCorner = m_camera.transform.TransformVector(frustumCorners[i]);
            frustumCornersWorldPos[i] = worldSpaceCorner;
            material.SetVector("_vector" + alphabet[i], frustumCornersWorldPos[i]);
        }
    }
    private IEnumerator ScanEffect()
    {
        yield return new WaitForSeconds(m_timeToScan);
        ResetScanningEffect(false);
    }
    private void ResetScanningEffect(bool isOnDisable)
    {
        if (profile.TryGet<ColorCurves>(out var col))
        {
            originalTex = new TextureCurve(originalKeyTex, 0, false, new Vector2(0, 1));
            col.hueVsSat.Override(originalTex);
            col.hueVsSat.overrideState = false;
            m_scanning = false;
            m_hasBeganScanning = false;
        }
        if (Indicator.Instance != null && !isOnDisable)
        {
            SetScanner(false);
            IndicatorChangeLayor(Indicator.Instance.objCanMoveAround, true, "SeeThroughWallsObjects");
            IndicatorChangeLayor(Indicator.Instance.objCanHookTo, true, "SeeThroughWallsHook");
            IndicatorChangeLayor(Indicator.Instance.objPower, true, "Power");
            IndicatorChangeLayor(Indicator.Instance.objSwitch, true, "Switch");
            foreach (var indicator in Indicator.Instance.objCanMoveAround)
            {
                indicator.layer = LayerMask.NameToLayer("Default");
            }
        }
    }
    private void OnDisable()
    {
        m_scanDistance = 0;
        material.SetFloat("_ScanDistance", m_scanDistance);
        ResetScanningEffect(true);
        scannerAction.performed -= _ => Scanner();

    }
    private void IndicatorChangeLayor(List<GameObject> gameObjects, bool changeToDefaultLayer, string layorChange)
    {
        foreach (var item in gameObjects)
        {
            if (!changeToDefaultLayer && Vector3.Distance(m_scanLocation.position, item.transform.position) <= m_scanDistance)
            {
                item.layer = LayerMask.NameToLayer(layorChange);
            }
            else
            {
                item.layer = LayerMask.NameToLayer("Default");
            }
        }
    }
    private void SetScanner(bool setActive)
    {
        for (int i = 0; i < m_passVolume.customPasses.Count; i++)
        {
            m_passVolume.customPasses[i].enabled = setActive;
        }
    }
}