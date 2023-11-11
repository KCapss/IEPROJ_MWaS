using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

using Bloom = UnityEngine.Rendering.Universal.Bloom;


public class TurnIndicator : MonoBehaviour
{
    [Header("Shader Parameters")]
    [SerializeField] private float duration;
    [SerializeField] private Color attackColor;
    [SerializeField] private AnimationCurve intensityCurve;
    [SerializeField] private AnimationCurve scatterCurve;


    private Material mat;
    private Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    private float elapsedTime = 0;

    [Header("Debug Testing")]
    [SerializeField] private bool isActive;
    [SerializeField] private Volume _localVolume;
    [SerializeField] private Bloom _bloom;



    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        mat = GetComponent<SpriteRenderer>().sharedMaterial;
        mat.SetColor("_shaderOutlineColor", attackColor);

        if (_localVolume.profile.TryGet<Bloom>(out _bloom))
        {
            Debug.Log("Bloom Retrive Success");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            elapsedTime += Time.deltaTime;
            float delta = elapsedTime / duration;

            SetbloomIntensity(Mathf.Lerp(0, 1, intensityCurve.Evaluate(delta)));
            SetScatterIntensity(Mathf.Lerp(0, 1, scatterCurve.Evaluate(delta)));


            if (elapsedTime > duration)
            {
                isActive = false;
                //Reset();
            }
               
        }
    }

    public void Reset()
    {
        elapsedTime = 0;
        SetbloomIntensity(0);
        SetScatterIntensity(0);
        setShaderOutline(Color.clear);
        isActive = false;
    }

    public void Restart()
    {
        elapsedTime = 0;
        SetbloomIntensity(0);
        SetScatterIntensity(0);
        setShaderOutline(attackColor);
        isActive = true;
    }

    public void setShaderOutline(Color color)
    {
        mat.SetColor("_shaderOutlineColor", color);
        _bloom.tint.value = color;
    }

    public void SetbloomIntensity(float intensity)
    {
        _localVolume.weight = intensity;
    }
    public void SetScatterIntensity(float intensity)
    {
        float fixedScatterRange = Mathf.Clamp(0.15f, 0.55f, intensity);
        _bloom.scatter.value = fixedScatterRange;
    }
}
