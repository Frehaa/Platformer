What are the goals for the project, what does it need to be able to do, what is the vision?

1. The character should be able to be thrown around by forces. If hit by a potent attack the player character should fly through the air with "realistic" <b>physics</b>.

2. The character should react differently to the materials he interacts with. Ice is hard and has little friction and is therefore harder to move precise on. Mud is soft and has a lot of friction and is therefore harder to move fast on.

3. The character cannot die. The difficulty in the game lies in being able to execute something hard (like a boss fight) without failing. Instead of a hard reset by death, the level and mob design should instead be "reset" dynamically if the player makes a mistake.

4. The character is little and nimble.

5. The player and enemies can attack each other.

6. Items the player is using should be able to affect the players movement. (i.e. if the player is using a giant sword, it might be hard to jump, but if the sword is used to hit the ground, the character is sent up in the air, effectively jumping.)

7. Same as the player the enemies should be able to be thrown by forces and should react to the materials they interact with.

8. Monster though do not need to behave differently depending on gear(?)

9. Monsters have different attacks patterns and has to be beat a certain way.

10.

###### Physics

By "realistic" physics I mean that the character is slowed down by air resistance and fall dawn because of gravity. There should be a soft limit to how fast the player can move horizontally or vertically and beyond that limit the player is slowed down. This means that if the player is falling from a high place, the velocity won't just increase into infinity but will reach a maximum speed. And if the player is sent flying though the air at 100 units/s, the speed will decrease to below the soft limit.

#### Potential classes

<b>Player</b>
 * velocity

<b>Equipment</b>


<b>Monster</b>

<b>Material</b>
 * Friction
 * Hardness
