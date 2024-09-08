using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;
public class GFXControl : MonoBehaviour
{
    public UniversalRenderPipelineAsset myRenderer;
    public TextMeshProUGUI Rendertext;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        renderset();

    }

    public void renderset()
    {
        myRenderer.renderScale = GetComponent<Slider>().value;

        Rendertext.text = myRenderer.renderScale.ToString().Substring(0, 4) ;
    }
}
