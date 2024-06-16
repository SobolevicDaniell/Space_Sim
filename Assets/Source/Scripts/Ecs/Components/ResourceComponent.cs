namespace Ecs
{
    [System.Serializable]
    public struct ResourceComponent
    {
        public float maxFuel;
        public float currentFuel;
        public float maxElectricity;
        public float currentElectricity;
        public float maxMaterials;
        public float currentMaterials;

        public float minElectricityLazer;
    }
}