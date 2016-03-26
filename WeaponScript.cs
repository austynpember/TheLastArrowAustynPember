using UnityEngine;
using System.Collections;
namespace Completed
{
    /// Launch projectile
    public class WeaponScript : MonoBehaviour
    {
        public Transform shotPrefab;
        
        public float shootingRate = 0.25f;
        
        private Player player;	            //Transform to attempt to move toward each turn.

        private float shootCooldown;
        private float someScaleX;
        private float someScaleY;

        void Start()
        {
            player = GetComponent<Player>();
            someScaleX = transform.localScale.x;
            someScaleY = transform.localScale.y;
            shootCooldown = 0f;
        }

        void Update()
        {
            if (shootCooldown > 0)
            {
                shootCooldown -= Time.deltaTime;
            }
        }

        //--------------------------------
        // 3 - Shooting from another script
        //--------------------------------

        /// <summary>
        /// Create a new projectile if possible
        /// </summary>
        public void Attack(bool isEnemy)
        {
            if (CanAttack)
            {
                shootCooldown = shootingRate;
                // Create a new shot
                var shotTransform = Instantiate(shotPrefab) as Transform;

                // Assign position
                shotTransform.position = transform.position;

                // The is enemy property
                ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
                if (shot != null)
                {
                    shot.isEnemyShot = isEnemy;
                }

                // Make the weapon shot always towards it
                MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
                if (move != null)
                {
                    move.direction = player.lastMovedDirection; // towards in 2D space is the right of the sprite
                                                                //turn sprite
                    if (player.lastMovedDirection.x >= 0)
                    {
                        move.transform.localScale = new Vector2(someScaleX, move.transform.localScale.y);
                    }
                    else
                    {
                        move.transform.transform.localScale = new Vector2(-someScaleX, move.transform.localScale.y);
                    }

                    if (player.lastMovedDirection.y >= 0)
                    {
                        move.transform.localScale = new Vector2(someScaleY, transform.localScale.x);
                    }
                    else
                    {
                        move.transform.localScale = new Vector2(-someScaleY, transform.localScale.x);
                    }
                }
            }
        }

        /// <summary>
        /// Is the weapon ready to create a new projectile?
        /// </summary>
        public bool CanAttack
        {
            get
            {
                return shootCooldown <= 0f;
            }
        }
    }
}