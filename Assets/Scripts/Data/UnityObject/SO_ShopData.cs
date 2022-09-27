using System;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_ShopData", menuName = "Data/SO_ShopData", order = 0)]
    public class SO_ShopData : ScriptableObject
    {
        public ShopData shopdata;
    }
}