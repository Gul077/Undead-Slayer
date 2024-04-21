using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Ensure this is linked to your TMP component
    public static int score;
    public int targetScore = 100; // The score the player needs to reach

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "Score: " + score.ToString() + "/" + targetScore.ToString();

    }
}
