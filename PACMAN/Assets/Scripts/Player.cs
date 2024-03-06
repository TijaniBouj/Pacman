using System;
using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
    public Player playerController;
    public Player playerController1;
    public Player playerController2;
    public Player playerController3;

    private bool playerCollisionEnabled = false;
    public int coins;
    public float moveSpeed = 5f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            Debug.Log("Coin collected");
            coins = coins + 1;
            Debug.Log(coins);
            other.gameObject.SetActive(false);
            StartCoroutine(ReactivateCoinAfterDelay(other.gameObject, 2f));
            if (coins >= 2)
            {
                coins = 0;
                playerCollisionEnabled = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost") && playerCollisionEnabled)
        {
            playerController.DisableNavMeshAgent();
            playerController.ResetPlayerPosition();
        }
        if (other.CompareTag("Ghost1") && playerCollisionEnabled)
        {
            playerController1.DisableNavMeshAgent();
            playerController1.ResetPlayerPosition();
        }
        if (other.CompareTag("Ghost2") && playerCollisionEnabled)
        {
            playerController2.DisableNavMeshAgent();
            playerController2.ResetPlayerPosition();
        }
        if (other.CompareTag("Ghost3") && playerCollisionEnabled)
        {
            playerController3.DisableNavMeshAgent();
            playerController3.ResetPlayerPosition();
        }
    }

    private IEnumerator ReactivateCoinAfterDelay(GameObject coinToReactivate, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (coinToReactivate != null)
        {
            coinToReactivate.SetActive(true);
        }
    }
}