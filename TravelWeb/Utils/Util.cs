namespace TravelWeb.Utils
{
    public class Util
    {
        public static DateTime? StringToDate(string dateString)
        {
            if (dateString == null)
                return null;
            return DateTime.ParseExact(dateString, "dd/MM/yyyy", null);
        }

        public static string DateToString(DateTime? dateTime)
        {
            if (dateTime == null)
                return "";

            DateTime date = (DateTime)dateTime;
            string str = string.Format("{0:D2}/{1:D2}/{2}", date.Day, date.Month, date.Year);
            return str;
        }
    }
}
