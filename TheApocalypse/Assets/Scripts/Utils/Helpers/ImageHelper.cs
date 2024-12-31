using Assets.Scripts.Models.Generic;
using System;
using UnityEngine;

namespace Assets.Scripts.Utils.Helpers
{
    /// <summary>
    /// Methods for images.
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Add one image on top of another.
        /// </summary>
        /// <param name="bottomImage">The bottom image.</param>
        /// <param name="topImage">The top image.</param>
        /// <returns>The combined image.</returns>
        public static Texture2D AlphaBlend(this Texture2D bottomImage, Texture2D topImage, ColorInfo colorInfo = null)
        {
            if (topImage == null)
            {
                return bottomImage;
            }

            if (bottomImage.width != topImage.width || bottomImage.height != topImage.height)
            {
                throw new InvalidOperationException("AlphaBlend only works with two equal sized images");
            }            

            if (colorInfo != null && colorInfo.shouldInvert)
            {
                topImage = InvertTexture(topImage);
            }


            var bottomData = bottomImage.GetPixels();
            var topData = topImage.GetPixels();
            var count = bottomData.Length;
            var pixelColor = new Color[count];

            for (int i = 0; i < count; i++)
            {
                var bottomColor = bottomData[i];
                var topColor = topData[i];
                var topAlpha = topColor.a;
                var bottomAlpha = 1f - topColor.a;
                var alpha = topAlpha + bottomAlpha * bottomColor.a;

                if (colorInfo != null && topAlpha != 0)
                {
                    topColor = ((Color)colorInfo.color + topColor) / 2;
                }

                var finalColor = (topColor * topAlpha + bottomColor * bottomColor.a * bottomAlpha) / alpha;
                finalColor.a = alpha;
                pixelColor[i] = finalColor;
            }

            var res = new Texture2D(topImage.width, topImage.height);

            res.SetPixels(pixelColor);
            res.Apply();

            return res;
        }

        private static Texture2D InvertTexture(Texture2D original)
        {
            var imageData = original.GetPixels();
            var count = imageData.Length;
            var pixelColor = new Color[count];

            for (int i = 0; i < count; i++)
            {
                var color = imageData[i];
                var alpha = color.a;
                //pixelColor[i] = new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
                pixelColor[i] = new Color(color.r * 5f, color.g * 5f, color.b * 5f)
                {
                    a = alpha
                };
            }

            var res = new Texture2D(original.width, original.height);

            res.SetPixels(pixelColor);
            res.Apply();

            return res;
        }
    }
}
