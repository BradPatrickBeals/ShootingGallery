using System;
using UnityEngine;

namespace Fireblade
{
    [Serializable, CreateAssetMenu(fileName = "Readme", menuName = "Fireblade/Readme")]
    public class Readme : ScriptableObject
    {
        // Fields *****************************************************************
        public Texture2D icon;
        public string title;
        public Section[] sections;
        public bool loadedLayout;

        // Types ******************************************************************
        [Serializable]
        public class Section
        {
            public string heading, text, linkText, url;
        }
    }
}
