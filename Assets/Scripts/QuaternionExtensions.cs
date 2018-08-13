using UnityEngine;

public static class QuaternionExtensions
{
    public static Quaternion AlignToRightAngles(this Quaternion rotation)
    {
        return Quaternion.Euler(Mathf.Round(rotation.eulerAngles.x / 90) * 90, Mathf.Round(rotation.eulerAngles.y / 90) * 90, Mathf.Round(rotation.eulerAngles.z / 90) * 90);
    }
}
