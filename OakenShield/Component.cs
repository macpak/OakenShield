namespace OakenShield
{
    public class Component
    {
        public static TypeDescriptor<T> For<T>()
        {
            return new TypeDescriptor<T>();
        }
    }
}