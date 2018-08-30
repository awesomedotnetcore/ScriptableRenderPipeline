﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TestSceneController : MonoBehaviour {

    public GameObject[] shGraphObjs;
    public GameObject[] builtInObjs;
    protected Vector3[] startingLocPosSG;
    protected Vector3[] startingLocPosBI;

    protected GameObject currentObj = null;

    // Rotational directional light
    public GameObject dirLight;
    public bool rotateLight = true;
    public float rotationDegPerS = 45.0f;

    protected int mode = 0;
    protected bool viewingSG = true;
    protected int maxMode;

    // UI
    protected bool showUI = true;
    protected CanvasController canvasController;

    // Camera movement
    protected GameObject cameraGO;

    // Rotation
    public float sensitivity = 30.0f; 
    public float maxX = 88.0f;
    public float minX = -5.0f;
    private float rotX = 0.0f;

    private float rotY = 0.0f;
    // Zoom
    public float minCameraDist = 1.0f;
    public float maxCameraDist = 15.0f;
    private float currentDist = 5.0f;
    private float scrollSensitivity = 4.5f;


    void Start()
    {
        if (shGraphObjs.Length != builtInObjs.Length)
        {
            Debug.LogError("The # of test cases must be even.");
        }
        maxMode = shGraphObjs.Length;

        for (int x = 0; x < maxMode; x++)
        {
            if (shGraphObjs[x] == null) Debug.LogError("shGraphObjs["+x+"] is null");
            if (builtInObjs[x] == null) Debug.LogError("builtInObjs["+x+"] is null");
        }

        startingLocPosSG = new Vector3[maxMode];
        startingLocPosBI = new Vector3[maxMode];
        for (int x = 0; x < maxMode; x++)
        {
            startingLocPosSG[x] = shGraphObjs[x].transform.localPosition;
            startingLocPosBI[x] = builtInObjs[x].transform.localPosition;
        }

        cameraGO = Camera.main.gameObject;
        currentDist = Vector3.Distance(cameraGO.transform.position, transform.position);
        rotX = cameraGO.transform.eulerAngles.x;
        rotY = cameraGO.transform.eulerAngles.y;

        canvasController = FindObjectOfType<CanvasController>();
        if (canvasController != null) canvasController.UpdateText(null, mode, maxMode, viewingSG);
    }

    public abstract void ExtraFunctionality();

    public void Update()
    {
        // Input
        bool changed = false;
        if (Input.GetKeyDown("a"))
        {
            mode--;
            if (mode < 0) mode = maxMode;
            changed = true;
        }
        if (Input.GetKeyDown("d"))
        {
            mode++;
            if (mode > maxMode) mode = 0;
            changed = true;
        }
        if (Input.GetKeyDown("space"))
        {
            viewingSG = !viewingSG;
            changed = true;
        }
        if (Input.GetKeyDown("q"))
        {
            showUI = !showUI;
            changed = true;
            if (canvasController != null) canvasController.gameObject.SetActive(showUI);
        }
        if (Input.GetKeyDown("z"))
        {
            rotateLight = !rotateLight;
            if (dirLight == null) Debug.LogWarning("Warning: dirLight == null");
        }

        // Update stuff if we get need to
        if (changed)
        {
            for (int x = 0; x < maxMode; x++)
            {
                if (mode == 0)
                {
                    shGraphObjs[x].SetActive(true);
                    builtInObjs[x].SetActive(true);

                    if (!shGraphObjs[x].isStatic) shGraphObjs[x].transform.localPosition = startingLocPosSG[x];
                    if (!builtInObjs[x].isStatic) builtInObjs[x].transform.localPosition = startingLocPosBI[x];
                }
                else
                {
                    bool active = ((mode - 1) == x);
                    bool sgActive = active &&  viewingSG;
                    bool biActive = active && !viewingSG;
                    if (!shGraphObjs[x].isStatic) shGraphObjs[x].SetActive(sgActive);
                    if (!builtInObjs[x].isStatic) builtInObjs[x].SetActive(biActive);
                    if (active) currentObj = sgActive ? shGraphObjs[x] : builtInObjs[x];

                    if (!shGraphObjs[x].isStatic) shGraphObjs[x].transform.localPosition = Vector3.zero;
                    if (!builtInObjs[x].isStatic) builtInObjs[x].transform.localPosition = Vector3.zero;
                }
            }
            if (mode == 0) currentObj = null;

            if (canvasController != null) canvasController.UpdateText(currentObj ? currentObj.name : null, mode, maxMode, viewingSG);
        }

        if (dirLight != null && rotateLight)
        {
            dirLight.transform.eulerAngles += new Vector3(0.0f, (rotationDegPerS * Time.deltaTime), 0.0f);
        }

        ExtraFunctionality();

        MoveCamera();
    }

    private void MoveCamera()
    {
        // Set rotation
        float gainX = Input.GetAxis("Mouse Y");
        float gainY = Input.GetAxis("Mouse X");

        rotX += (gainX * Time.deltaTime * sensitivity);
        rotY -= (gainY * Time.deltaTime * sensitivity);
        if (rotX < minX) rotX = minX;
        if (rotX > maxX) rotX = maxX;
        rotY %= 360.0f;
        if (rotY < 0.0f) rotY += 360.0f;

        cameraGO.transform.eulerAngles = new Vector3(rotX, rotY, 0.0f);

        // Distance
        currentDist -= Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        if (currentDist < minCameraDist) currentDist = minCameraDist;
        if (currentDist > maxCameraDist) currentDist = maxCameraDist;

        // Set position
        float sinY = Mathf.Sin(rotY * Mathf.Deg2Rad);
        float sinX = Mathf.Sin(rotX * Mathf.Deg2Rad);
        float cosY = Mathf.Cos(rotY * Mathf.Deg2Rad);
        float cosX = Mathf.Cos(rotX * Mathf.Deg2Rad);
        cameraGO.transform.position =
            GetRotationPointGameObj().transform.position +
            new Vector3(
                (currentDist * -sinY) * cosX,
                (currentDist * sinX),
                (currentDist * -cosY) * cosX);
    }

    protected virtual GameObject GetRotationPointGameObj()
    {
        return gameObject;
    }

}