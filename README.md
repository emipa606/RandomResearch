# [Random Research (Continued)]()

![Image](https://i.imgur.com/buuPQel.png)

Update of Uuuggggs mod https://steamcommunity.com/sharedfiles/filedetails/?id=1374065851
continued in their memory as they sadly passed away.

- Research benches info should no longer show the current research project until it should be visible

![Image](https://i.imgur.com/pufA0kM.png)
	
![Image](https://i.imgur.com/Z4GOv8H.png)

A scenario option for random research, with optional blind research: You don't even know what is being researched (until a certain amount of progress is made)

Note that you do need to **Turn it on** as a scenario option. Which is to say, you can keep this mod active while playing a non-random scenario.

There's a button to re-roll the research once you know what it is. If you want to do that, y'know. Research that was canceled can be continued.


When choosing a scenario, Click 'Scenario Editor', Check on 'Edit mode', Click 'Add part', Click 'Random Research' in the list to add it. To set the Blind Research percentage threshold, scroll down and find Random Research/"blind until progress"

Maybe for balance you'll want to also enable "Research Speed" and crank it up to compensate for the random. ("Add Part:Stat Multiplier", "Research Speed" is top right of the big list of stats)

---

And I mean obviously it doesn't research things you're not able to normally - requirements are checked. It even checks if you have the proper bench built, and pops up a warning if you need a better bench.

---

You can turn on Random Research without a new game by using the Development mode. Options Menu, Check on Development Mode. Press '/' Key to open the debug action list. "Make Research Random" should be last in the list. The blind % can be set by editing the save file: Add "blindThreshold0.5/blindThreshold" after the line "defRandomResearch/def"

Similarly, Random Research can be turned off from the Development mode. That is also required to make it safe to remove from the modlist.

And if you turn on God Mode, this is all disabled.

---

And hey, this supports ResearchTree, in that it prevents queuing and displaying in-progress things.

The ResearchTree GUI to re-roll and continue is workable - a cancel button floating where the queue would go, and you get two completion letters if you continue cancelled research. I blame ResearchTree for that, 

---

Hey, do you want research to be a little less random? Nope. Who's gonna design the system that chooses what to do, that satisfies everyone's requests for what they think is pseudo-random enough?

What you get is the ability to pause research (once you can see what it is)

---

Ludeon Forum (modlist) : https://ludeon.com/forums/index.php?topic=40701
GitHub source : https://github.com/alextd/RimWorld-RandomResearch

![Image](https://i.imgur.com/PwoNOj4.png)



-  See if the error persists if you just have this mod and its requirements active.
-  If not, try adding your other mods until it happens again.
-  Post your error-log using the [Log Uploader](https://steamcommunity.com/sharedfiles/filedetails/?id=2873415404) and command Ctrl+F12
-  For best support, please use the Discord-channel for error-reporting.
-  Do not report errors by making a discussion-thread, I get no notification of that.
-  If you have the solution for a problem, please post it to the GitHub repository.
-  Use [RimSort](https://github.com/RimSort/RimSort/releases/latest) to sort your mods


