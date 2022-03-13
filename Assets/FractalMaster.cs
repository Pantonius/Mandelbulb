using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalMaster : MonoBehaviour
{
    public ComputeShader mandelbulb;
    RenderTexture texture;

    // Fractal
    public float power = 9;
    public float darkness = 70;

    // Colours
    [Header("Colours")]
    public bool normalsColour;
    [Range(0, 1)] public float blackAndWhite = 0;
    [Range(0, 1)] public float redA;
    [Range(0, 1)] public float greenA;
    [Range(0, 1)] public float blueA;
    [Range(0, 1)] public float redB;
    [Range(0, 1)] public float greenB;
    [Range(0, 1)] public float blueB;

    // Animation
    [Header("Animation")]
    public bool animate = true;
    public float powerSpeed = .1f;


    // World
    Camera cam;
    Light lightSource;

    void Init()
    {
        cam = Camera.current;
        lightSource = FindObjectOfType<Light>();
    }

    void InitRenderTexture()
    {
        if(texture == null || texture.width != cam.pixelWidth || texture.height != cam.pixelHeight)
        {
            if (texture != null) texture.Release();

            texture = new RenderTexture(cam.pixelWidth, cam.pixelHeight, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
            texture.enableRandomWrite = true;
            texture.Create();
        }
    }

    void SetShaderParams()
    {
        mandelbulb.SetTexture(0, "Result", texture);

        mandelbulb.SetFloat("power", power);
        mandelbulb.SetFloat("darkness", darkness);
        mandelbulb.SetFloat("bw", blackAndWhite);

        mandelbulb.SetVector("colourA", new Vector3(redA, greenA, blueA));
        mandelbulb.SetVector("colourB", new Vector3(redB, greenB, blueB));
        mandelbulb.SetBool("normalsColour", normalsColour);

        mandelbulb.SetMatrix("_CameraPos", cam.cameraToWorldMatrix);
        mandelbulb.SetMatrix("_CameraInverseProjection", cam.projectionMatrix.inverse);
        mandelbulb.SetVector("_LightDirection", lightSource.transform.forward);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(animate)
        {
            power += powerSpeed * Time.deltaTime;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Init();
        InitRenderTexture();
        SetShaderParams();

        int kernelHandle = mandelbulb.FindKernel("CSMain");

        mandelbulb.Dispatch(kernelHandle, Mathf.CeilToInt(cam.pixelWidth / 8.0f), Mathf.CeilToInt(cam.pixelHeight / 8.0f), 1);

        Graphics.Blit(texture, destination);
    }
}
