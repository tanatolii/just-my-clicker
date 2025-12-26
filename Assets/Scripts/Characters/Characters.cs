using UnityEngine;

public class Characters : MonoBehaviour
{
    public void SetEmotion(string characterName, string face)
    {
        if (characterName == "tanatolii")
        {
            tanatolii t = transform.Find(characterName).GetComponent<tanatolii>();
            t.CurrentEmotion = face;
            Debug.Log(face);
        }
    }
    public void ShowHideCharacter(string characterName, bool state)
    {
        if (characterName == "tanatolii")
        {
            tanatolii t = transform.Find(characterName).GetComponent<tanatolii>();
            t.TurnOnTheBackGround(state);
        }
    }
}
