# Assignment2
 
### Team members: Simone Badaruddin, Nithi Deivanayagam, Inna Gruneva, Lily Logan

# Implementations

### Dot Product - Implemented by Simone Badaruddin
Implemented Emoji Reaction system for character (he has a lot of emoitons)! The character receives a shy emoji floating animatedly above their head when in the bathroom, facing the ghost in the shower. The character similarly receives a scared emoji when close to and facing an enemy. The character facing each trigger is calculated using dot product. Modified files include EmojiReactor.cs, ScaryRmojiHandler.cs and updates to Observer.cs.


### Linear Interpolation - Implemented by Lily Logan
Added functionality for the player to rotate smoothly. Can reference code in Scripts/PlayerMovement.cs



### New Particle Effect - Implemented by Inna Gruneva
Added particle system at light objects. 
Used an empty game object with a rigidbody and collider to trigger the particle system when the player comes up to the light. 
The empty game object holds a script called Firefly Particle Collision 2 that holds functionality. 



### New Sound Effect - Implemented by Nithi Deivanayagam
Added laser sounds to the entrances to most of the rooms. When the player walks into a room, they will hear a laser sound, but it's spread out, so the player won't know what rooms are going to make that sound. Can reference code in Scripts/PlaySound.cs

