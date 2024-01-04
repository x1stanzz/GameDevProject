using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string _volumeName;
    [SerializeField] private string _textIntro; //Sound: or Music:
    private Text _txt;

    private void Awake()
    {
        _txt = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateVolume();
    }
    private void UpdateVolume()
    {
        float volumeValue = Mathf.RoundToInt(PlayerPrefs.GetFloat(_volumeName) * 100);
        _txt.text = _textIntro + volumeValue.ToString();
    }
}
