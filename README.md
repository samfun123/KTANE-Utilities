# KTANE Utilities
This repo has a few different scripts that may be useful for you if you're developing a KTANE mod. Check out the descriptions of the scripts below for more info.

# Non-Assembly Scripts
These can be used in any script. They don't depend on being in a custom assembly that has access to KTANE types.

## ModdedAPI.cs
Allows you to create and access modded APIs like exploding the bomb or getting the current mode. Documentation is inside the script.

## ModConfig.cs
A better version of KMModSettings. Allows you to specify the type of your settings and it will converting to and from the JSON. Example:
```csharp
ModConfig<TweakSettings> modConfig = new ModConfig<TweakSettings>("TweakSettings");
TweaksSettings settings = modConfig.Settings; // Read settings.
modConfig.Settings = settings; // Write settings.
```

## ReflectionHelper.cs
Includes many methods to make reflection much easier. Example:
```csharp
var timer = module.GetComponent("BombComponent").GetValue<object>("Bomb").CallMethod<object>("GetTimer");
var time = timer.GetValue<float>("TimeRemaining");
time = time / 2;
timer.SetValue("TimeRemaining", time);
```

# Assembly Scripts
The opposite of Non-Assembly scripts. Can only be used in a custom assembly that has access to KTANE types.
## LeaderboardController.cs
Allows you to disable the leaderboard while not conflicting with other mods. Documentation is inside the script.