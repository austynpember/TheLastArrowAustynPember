using UnityEngine;
using System.Collections;

namespace Completed
{
    /// <summary>
    /// Simply moves the current game object
    /// </summary>
    public class MoveScript : MonoBehaviour
    {
        // 1 - Designer variables

        /// <summary>
        /// Object speed
        /// </summary>
        public Vector2 speed = new Vector2(3, 3);

        /// <summary>
        /// Moving direction
        /// </summary>
        public Vector2 direction = new Vector2(1, 0);

        private Rigidbody2D rb2D;				//The Rigidbody2D component attached to this object.

        private Vector2 movement;

        void Update()
        {
            // 2 - Movement
            movement = new Vector2(
              speed.x * direction.x,
              speed.y * direction.y);
        }

        void FixedUpdate()
        {
            // Apply movement to the rigidbody
            rb2D = GetComponent<Rigidbody2D>();
            rb2D.velocity = movement;
        }
    }
}