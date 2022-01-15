using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Text _text;

    public Button Button { get { return _button; } }

    public Image Image { get { return _image; } }

    public Text Text { get { return _text; } }
}
