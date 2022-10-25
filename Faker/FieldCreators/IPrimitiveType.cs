namespace FieldCreators
{
    public interface IPrimitiveTypeCreator : IType
    {
        object Create();
    }
}