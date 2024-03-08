using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;

    private bool bCanRender = false;

    [SerializeField]
    private Texture[] textures;

    private int animationStep;
    [SerializeField]
    private float fps = 60f;

    private float fpsCounter;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    void Start()
    {
        SetCanRender(false);
    }
    private void Update() 
    {
        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 0.1f / fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
                animationStep = 0;

            lr.material.SetTexture("_MainTex", textures[animationStep]);
            fpsCounter = 0;
        }

        if (bCanRender)
        {
           for (int i = 0; i < points.Length; i++) 
           {
            lr.SetPosition(i, points[i].position);
            } 
        }
        
    }

    public void SetCanRender(bool bCanRender)
    {
        this.bCanRender = bCanRender;
        lr.enabled = bCanRender;
    }
}
