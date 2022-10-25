namespace FakerLib
{
    internal interface IFaker
    {
        T Create<T>();
    }
}