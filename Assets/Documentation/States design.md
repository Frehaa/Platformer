## State design

States are designed to on a given update:

 First handle any input and do stuff based on the input. For example change the state or intern variables.

 Secondly update whatever it is in charge of. For example the PlayerRunning state updates the player velocity based on the input from handled in the first step.

A state has responsibility for handling it's own velocity changes. When falling the fall state is responsible for stopping the fall when landing, so that it isn't up to

---

 Problem here is that if the first step changes to state and the second step is dependent on handling the input on their own, the update call wont work.

---

Alternative would be changing the state on next update, but that would result in lag between the input and the update. This wont do.

---

Solution could be to change the state and tell the new state what it needed to know, if anything. This is a problem with the falling state because we need to stop the velocity when the player hits the ground, but if we change our state on the update we can't change the velocity. Solution can be to make a changeStateOnNextUpdate and a changeStateNow function. Or an after transform function.


---

Could make a OnStateEnter and OnStateExit to deal with some of this.

#### How to hande multiple inputs in a state

Some inputs will change the state of the player before every input is handled resulting in other inputs overriding the first (and perhaps changing the velocity or other ugly stuff). The solution to this could be to have the handle input function call itself on the current player state to verify handle it. So if the player pressed jump and crouch at the same time, the command with the highest priority (as in the first in the if else if list) would take effect, and any subsequent input would be handled accordingly as if the player was in the other state.
