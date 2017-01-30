using UnityEngine;

public class SumScore {

    public static int Score { get; protected set; }
    public static int HighScore { get; set; }
    private SumScore () { }

    public static void Add (int pointsToAdd) {
        Score += pointsToAdd;
    }

    public static void Subtract (int pointsToSubtract) {
        Add(-pointsToSubtract);
    }

    public static void Reset () {
        Score = 0;
    }

    public static void SaveHighScore () {
        if (Score > HighScore) {
            HighScore = Score;
            PlayerPrefs.SetInt("sumHS", Score);
        }
    }

    public static void ClearHighScore () {
        PlayerPrefs.DeleteKey("sumHS");
        HighScore = 0;
    }
}
