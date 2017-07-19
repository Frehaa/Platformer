## Todo list

* Write more documentation!

* Rework Raycasting to fix potential errors where the player moves past collidable objects when the object is diagonally between the player and the move-to position. (potentially only have one raycast with rays in the direction of the player velocity)

* Make jumping feel good

* Allow for state change based on outside manipulation of velocity (ex. when jumping and send flying the player shouldn't be in a jump state but a "falling" state)

* Sliding

* Particle effects for HighImpact

* Slope Running

  Slopes are difficult because they can gradually increase which results in moving up one slope and through the next if not careful. Also there needs to be taken care not to just continue along the slope when the slope ends. So when moving up a slope the player should continue to the top of the slope, and any leftover velocity should be used to move the player further along the direction they are going.

  Note: If the player would end one upwards going slope and immediately following is a downwards going slope. Would it ever matter very much that the velocity is precise? If the speed is so high that it is noticeable that the player rounded the peak in a single frame or whatnot, shouldn't the player be moving so fast that they shouldn't seamlessly run down the next slope but instead just fly off the slope to begin with.

  Note: What about slope peaks that end in a drop.

* WallGrab

* Transitions to HighImpact

* Rework input
