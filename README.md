## Arcade Cabinet Game Switcher for .NET
A .NET Framework application built for Windows devices, the purpose of this program is to be able to easily switch games at the press of a single button. For example, if you have a home built arcade cabinet and you only have an arcade controller, but an extra button or two to switch through games you can use this program so that you do not have to go back to your mouse/keyboard to switch games and you do not have to see the windows interface. Programs like this exist for MAME games but I could not find one for more obscure games like IIDX, K-shoot, USVC, Popn'music, Lunatic Rave, etc. On button press this will switch to the next game in the specified list and give you a countdown from 5, once the countdown is down it will attempt to gracefully exit the game, wait for the game to completely close, then open the next game. You can cycle to any game you don't just have to go to the next in the list.

### Basic Usage
There is some setup to do before you use this program, .Net Framework 4.5 is required. Along with that in the CycleThroughGames.cs file you will see a List variable and a Dictionary variable, one called gamePictures and the other called games. You will need to add your gamePictures to the resources folder in your solution explorer, once you do this you can access it from the Properties.Resources and populate it in the list, this will put the picture in the list. After this you will need to add your games EXE name to the games dictionary key (without the .exe extension) and the games full path to the games dictionary value (with extension). Feel free to add as many games as you want, after that initial setup you will be done.


### TODO
- [x] Able to close and open any game, not just the previous open game
- [x] Close game with GUI cleanly
- [ ] Need a arcade controller to find keyCode for IsUp and isDown
- [ ] Transition more pretty (Maybe a blur transition)
- [ ] Have a shadow for the countdown instead of a block of black
- [ ] Clean up comments when I get arcade machine