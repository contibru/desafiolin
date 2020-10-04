using System;

namespace DesafioLin.Common
{
    public static class ValidateField
    {
        public static bool IsValid(string field)
        {
            if (field == "")
                return false;

            if (field == null)
                return false;

            return true;
        }

        public static bool IsValid(int? field)
        {
            if (field == 0)
                return false;

            if (field == null)
                return false;

            return true;
        }

        public static bool IsValid(uint? field)
        {
            if (field == 0)
                return false;

            if (field == null)
                return false;

            return true;
        }

        public static bool IsValid(int field)
        {
            if (field == 0)
                return false;

            return true;
        }

        public static bool IsValid(uint field)
        {
            if (field == 0)
                return false;

            return true;
        }

        public static bool IsValid(float? field)
        {
            if (field == 0)
                return false;

            if (field == null)
                return false;

            return true;
        }

        public static bool IsValid(decimal? field)
        {
            if (field == 0)
                return false;

            if (field == null)
                return false;

            return true;
        }

        public static bool IsValid(bool? field)
        {
            if (field == null)
                return false;

            if (field == false)
                return false;

            return true;
        }

        public static bool IsValid(DateTime? field)
        {
            if (field == null)
                return false;

            return true;
        }

        public static bool IsValid(DateTime field)
        {
            if (field == null)
                return false;

            return true;
        }
    }
}