using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class CharacterSelectObject
{
  public Sprite splash;
  public string characterName;
  public Color characterColor;
}

public class char_select : MonoBehaviour
{
  [Header ("Animator")]
  [SerializeField] private Animator ani;
  
  [Header ("List of Characters")]
  [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>(); 

  [Header ("Sounds")]
  [SerializeField] private AudioSource cycle;
  [SerializeField] private AudioSource selected;
  [SerializeField] private AudioSource music;

    [Header ("UI References")]
  [SerializeField] private TextMeshProUGUI middleCharacterName;
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
        music.Play();
    UpdateCharacterSelectionUI();
        
  }

  public void LeftArrow()
  {
    cycle.Play();
    decreaseIndexes();
    UpdateCharacterSelectionUI();
    
  }

  public void RightArrow()
  {
    cycle.Play();
    increaseIndexes();
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
    ani.SetInteger("index",index);
    
    middleCharacterName.text = characterList[index].characterName;
    if(characterList[index].characterName =="Elite Knight")
        {

            ani.SetBool("isKnight", true);
            StartCoroutine(knight());
        }

        leftCharacterSplash.sprite = characterList[leftIndex].splash;
    leftCharacterName.text = characterList[leftIndex].characterName;
    leftBackgroundColor.color = characterList[leftIndex].characterColor;

    rightCharacterSplash.sprite = characterList[rightIndex].splash;
    rightCharacterName.text = characterList[rightIndex].characterName;
    rightBackgroundColor.color = characterList[rightIndex].characterColor;

       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftArrow();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightArrow();
        }
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
    selected.Play();
    Debug.Log("Pressed");
  }
    IEnumerator knight() { 
        
        yield return new WaitForSeconds(3f);
        ani.SetBool("isKnight",false);
        yield return 0;

    }
}
