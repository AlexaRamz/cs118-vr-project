using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnaglyphRenderer : MonoBehaviour
{
    public RenderTexture leftTexture;
    public RenderTexture rightTexture;
    public RawImage rawImageOutput;

    private Texture2D leftTex2D;
    private Texture2D rightTex2D;
    private Texture2D anaglyphTex2D;

    void Start()
    {
        int width = leftTexture.width;
        int height = leftTexture.height;

        leftTex2D = new Texture2D(width, height, TextureFormat.RGB24, false);
        rightTex2D = new Texture2D(width, height, TextureFormat.RGB24, false);
        anaglyphTex2D = new Texture2D(width, height, TextureFormat.RGB24, false);

        StartCoroutine(RenderStereo());
    }

    IEnumerator RenderStereo()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame(); // GPU rendering completion wait

            // 1. RenderTexture ¡æ Texture2D copy
            RenderTexture.active = leftTexture;
            leftTex2D.ReadPixels(new Rect(0, 0, leftTexture.width, leftTexture.height), 0, 0);
            leftTex2D.Apply();

            RenderTexture.active = rightTexture;
            rightTex2D.ReadPixels(new Rect(0, 0, rightTexture.width, rightTexture.height), 0, 0);
            rightTex2D.Apply();

            RenderTexture.active = null;

            // 2. Anaglyph
            for (int y = 0; y < anaglyphTex2D.height; y++)
            {
                for (int x = 0; x < anaglyphTex2D.width; x++)
                {
                    Color leftColor = leftTex2D.GetPixel(x, y);
                    Color rightColor = rightTex2D.GetPixel(x, y);

                    Color anaglyph = new Color(
                        leftColor.r,      // left eye = red
                        rightColor.g,     // right eye = green
                        rightColor.b,     // right eye = blue
                        1f                // alpha
                    );

                    anaglyphTex2D.SetPixel(x, y, anaglyph);
                }
            }

         
            anaglyphTex2D.Apply();
            rawImageOutput.texture = anaglyphTex2D;
        }
    }
}
