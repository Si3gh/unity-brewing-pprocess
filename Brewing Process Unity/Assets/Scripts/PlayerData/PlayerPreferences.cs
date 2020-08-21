using System;
using UnityEngine;

namespace PlayerData
{
    public static class PlayerPreferences
    {
        public static Guid GetPlayerId()
        {
            if (!PlayerPrefs.HasKey(PlayerPreferencesKey.UserId))
            {
                PlayerPrefs.SetString(PlayerPreferencesKey.UserId, Guid.NewGuid().ToString());
                PlayerPrefs.Save();
            }
            return PlayerPrefs.HasKey(PlayerPreferencesKey.UserId)
                ? Guid.Parse(PlayerPrefs.GetString(PlayerPreferencesKey.UserId))
                : Guid.Empty;
        }
    }
}