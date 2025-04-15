using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;
using System;
public class GFXControl : MonoBehaviour
{
    public UniversalRenderPipelineAsset myRenderer;
    public TextMeshProUGUI Rendertext;
    private UniversalAdditionalCameraData data;
    // Start is called before the first frame update
    void Start()
    {
        data = Camera.main.GetUniversalAdditionalCameraData();
    }

    public void renderset()
    {
        myRenderer.renderScale = GetComponent<Slider>().value;

        Rendertext.text = myRenderer.renderScale.ToString().Substring(0, 4) ;
    }
    public void RendererChange(int  index)
    {
        data.SetRenderer(index);
    }
    public void postProcessToggle(Boolean bo)
    {
        data.renderPostProcessing = bo;

    }
}
