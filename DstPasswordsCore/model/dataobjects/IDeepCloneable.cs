namespace DistributedPasswordsWPF.model.dataobjects
{
    internal interface IDeepCloneable<T>
    {
        T DeepClone();
    }
}