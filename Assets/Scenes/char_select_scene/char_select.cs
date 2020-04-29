using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class char_select : MonoBehaviour
{
  
  [Header ("List of Characters")]
  [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>(); 

  [Header ("Sounds")]
  [SerializeField] private AudioClip cycle;
  [SerializeField] private AudioClip selected;

  [Header ("UI References")]
  [SerializeField] private TextMeshProUGUI middleCharacterName;
  [SerializeField] private Image middleCharacterSplash;
  [SerializeField] private Image middleBackgroundColor;
  [SerializeField] private TextMeshProUGUI leftCharacterName;
  [SerializeField] private Image leftCharacterSplash;
  [SerializeField] private Image leftBackgroundColor;
  [SerializeField] private TextMeshProUGUI rightCharacterName;
  [SerializeField] private Image rightCharacterSplash;
  [SerializeField] private Image rightBackgroundColor;
  
  private int index;
  private int leftIndex;
  private int rightIndex;

  private void Start() 
  {
    UpdateCharacterSelectionUI();
  }

  public void LeftArrow()
  {
    decreaseIndexes();
    AudioManager.Instance.PlaySFX(cycle);
    UpdateCharacterSelectionUI();
  }

  public void RightArrow()
  {
    increaseIndexes();
    AudioManager.Instance.PlaySFX(cycle);
    UpdateCharacterSelectionUI();
  } 

  private void UpdateCharacterSelectionUI()
  {

    if(index == 0) {
      leftIndex = characterList.Count - 1;
      rightIndex = 1;
    } else if(index == characterList.Count - 1) {
      leftIndex = index - 1;
      rightIndex = 0;
    } else {
      leftIndex = index - 1;
      rightIndex = index + 1;
    }

    middleCharacterSplash.sprite = characterList[index].splash;
    middleCharacterName.text = characterList[index].characterName;
    middleBackgroundColor.color = characterList[index].characterColor;
    
    leftCharacterSplash.sprite = characterList[leftIndex].splash;
    leftCharacterName.text = characterList[leftIndex].characterName;
    leftBackgroundColor.color = characterList[leftIndex].characterColor;

    rightCharacterSplash.sprite = characterList[rightIndex].splash;
    rightCharacterName.text = characterList[rightIndex].characterName;
    rightBackgroundColor.color = characterList[rightIndex].characterColor;    


  }


  private void increaseIndexes() {
    if (index == characterList.Count - 1) {
      index = 0;
    } else {
      index++;
    }
  }

  private void decreaseIndexes() {
    if (index == 0) {
      index = characterList.Count - 1;
    } else {
      index--;
    }
  }

  public void select() 
  {
    
  }

}

[System.Serializable]
public class CharacterSelectObject
{
  public Sprite splash;
  public string characterName;
  public Color characterColor;
}