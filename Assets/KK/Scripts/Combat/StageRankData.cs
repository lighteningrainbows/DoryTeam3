using UnityEngine;

[System.Serializable]
public class StageRankData
{
    public float sTime;
    public float aTime;
    public float bTime;
    public float cTime;

    public Sprite sRankSprite;
    public Sprite aRankSprite;
    public Sprite bRankSprite;
    public Sprite cRankSprite;
    public Sprite dRankSprite;

    public string GetRank(float clearTime)
    {
        if (clearTime <= sTime) return "S";
        if (clearTime <= aTime) return "A";
        if (clearTime <= bTime) return "B";
        if (clearTime <= cTime) return "C";
        return "D";
    }

    public Sprite GetRankSprite(float clearTime)
    {
        string rank = GetRank(clearTime);

        switch (rank)
        {
            case "S": return sRankSprite;
            case "A": return aRankSprite;
            case "B": return bRankSprite;
            case "C": return cRankSprite;
            default: return dRankSprite;
        }
    }
}