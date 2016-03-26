using UnityEngine;
using System.Collections;

namespace Completed
{
    public class OpenDoor : MonoBehaviour
    {
        private Animator animator;
        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Check if the tag of the trigger collided with is Exit.
            if (other.tag == "Player")
            {
                //Set the trigger for the player animator to transition to the playerHit animation.
                animator.SetTrigger("Open");
            }
        }
    }
}