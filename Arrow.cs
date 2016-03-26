using UnityEngine;
using System.Collections;

namespace Completed
{
    public class Arrow : MovingObject 
    {
        // Public variable 
        public int speed = 6;
        public int wallDamage = 2;                  //How much damage an arrow does to a wall when it hits it.
        public bool attachedToPlayer = false;        //if arrow is on player
        public bool isFlying = false;
        public AudioClip drinkSound1;				//1 of 2 Audio clips to play when player collects a soda object.
        public AudioClip drinkSound2;				//2 of 2 Audio clips to play when player collects a soda object.
        public Vector2 launchDirection;
        
        private Transform player;	            //Transform to attempt to move toward each turn.

        //Start overrides the Start function of MovingObject
        protected override void Start()
        {
            GameManager.instance.AddArrowsToList(this);

            //Find the Player GameObject using it's tag and store a reference to its transform component.
            player = GameObject.FindGameObjectWithTag("Player").transform;

            //Call the Start function of the MovingObject base class.
            base.Start();
        }

        private void Update()
        {
            // if player is running with arrow
            if (!attachedToPlayer)
            {
                AttemptMove<Wall>(Mathf.RoundToInt(launchDirection.x), Mathf.RoundToInt(launchDirection.y));
            }
            //if ((Mathf.RoundToInt(rb2D.position.x) != Mathf.RoundToInt(player.position.x)) && )
            //{

            //}
        }

        public void FireArrow()
        {
            AttemptMove<Wall>(Mathf.RoundToInt(launchDirection.x), Mathf.RoundToInt(launchDirection.y));
            attachedToPlayer = false;
            isFlying = true;
        }
        
        //AttemptMove overrides the AttemptMove function in the base class MovingObject
        //AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            base.AttemptMove<T>(xDir, yDir);

            //Hit allows us to reference the result of the Linecast done in Move.
            RaycastHit2D hit;

            //If Move returns true, meaning Player was able to move into an empty space.
            if (Move(xDir, yDir, out hit))
            {
                //animator.SetTrigger("playerWalk");

                //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
                //SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
            }
        }

        //OnCantMove overrides the abstract function OnCantMove in MovingObject.
        //It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
        protected override void OnCantMove<T>(T component)
        {
            //var object = component.GetComponent
            ////Set hitWall to equal the component passed in as a parameter.
            Wall hitWall = component as Wall;

            ////Call the DamageWall function of the Wall we are hitting.
            hitWall.DamageWall(wallDamage);
        }

        //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
        private void OnTriggerEnter2D(Collider2D other)
        {
            //Check if the tag of the trigger collided with is Exit.
            if (other.tag == "Exit")
            {

            }

            //Check if the tag of the trigger collided with is Exit.
            if (other.tag == "Player")
            {
                //Call the RandomizeSfx function of SoundManager and pass in two drinking sounds to choose between to play the drinking sound effect.
                SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().isKinematic = false;
                attachedToPlayer = true;
            }

        }
    }
}