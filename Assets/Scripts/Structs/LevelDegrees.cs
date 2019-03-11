using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDegrees
{
    private int[] scores;
    private int currentScore;
    public LevelDegrees(int optimumScore, int mediumScore, int lowScore)
    {
        scores = new int[3] { optimumScore, mediumScore, lowScore };
        currentScore = 0;
    }

    public void IncreaseCurrentScore()
    {
        currentScore++;
    }

    public int GetLevelDegree()
    {
        if(currentScore <= scores[0])
        {
            return 3;
        }else if(currentScore > scores[0] && currentScore <= scores[1])
        {
            return 2;
        }else if(currentScore > scores[1] && currentScore <= scores[2])
        {
            return 1;
        }
        return 0;

    }
}
