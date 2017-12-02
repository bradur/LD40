// Author : bradur

using UnityEngine;

public class Tools : MonoBehaviour
{
    public static int IntParseFast(string value)
    {
        int result = 0;
        try
        {
            for (int i = 0; i < value.Length; i++)
            {
                char letter = value[i];
                result = 10 * result + (letter - 48);
            }
        }
        catch (System.NullReferenceException)
        {
            result = -1;
        }
        return result;
    }
}
