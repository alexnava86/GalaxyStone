using UnityEngine;
using System.Collections;

public class ColorSwapper : MonoBehaviour
{
    [Range(-180, 180)] public int hue;
    [Range(-1f, 1f)] public float saturation;
    [Range(-1f, 1f)] public float brightness;
    [Range(0, 2)] public int colorType;
    private Texture2D colorPalette;
    private Color[] spriteColors;
    public AbstractCharacter character;// TAKE THIS OUT

    private void Start()
    {
        InitializeColorSwapTexture();
        //AlterColorGroup();
    }
    private void Update()
    {
        //TEST
        //SwapColor(220, new Color(0f, 0f, 0f, 1f));//Turns color with r value of 235 to full red
        AlterColorGroup(character, character.mainColor.ToArray());
    }

    public void InitializeColorSwapTexture()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
        {
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
        }
        colorSwapTex.Apply();

        this.gameObject.GetComponent<SpriteRenderer>().material.SetTexture("_SwapTex", colorSwapTex);

        spriteColors = new Color[colorSwapTex.width];
        colorPalette = colorSwapTex;
    }

    private void SwapColor(int startColor, Color32 newColor)
    {
        spriteColors[startColor] = newColor;
        colorPalette.SetPixel(startColor, 0, newColor);
        colorPalette.Apply();
    }

    private void AlterColorGroup(AbstractCharacter character, Color32[] colorGroup)
    {

        for (int i = 0; i < colorGroup.Length; i++)
        {
            HSB hsb = new HSB();
            RGB rgb = new RGB();

            rgb.r = colorGroup[i].r;
            rgb.g = colorGroup[i].g;
            rgb.b = colorGroup[i].b;
            //Debug.Log("R=" + rgb.r + " G=" + rgb.g + " B=" + rgb.b);

            hsb = HSBFromRGB(rgb.r, rgb.g, rgb.b);
            hsb.hue += hue;
            hsb.saturation += saturation;
            hsb.brightness += brightness;
            rgb = RGBFromHSB(hsb.hue, hsb.saturation, hsb.brightness);
            Debug.Log("Hue=" + hsb.hue + " Sat=" + hsb.saturation + " Bri=" + hsb.brightness);
            Debug.Log("R=" + rgb.r + " G=" + rgb.g + " B=" + rgb.b);
            Color newColor = new Color(rgb.r / 255f, rgb.g / 255f, rgb.b / 255f);
            float num = colorGroup[i].r;
            int startColor = (int)num;
            Debug.Log("Start Color=" + startColor + " New Color=" + newColor);

            SwapColor(startColor, newColor);
            //Debug.Log(startColor);
        }
    }
    public RGB RGBFromHSB(float hue, float saturation, float brightness)
    {
        RGB color = new RGB();
        int i;
        float f, p, q, k;

        hue /= 60;
        i = (int)Mathf.Floor(hue);
        f = hue - i;
        p = brightness * (1 - saturation);
        q = brightness * (1 - saturation * f);
        k = brightness * (1 - saturation * (1 - f));
        switch (i)
        {
            case 0:
                color.r = brightness;
                color.g = k;
                color.b = p;
                break;
            case 1:
                color.r = q;
                color.g = brightness;
                color.b = p;
                break;
            case 2:
                color.r = p;
                color.g = brightness;
                color.b = k;
                break;
            case 3:
                color.r = p;
                color.g = q;
                color.b = brightness;
                break;
            case 4:
                color.r = k;
                color.g = p;
                color.b = brightness;
                break;
            default:
                color.r = brightness;
                color.g = p;
                color.b = q;
                break;
        }
        color.r *= 255;
        color.g *= 255;
        color.b *= 255;
        color.r = (int)color.r;
        color.g = (int)color.g;
        color.b = (int)color.b;
        return color;
    }
    public HSB HSBFromRGB(float r, float g, float b)
    {
        HSB color = new HSB();

        float min = Mathf.Min(r, g, b);
        float max = Mathf.Max(r, g, b);
        float delta = max - min;
        float hue = 0;
        float saturation = 0;
        float brightness = max / 255;

        if (max == r)
        {
            hue = (g - b) / delta;
        }
        if (max == g)
        {
            hue = 2 + (b - r) / delta;
        }
        if (max == b)
        {
            hue = 4 + (r - g) / delta;
        }
        hue *= 60;
        if (hue < 0)
        {
            hue += 360;
        }
        else if (hue > 360)
        {
            hue -= 360;
        }
        hue = (int)hue;
        if (max != 0)
        {
            saturation = delta / max;
        }
        else
        {
            saturation = 0;
            hue = 0;
        }
        color.hue = hue;
        color.saturation = saturation;
        color.brightness = brightness;
        return color;
    }
    public struct HSB
    {
        public float hue;
        public float saturation;
        public float brightness;
    }
    public struct RGB
    {
        //public byte r;
        public float r;
        public float g;
        public float b;
    }
}
