using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TJ {

	public static float toDeg(float rad)
	{
		return rad * (180 / Mathf.PI);
	}

	public static float toRad(float deg)
	{
		return deg * (Mathf.PI / 180);
	}

	// normalizes an angle so its in the range [0, 360)
	public static float angleNorm360(float deg)
	{
		while(deg < 0) deg += 360;
		while (deg >= 360) deg -= 360;
		return deg;
	}

	// normalizes an angle so its in the range [-180, +180)
	public static float angleNorm180(float deg)
	{
		while (deg < -180) deg += 360;
		while (deg >= 180) deg -= 360;
		return deg;
	}

	// linear interpolator
	public static float linearInterp(float A, float B, float AB, float a, float b)
	{
		float u = (AB - A) / (B - A);
		return a + (b - a) * u;
	}
    public static Vector3 linearInterp(Vector3 A, Vector3 B, float blend)
    {
        return blend * A + (1 - blend) * B;
    }

}
