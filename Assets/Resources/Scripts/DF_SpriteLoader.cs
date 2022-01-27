using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DF_SpriteLoader : MonoBehaviour
{
    public GameObject avatar;

    public void SetAvatar(string name)
    {
        avatar.GetComponent<Image>().sprite = Resources.Load<Sprite>("Graphics/Avatars/" + name);
    }
}
