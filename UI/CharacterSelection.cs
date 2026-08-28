using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private int _characterId = 0;
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _selectButton;

    [SerializeField] private bool[] _purshasedCharacter;
    [SerializeField] private int[] _priceCharacter;

    [SerializeField] private TextMeshProUGUI _bankText;

    private void OnEnable()
    {
        SetupSkinInfo();
    }

    private void OnDisable()
    {
        _selectButton.SetActive(false);
    }
    private void SetupSkinInfo()
    {
        _purshasedCharacter[0] = true;

        for (int i = 1; i < _purshasedCharacter.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("CharacterPurshased" + i) == 1;
            if (unlocked)
            {
                _purshasedCharacter[i] = true;
            }
        }

        _bankText.text = "Bank:"+ PlayerPrefs.GetInt("TotalFruitCollected").ToString();

        _selectButton.SetActive(_purshasedCharacter[_characterId]);
        _buyButton.SetActive(!_purshasedCharacter[_characterId]);
        if (!_purshasedCharacter[_characterId])
            _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: "+_priceCharacter[_characterId].ToString();

        anim.SetInteger("character_id", _characterId);
    }
    //increment character_id for selecting next character
    public void NextCharacter()
    {
        _characterId++;
        if (_characterId > 3)
        {
            _characterId = 0;
        }
        AudioManager.instance.PlaySFX(6);
        SetupSkinInfo();
    }


    //decrement character_id for selecting preview character
    public void PreviewCharacter()
    {
        _characterId--;
        if(_characterId < 0) 
        {
            _characterId = 3;
        }
        AudioManager.instance.PlaySFX(6);
        SetupSkinInfo();
    }

    //This function is for verifying if the player has enough amount of fruits 
    public bool HasEnoughtMoney()
    {
        int totalFruit = PlayerPrefs.GetInt("TotalFruitCollected");
        if(totalFruit >= _priceCharacter[_characterId])
        {
            totalFruit = totalFruit  - _priceCharacter[_characterId];
            AudioManager.instance.PlaySFX(6);
            PlayerPrefs.SetInt("TotalFruitCollected", totalFruit);
            return true;
        }
        AudioManager.instance.PlaySFX(7);
        return false;
    }
    public void Buy()
    {
        if(HasEnoughtMoney() == true)
        { 
            PlayerPrefs.SetInt("CharacterPurshased"+  _characterId, 1);
            SetupSkinInfo() ;
            _purshasedCharacter[_characterId] = true;
        }
        else
        {
            Debug.Log("You dont have enough fruit");
        }
    }

    //function for selecting a character
    public void Select()
    {
        PlayerManager.instance.choosenCharacterId = _characterId;
    }

    //function that switch the button of select button
    public void SwitchSelectButton(GameObject newButton)
    {
        _selectButton = newButton;
    }

}
