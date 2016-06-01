namespace CollisionUtilities
{
    class Check
    {
        public static bool IgnoredTags(string checkTag, string[] ignored)
        {
            for(int i = 0; i < ignored.Length; i++)
            {
                if(checkTag == ignored[i])
                {
                    return true;
                }
            }
            return false;
        }
    }  
}