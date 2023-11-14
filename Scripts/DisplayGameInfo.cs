using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class DisplayGameInfo : MonoBehaviour
{

    // Sprites for displaying the lives remaining
    public Sprite fullHeart;
    public Sprite halfHeart;

    // Player game object
    public GameObject player;

    private void Start()
    {
        DisplayLivesAndScore();
    }

    private void Update()
    {
        DisplayLivesAndScore();
    }

    void DisplayLivesAndScore()
    {

        // Setting up the hearts
        Image[] childrenImages = GetComponentsInChildren<Image>();

        // Remaining player lives
        float remainingPlayerLives = player.GetComponent<PlayerController>().nbLives;

        // Remaining player full lives
        double fullLivesRemaining = Math.Truncate(remainingPlayerLives);

        // Is there a half life that need to be display ?
        bool halfLifeRemaining = remainingPlayerLives - fullLivesRemaining > 0;

        // For each of the image that displays the lives
        for (int i = 0; i < childrenImages.Length; i++)
        {
            Image childImage = childrenImages[i];
            childImage.enabled = true;

            if (i < fullLivesRemaining)
            {
                // Change the image sprite with the sprite fetched from the class DisplayGameInfo linked to the canvas
                childImage.sprite = GetComponent<DisplayGameInfo>().fullHeart;
            }
            else
            {
                if (halfLifeRemaining && i < remainingPlayerLives + 0.5 )
                {
                    // Change the image sprite with the sprite fetched from the class DisplayGameInfo linked to the canvas
                    childImage.sprite = GetComponent<DisplayGameInfo>().halfHeart;
                }
                else
                {
                    childImage.enabled = false;
                }
            }

        }

        // Setting up the score
        TextMeshProUGUI scoreText = GetComponentInChildren<TextMeshProUGUI>();
        float playerScore = player.GetComponent<PlayerController>().score;
        scoreText.text = playerScore.ToString();

    }
}
