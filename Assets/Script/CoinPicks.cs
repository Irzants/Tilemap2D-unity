using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPicks : MonoBehaviour
{

    [SerializeField] AudioClip coinPick;
    [SerializeField] int pointsForCoin = 100;


    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsForCoin);
            AudioSource.PlayClipAtPoint(coinPick, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
