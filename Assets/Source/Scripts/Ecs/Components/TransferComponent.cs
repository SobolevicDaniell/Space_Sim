using Leopotam.Ecs;

public struct TransferComponent /*: IEcsAutoReset<TransferComponent>*/
{
    public float transferEnergyRate;
    public float transferFuelRate;
    public float transferMaterialRate;

    // public void AutoReset(ref TransferComponent c)
    // {
    //     c.transferEnergyRate = 0;
    //     c.transferFuelRate = 0;
    //     c.transferMaterialRate = 0;
    // }
}