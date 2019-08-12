# Arcade Cabinet Game Switcher for .NET

A .NET Framework application built for Windows devices, the purpose of this program is to be able to easily switch games at the press of a single button. For example, if you have a home built arcade cabinet and you only have an arcade controller, but an extra button or two to switch through games you can use this program so that you do not have to go back to your mouse/keyboard to switch games and you do not have to see the windows interface. Programs like this exist for MAME games but I could not find one for more obscure games like IIDX, K-shoot, USVC, Popn'music, Lunatic Rave, etc. On button press this will switch to the next game in the specified list and give you a countdown from 5, once the countdown is down it will attempt to gracefully exit the game, wait for the game to completely close, then open the next game. You can cycle to any game you don't just have to go to the next in the list.

## Basic Usage

There is some setup to do before you use this program, .Net Framework 4.5 is required.

1. Add your games to CycleThroughGames.cs like below
2. Add Images to resources folder

```cs
private List<Image> gamePictures = new List<Image> {
    Properties.Resources.SDVX_VIVID_WAVE,
    Properties.Resources.POPN_MUSIC
    };

private Dictionary<string, string> games = new Dictionary<string, string> {
        { "usc-game", @"C:\Users\burningsunrise\Desktop\USDVXC\usc-game.exe" },
        { "notepad", @"C:\Windows\System32\Notepad.exe" }
    };
```


## TODO

- [x] Able to close and open any game, not just the previous open game
- [x] Close game with GUI cleanly
- [ ] Need a arcade controller to find keyCode for IsUp and isDown
- [ ] Transition more pretty (Maybe a blur transition)
- [ ] Have a shadow for the countdown instead of a block of black
- [ ] Clean up comments when I get arcade machine

If there are any questions, bugs, or feature requests please open an issue on gitlab! :dog:
