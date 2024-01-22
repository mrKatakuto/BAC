using UnityEngine;

public static class PlayerPrefsDebugger
{
    public static void SetString(string key, string value)
    {
        Debug.Log($"[PlayerPrefs] SetString called with key='{key}', value='{value}'");
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public static string GetString(string key, string defaultValue = "")
    {
        if (PlayerPrefs.HasKey(key))
        {
            string value = PlayerPrefs.GetString(key);
            Debug.Log($"[PlayerPrefs] GetString called with key='{key}', returning value='{value}'");
            return value;
        }
        else
        {
            Debug.Log($"[PlayerPrefs] GetString called with key='{key}', but key does not exist. Returning default value='{defaultValue}'");
            return defaultValue;
        }
    }

    public static void SetFloat(string key, float value)
    {
        Debug.Log($"[PlayerPrefs] SetFloat called with key='{key}', value={value}");
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public static float GetFloat(string key, float defaultValue = 0.0f)
    {
        if (PlayerPrefs.HasKey(key))
        {
            float value = PlayerPrefs.GetFloat(key);
            Debug.Log($"[PlayerPrefs] GetFloat called with key='{key}', returning value={value}");
            return value;
        }
        else
        {
            Debug.Log($"[PlayerPrefs] GetFloat called with key='{key}', but key does not exist. Returning default value={defaultValue}");
            return defaultValue;
        }
    }

    public static void SetInt(string key, int value)
    {
        Debug.Log($"[PlayerPrefs] SetInt called with key='{key}', value={value}");
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public static int GetInt(string key, int defaultValue = 0)
    {
        if (PlayerPrefs.HasKey(key))
        {
            int value = PlayerPrefs.GetInt(key);
            Debug.Log($"[PlayerPrefs] GetInt called with key='{key}', returning value={value}");
            return value;
        }
        else
        {
            Debug.Log($"[PlayerPrefs] GetInt called with key='{key}', but key does not exist. Returning default value={defaultValue}");
            return defaultValue;
        }
    }

    public static bool HasKey(string key)
    {
        bool hasKey = PlayerPrefs.HasKey(key);
        Debug.Log($"[PlayerPrefs] HasKey called with key='{key}', returning {hasKey}");
        return hasKey;
    }

    public static void DeleteKey(string key)
    {
        Debug.Log($"[PlayerPrefs] DeleteKey called with key='{key}'");
        PlayerPrefs.DeleteKey(key);
    }

    public static void Save()
    {
        Debug.Log("[PlayerPrefs] Save called");
        PlayerPrefs.Save();
    }

    // Add other methods like SetBool or GetBool if you need them...
}
