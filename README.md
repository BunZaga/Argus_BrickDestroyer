# Argus_BrickDestroyer

I grabbed a laser component/renderer I had laying around from a previous 'bubble pop' project for the guidance laser.  I thought it would be cool to have a little guidance that helps a bit, but doesn't show every bounce.

I started with the turret and basic ball shooter.  I originally tried to control the physics and bouncing, but then ran into weird conditions where the ball could be above, below, or to the left/right of a brick and I wanted to control the bounce direction based on a normal, but didn't have access to the data.  So I scrapped everything and went with the built in Unity physX engine and let it auto-control the ball, which was way better.

I wanted to add aditional functionality to make the game more 'fun', so I added a Laser gun, and wanted to also add a Rocket gun, which would shoot two rockets to the spot the ball would normally 'bounce' but didn't get to it due to Easter planning/cooking.  I was able to get the Laser gun in, so I'm happy with that.

I started out using interfaces for the ammo and gun controls, but then realized I wanted some specific variables for some functions, while keeping some base functionality the same, so moved to abstract classes instead.

I wanted to create a 'Level Editor', but didn't get around to it.  Instead I just used txt files for level data.

I didn't add any sound, which is a shame, because I really think sound is almost as important as graphics and game play.

I went with 3d vs 2d, just because I think 3d is way cooler, even if it is limited to two axis.

I didn't get time to add 'destruction' effects for the bricks, that would have been cool.

For the ammo Icons, I didn't want to write different scripts for each type, so I created a base class, and created unique Unity scriptable object events that different classes could listen for or subscribe to.  This way I could use the same code, with unique events for each type.

I really don't like hard coding everything like popups, and one time events, etc.  So I'd rather move those out to be data driven.  As an example, the UI controller is aweful from my perspective, but it got the job done in the short time I had.

I think that is one HUGE thing I'd do differently, is make everything I can data or experience driven, rather than have everything created and disabled at runtime.  For example, I'd start the game out with one gun and the base bullet.  Once a thing is needed, I'd like to load it from disk, then add it dynamically, instead of having it all exist, but that's just the way I like to do things.

Another thing I'd want to change is to make the 'drops' from the bricks data driven too.  For example, some bricks grant laser ammo, and it's hard coded into the brick prefab itself.  I'd prefer to make that data driven as well, perhaps a 4th/5th variable which is a reference to a powerup type, and the amount it grants.

Overall, I enjoyed the project, and found myself laying in bed at night thinking of what I can do to make it more fun, or how I would code different things, etc.  I would do a lot of things different if it was a more long term project, but I'm happy with what I ended up with.

Towards the end, the coding got a little chaotic, with putting events and references primarily in the GameGrid, it basically became my Game Manager.  Also, I would rather load the level data from disk, instead of have it all referenced in the inspector... lots of things I would change.
