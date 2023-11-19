using UnityEngine;

namespace App.Scripts.Models
{
    public class NoteColorPreference
    {
        public Color InnerColor { get; private set; }
        public Color OuterColor { get; private set; }
        public Color RotColor { get; private set; }

        public NoteColorPreference(Color innerColor, Color outerColor, Color rotColor)
        {
            InnerColor = innerColor;
            OuterColor = outerColor;
            RotColor = rotColor;
        }

        public NoteColorPreference()
        {
            InnerColor = PlayerPrefs.GetString("InnerColor", "23,255,238").ToColor();
            OuterColor = PlayerPrefs.GetString("OuterColor", "255,23,145").ToColor();
            RotColor = PlayerPrefs.GetString("RotColor", "255,255,255").ToColor();
        }

        public void SetColor(Color inner, Color outer, Color rotColor)
        {
            InnerColor = inner;
            PlayerPrefs.SetString("InnerColor", inner.ToRGBString());
            OuterColor = outer;
            PlayerPrefs.SetString("OuterColor", outer.ToRGBString());
            RotColor = rotColor;
            PlayerPrefs.SetString("RotColor", rotColor.ToRGBString());
        }
    }

    public static class Utils
    {
        public static string ToRGBString(this Color color)
        {
            return $"{(int)(color.r * 255)},{(int)(color.g * 255)},{(int)(color.b * 255)}";
        }

        public static Color ToColor(this string rgbString)
        {
            var rgb = rgbString.Split(',');
            return new Color(float.Parse(rgb[0]) / 255f, float.Parse(rgb[1]) / 255f, float.Parse(rgb[2]) / 255f, 1);
        }
    }
}