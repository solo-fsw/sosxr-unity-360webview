using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class ButtonButton : MonoBehaviour
{
    private Button _button;


    private void Awake()
    {
        _button = GetComponent<Button>();
    }


    [ContextMenu(nameof(Click))]
    public void Click()
    {
        _button.onClick.Invoke();
    }
}