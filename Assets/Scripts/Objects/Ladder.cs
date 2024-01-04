using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Movement player = other.GetComponent<Movement>();
        if (player != null)
            player.CanClimb = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Movement player = other.GetComponent<Movement>();
        if (player != null)
            player.CanClimb = false;
    }
}
