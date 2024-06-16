namespace Ecs
{
    [System.Serializable]
    public struct ResourceSpendingComponent
    {
        public float fuelSpending;
        public float electricityLazerSpending;
        
        public float electricityGenerationRate;
    }
}