using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Item : MonoBehaviour, IItem
{
    public void Use()
    {
        Destroy(gameObject);
    }
}


public interface IItem
{
    public void Use();
}