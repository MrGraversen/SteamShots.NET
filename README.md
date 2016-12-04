# SteamShots.NET

A Windows utility to disover, extract, and organize the arcaic hell that is Steam screenshots.

Extracted screenshots are copied to `%USERPROFILE%\Desktop\SteamShots.NET`.

### Dependencies and Requirements

* Depends on JSON.NET
* Requires a [valid Steam Web API key](https://steamcommunity.com/dev/apikey) (goes in`steam_api_key.txt` next to the executable)

### Process Overview

In short, the following is done to accomplish this task:

1. Discover the Steam installation path using the Windows registry (`HKEY_CURRENT_USER\Software\Valve\Steam, SteamPath`)
2. Discover any users (in 32 bit steam-id format)
3. Convert 32 bit steam-id to 64-bit steam id (required by the Steam Web API)
4. For each discovered user, discover any games with screenshots
5. Resolve each Steam app-id against the Steam Web API
6. Copy all screenshots to a nicely formatted filepath, for example like so: `SteamShots.NET\Kvaseren\Killing Floor\`

### So how about some visuals

**The process completed**

![SteamShots.NET](http://media.martinbytes.com/2016-11-27_01-54-27.png)

**Produces the following directory tree (excluding the files)**

![CMD](http://media.martinbytes.com/2016-11-27_02-21-17.png)

### Improvements

The utility is resistent to most sorts of errors.
There are some checks in place to ensure nothing bad happens.
Some error resistence is also an effect of external factors, for example if you put in an invalid API key, the user IDs simply don't resolve, etc.

Please feel free to add improvements to this utility.
