using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Toggle : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    public bool isCollectable = false;
    public bool isEnd = false;
    private bool hasBeenCollected = false;
    private bool playerInRange;


    // Audio
    public AudioSource source;
    public AudioClip clip1;

    // Start is called before the first frame update

    void Start()
    {
        if (isCollectable)
        {
            UICounter.numCollect += 1; // How many are on the map
        }
        if (text1 != null) // Prevents Null reference
        {
            text1.enabled = false;
            text2.enabled = false;
        }
    }

    private void Update()
    {
        if (playerInRange && isCollectable && !hasBeenCollected && Input.GetKeyDown(KeyCode.E))
        {
            source.PlayOneShot(clip1);
            hasBeenCollected = true;
            UICounter.collectablesCount += 1;
            UICounter.animStart = true;
            StartCoroutine(Wait()); // Wait before destroy
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {

            if (isEnd && (UICounter.collectedALL)) // If you collected all the pieces
            {
                SceneManager.LoadScene("Win"); 
            }

            if (isEnd && (!UICounter.collectedALL)) // If you didn't
            {
                SceneManager.LoadScene("Win(Bad)");
            }

            if (isCollectable)
            {
                playerInRange = true;
            }


            if (text1 != null && text2 != null && !isCollectable)
            {
                text1.enabled = true;
                text2.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCollectable)
            playerInRange = false;


        if (other.gameObject.CompareTag("player") && text1 != null && text2 != null && !isCollectable) // Make sure its reusable without having to use this part
        {
            text1.enabled = false;
            text2.enabled = false;
        }
    }

    private IEnumerator Wait() // Script Fighting
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
