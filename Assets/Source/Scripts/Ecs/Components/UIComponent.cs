using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ecs
{
    [System.Serializable]
    public struct UIComponent
    {
        public TMP_Text dockText;

        public Slider fuelSlider;
        public Slider electricitySlider;
        public TMP_Text materialsText;
        public TMP_Text stabText;

        public GameObject statMenu;
        public GameObject otherStatMenu;
        
        public TMP_Text fuelProductionText;
    }
}