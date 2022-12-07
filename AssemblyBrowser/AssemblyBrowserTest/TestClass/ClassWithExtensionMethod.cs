namespace AssemblyBrowserTest
{
    public static class ClassWithExtensionMethod
    {
        public static int extensionMethod(this Tests t, int x)
        {
            return x;
        }
    }
}