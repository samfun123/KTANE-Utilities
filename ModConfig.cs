﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using UnityEngine;

class ModConfig<T>
{
    public ModConfig(string filename)
    {
		SettingsPath = Path.Combine(Path.Combine(Application.persistentDataPath, "Modsettings"), filename + ".json");
    }

	readonly string SettingsPath = null;

	public string SerializeSettings(T settings)
	{
		return JsonConvert.SerializeObject(settings, Formatting.Indented, new StringEnumConverter());
	}

	static readonly object settingsFileLock = new object();

	public T Settings
    {
        get
        {
            try
            {
				lock (settingsFileLock)
				{
					if (!File.Exists(SettingsPath))
					{
						File.WriteAllText(SettingsPath, SerializeSettings(Activator.CreateInstance<T>()));
					}

					T deserialized = JsonConvert.DeserializeObject<T>(
						File.ReadAllText(SettingsPath),
						new JsonSerializerSettings { Error = (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args) => args.ErrorContext.Handled = true }
					);
					return deserialized != null ? deserialized : Activator.CreateInstance<T>();
				}
            }
            catch (Exception e)
            {
				Debug.LogFormat("An exception has occurred while attempting to read the settings from {0}\nDefault settings will be used for the type of {1}.", SettingsPath, typeof(T).ToString());
				Debug.LogException(e);
                return Activator.CreateInstance<T>();
            }
        }

        set
        {
            if (value.GetType() == typeof(T))
            {
				lock (settingsFileLock)
				{
					try
					{
						File.WriteAllText(SettingsPath, SerializeSettings(value));
					}
					catch (Exception e)
					{
						Debug.LogFormat("Failed to write to {0}", SettingsPath);
						Debug.LogException(e);
					}
				}
			}
        }
    }

    public override string ToString()
    {
        return SerializeSettings(Settings);
    }
}