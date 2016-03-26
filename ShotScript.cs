using UnityEngine;
using System.Collections;
namespace Completed
{

    public class ShotScript : MovingObject
    {
        // 1 - Designer variables

        /// <summary>
        /// Damage inflicted
        /// </summary>
        public int damage = 1;

        /// <summary>
        /// Projectile damage player or enemies?
        /// </summary>
        public bool isEnemyShot = false;

        public int wallDamage = 3;					//How much damage a player does to a wall when chopping it.

        protected override void Start()
        {
            // 2 - Limited time to live to avoid any leak
            Destroy(gameObject, 5f); // 20sec
        }

        //OnCantMove overrides the abstract function OnCantMove in MovingObject.
        //It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
        protected override void OnCantMove<T>(T component)
        {
            //Set hitWall to equal the component passed in as a parameter.
            Wall hitWall = component as Wall;

            //Call the DamageWall function of the Wall we are hitting.
            hitWall.DamageWall(wallDamage);
        }

        //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
        private void OnTriggerEnter2D(Collider2D other)
        {
            //Check if the tag of the trigger collided with is an Arrow.
            if (other.tag == "OuterWall")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
