namespace Game.Scripts
{
    public static class WeaponHelpers
    {
        public static string GetAnimationNameFor(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.Gun:
                    return "Shoot";
                case WeaponType.Bat:
                    return "Strike";
            }

            return null;
        }
    }
}