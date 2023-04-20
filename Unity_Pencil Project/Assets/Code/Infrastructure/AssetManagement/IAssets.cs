﻿using Code.Infrastructure.Services;
using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path, Vector3 atPosition);
        GameObject Instantiate(string path);
    }
}