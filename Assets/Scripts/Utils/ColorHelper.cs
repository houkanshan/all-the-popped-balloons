using UnityEngine;
using System.Collections;

static public class ColorHelper {
	static public Color alphaTo(Color color, float a) {
		color.a = a;
		return color;
	}

  // These functions are accessible from any Color struct.
  // (Put this script in the Plugins folder for Javascript access)

  public static Color Slerp(this Color a, Color b, float t)
  {
    return (HSBColor.Lerp(HSBColor.FromColor(a), HSBColor.FromColor(b), t)).ToColor();
  }

  // To use the following functions, you must input the Color you wish to change.
  // Example:
  // myColor.H(180, ref myColor);

  // You can either manipulate the values on a scale from 0 to 360 or
  // on a scale from 0 to 1.

  public static void H(this Color c, int hue0to360, ref Color thisColor)
  {
    HSBColor temp = HSBColor.FromColor(c);
    temp.h = (hue0to360 / 360.0f);
    thisColor = HSBColor.ToColor(temp);
  }

  public static void H(this Color c, float hue0to1, ref Color thisColor)
  {
    HSBColor temp = HSBColor.FromColor(thisColor);
    temp.h = hue0to1;
    thisColor = HSBColor.ToColor(temp);
  }

  public static void S(this Color c, int saturation0to360, ref Color thisColor)
  {
    HSBColor temp = HSBColor.FromColor(thisColor);
    temp.s = saturation0to360 / 360.0f;
    thisColor = HSBColor.ToColor(temp);
  }

  public static void S(this Color c, float saturation0to1, ref Color thisColor)
  {
    HSBColor temp = HSBColor.FromColor(thisColor);
    temp.s = saturation0to1;
    thisColor = HSBColor.ToColor(temp);
  }

  public static void B(this Color c, int brightness0to360, ref Color thisColor)
  {
    HSBColor temp = HSBColor.FromColor(thisColor);
    temp.b = brightness0to360 / 360.0f;
    thisColor = HSBColor.ToColor(temp);
  }

  public static void B(this Color c, float brightness0to1, ref Color thisColor)
  {
    HSBColor temp = HSBColor.FromColor(thisColor);
    temp.b = brightness0to1;
    thisColor = HSBColor.ToColor(temp);
  }
}
