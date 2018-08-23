namespace DstPasswordsCore.model.dataobjects
{
    internal interface IDeepCloneable<T>
    {
        T DeepClone();
    }
}