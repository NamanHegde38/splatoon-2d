using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerSpritesheetHandler : MonoBehaviour {

    [SerializeField, BoxGroup("Colours")] private Color mainColour;
    [SerializeField, BoxGroup("Colours")] private Color skinColour;

    [SerializeField, AssetSelector, BoxGroup("Textures")] private Texture2D headTexture;
    [SerializeField, AssetSelector, BoxGroup("Textures")] private Texture2D hairTexture;

    [SerializeField, AssetSelector, BoxGroup("Masks")] private Texture2D headMask;
    [SerializeField, AssetSelector, BoxGroup("Masks")] private Texture2D eyeMask;

    [SerializeField, AssetSelector, BoxGroup("Material")] private Material playerMaterial;

    private void Awake() {
        SetTexture();
    }

    private void SetTexture() {
        var texture = GetTexture();
        playerMaterial.mainTexture = texture;
    }

    private Texture2D GetTexture() {
        var texture = new Texture2D(512, 512, TextureFormat.RGBA32, true);
        
        var headPixels = headTexture.GetPixels(0, 0, 512, 512);
        var headMaskPixels = headMask.GetPixels(0, 0, 512, 512);
        TintColourArraysInsideMask(headPixels, skinColour, headMaskPixels);

        var eyeMaskPixels = eyeMask.GetPixels(0, 0, 512, 512);
        TintColourArraysInsideMask(headPixels, mainColour, eyeMaskPixels);
        
        var hairPixels = hairTexture.GetPixels(0, 0, 512, 512);
        TintColourArray(hairPixels, mainColour);

        MergeColourArrays(headPixels, hairPixels);
        texture.SetPixels(0, 0, 512, 512, headPixels);

        texture.Apply();
        return texture;
    }

    private static void MergeColourArrays(Color[] baseArray, Color[] overlay) {
        for (var i = 0; i < baseArray.Length; i++) {
            if (!(overlay[i].a > 0)) continue;
            if (overlay[i].a >= 1) {
                baseArray[i] = overlay[i];
            }
            else {
                var alpha = overlay[i].a;
                baseArray[i].r += (overlay[i].r - baseArray[i].r) * alpha;
                baseArray[i].g += (overlay[i].g - baseArray[i].g) * alpha;
                baseArray[i].b += (overlay[i].b - baseArray[i].b) * alpha;
                baseArray[i].a += overlay[i].a;
            }
        }
    }

    private static void TintColourArray(Color[] baseArray, Color tint) {
        for (var i = 0; i < baseArray.Length; i++) {
            baseArray[i].r *= tint.r;
            baseArray[i].g *= tint.g;
            baseArray[i].b *= tint.b;
        }
    }

    private static void TintColourArraysInsideMask(Color[] baseArray, Color tint, Color[] mask) {
        for (var i = 0; i < baseArray.Length; i++) {
            if (!(mask[i].a > 0)) continue;
            var baseColour = baseArray[i];
            var fullyTintedColour = tint * baseColour;
            var interpolateAmount = mask[i].a;
            baseArray[i].r += (fullyTintedColour.r - baseColour.r) * interpolateAmount;
            baseArray[i].g += (fullyTintedColour.g - baseColour.g) * interpolateAmount;
            baseArray[i].b += (fullyTintedColour.b - baseColour.b) * interpolateAmount;
        }
    }
}
