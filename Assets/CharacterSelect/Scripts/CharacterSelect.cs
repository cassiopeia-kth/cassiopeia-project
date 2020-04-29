using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    private int selectedCharacterIndex;
    private Color desiredColor;

    [Header("List of Characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>();

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image backgroundColor;

    [Header("Sounds")]
    [SerializeField] private AudioClip arrowClickSFX;
    [SerializeField] private AudioClip characterSelectMusic;

    [Header("Tweaks")]
    [SerializeField] private float backgroundColorTransitionSpeed = 10.0f;

    private void start()
    {
        UpdateCharacterSelectionUI();
        //Music should be played here
    }

    private void update()
    {   
        //This function changes the srite background color over time
        backgroundColor.color = Color.Lerp(backgroundColor.color, desiredColor, Time.deltaTime * backgroundColorTransitionSpeed);
    }

    public void leftArrow()
    {
        //function for left button
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
            selectedCharacterIndex = characterList.Count - 1;

        UpdateCharacterSelectionUI();
    }

    public void rightArrow()
    {   
        //function for right button
        selectedCharacterIndex++;
        if (selectedCharacterIndex ==characterList.Count)
            selectedCharacterIndex = 0;

        UpdateCharacterSelectionUI();
    }

    public void confirmButton()
    {   
        //function for confirm button
        Debug.Log(string.Format("Character {0}:{1} has been chosen", selectedCharacterIndex, characterList[selectedCharacterIndex].characterName));
    }

    private void UpdateCharacterSelectionUI()
    {
       characterSplash.sprite = characterList[selectedCharacterIndex].splash;
       characterName.text = characterList[selectedCharacterIndex].characterName;
        desiredColor = characterList[selectedCharacterIndex].characterColor;

    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash;
        public string characterName;
        public Color characterColor;
    }

}
