namespace smartkantin.Tools
{
    public static class GuidHelper
    {
        public static Guid ParseGuid(string guid)
        {
            try
            {
                return Guid.Parse(guid);
            }
            catch (Exception e)
            {
                Console.WriteLine("parse guid failed: " + guid);
                Console.WriteLine("example of valid guid: " + Guid.NewGuid().ToString());
                Console.WriteLine(e);
            }
            return Guid.Empty;
        }
    }
}