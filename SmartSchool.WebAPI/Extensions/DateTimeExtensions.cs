namespace SmartSchool.WebAPI.Extensions
{
    public static class DateTimeExtensions
    {
        static public int GetCurrentAge(this DateTime dateTime)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTime.Year;

            if(currentDate < dateTime.AddYears(age))
                age--;
            
            return age;
        }
    }
}