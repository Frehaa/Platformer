## Different states of the player character

The player character can be in one of multiple different states depending on user input and other factors such as position or ground type stood on. The player behaves differently in each state and it is limited which states can transition to which states.

#### Player states

##### 1. Idle

Default state. This is the state of the player character when no user input is given and the player is unmoving.

##### 2. Running

Simple movement state. This is the state of the player when told to run left or right on a flat surface.

##### 3. Airborne

The state for any in air action whether it be jumping, falling or just having been throw into a wall at 100 m/s.

##### 4. Crouching
##### 5. Sliding
##### 6. WallGrab
##### 7. HighImpact

#### 8. Ragdoll?

This might be what I need for anything that isn't character movement.

##### First Iteration of the states

#### 1. Idle

Does nothing but waits for input. Idle can transition into Running, Jumping, Falling, Ducking. Falling from not standing on anything, the others from user input.

#### 2. Running
