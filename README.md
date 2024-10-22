# Wiper - Terraria Plugin

Wiper is a Terraria server plugin designed to automatically detect and clear trash items from the game world. This plugin provides server admins with configurable options such as the time interval between checks, the number of items required to activate the wiper, and the ability to customize the wiper's name and color.

## Features

- Automatically clears a configurable number of inactive items.
- Customizable wiper name and color in the chat.
- Configurable intervals for item checking and clearing time.
- Real-time feedback when the wiper is activated.
- Logging of invalid color values with default fallback.

## Installation

1. Download the plugin `.dll` file and place it in your Terraria server's `ServerPlugins` folder.
2. Start your Terraria server with TShock installed.
3. A `Wiper.json` configuration file will be created in the `tshock` folder.
4. Edit the `Wiper.json` file to customize the settings as needed.

## Configuration

The `Wiper.json` file allows you to customize various settings:

- **enabled**: Enable or disable the Wiper.  
  Default: `true`
  
- **wiperName**: The name of the wiper in the chat when clearing items.  
  Default: `"Wiper"`
  
- **wiperColor**: The color of the wiper's name. Available colors: `Black`, `Red`, `Green`, `Gray`, `White`, `Orange`, `Yellow`, `Purple`, `Brown`.  
  Default: `"Red"`
  
- **checkIntervalSecond**: How often (in seconds) the plugin checks for items to clear.  
  Default: `3`
  
- **timeUntilClearSecond**: How many seconds the server waits before clearing the items once triggered.  
  Default: `10`
  
- **numberOfItemsToActivateTheWiper**: The minimum number of items that must exist before the wiper activates.  
  Default: `150`

Example `Wiper.json` configuration:
```json
{
  "enabled": true,
  "Wiper Name": "Wiper",
  "Wiper Color": "Red",
  "Check Interval in Second": 3,
  "Time Until Clear in Second": 10,
  "Number of Items to Activate The Wiper": 150
}
```

# How It Works
## Item Checking
The plugin monitors the number of active items on the server using a configurable check interval. If the number of items exceeds the `numberOfItemsToActivateTheWiper` setting, a countdown begins before the items are wiped.

## Item Clearing
The plugin sends a message to all players informing them of the number of items that will be cleared. After the countdown, the items are removed from the game world, and the players are notified again.

## Color Tagging
The plugin allows customization of the wiper's chat messages through the `wiperColor` option. If an invalid color is specified, the plugin defaults to `Red` and logs a warning in the server console.

# Contributing
If you would like to contribute to the development of Wiper, feel free to fork the repository, submit issues, or create pull requests. Contributions are always welcome!

# License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/KeyouXZ/Wiper/blob/main/LICENSE) file for details.