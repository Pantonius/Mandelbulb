using UnityEngine;
using UnityEngine.UI;

public class Mandelbrot : MonoBehaviour
{
    double width, height;
    double rStart, iStart;
    int maxIterations, increment;
    float zoom;

    // Compute Shader Stuff
    public ComputeShader shader;
    ComputeBuffer buffer;
    RenderTexture texture;
    public RawImage image;

    // Computer Buffer
    public struct DataStruct
    {
        public double w, h, r, i;
        public int screenWidth, screenHeight;
    }

    DataStruct[] data;

    // Start is called before the first frame update
    void Start()
    {
        width = 4.5;
        height = width * Screen.height / Screen.width;
        rStart = -2.0;
        iStart = -1.25;
        maxIterations = 500;
        increment = 3;
        zoom = .5f;

        data = new DataStruct[1];
        data[0] = new DataStruct {
                w = width,
                h = height,
                r = rStart,
                i = iStart,
                screenWidth = Screen.width,
                screenHeight = Screen.height
        };

        buffer = new ComputeBuffer(data.Length, 40);

        texture = new RenderTexture(Screen.width, Screen.height, 0);
        texture.enableRandomWrite = true;
        texture.Create();

        mandelbrot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void mandelbrot()
    {
        int kernelHandle = shader.FindKernel("CSMain");

        buffer.SetData(data);
        shader.SetBuffer(kernelHandle, "buffer", buffer);

        shader.SetInt("maxIterations", maxIterations);
        shader.SetTexture(kernelHandle, "Result", texture);

        shader.Dispatch(kernelHandle, Screen.width / 8, Screen.height / 8, 1);

        RenderTexture.active = texture;
        image.material.mainTexture = texture;
    }

    private void OnDestroy()
    {
        buffer.Dispose();
    }
}
