using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BestScoreDisplay : MonoBehaviour {

    // Use this for initialization
    void Start() {
        Text score = GetComponent<Text>();
        score.text = PlayerPrefs.GetInt("HScore").ToString();
 
        ScoreKeeper.Reset();
    }
}
