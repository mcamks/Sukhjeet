using System;

namespace BluSignalHelpMethod
{
    public static class BluSignalComman
    {
        /*
         *http://ondemand.websol.barchart.com/getSignal.xml?symbols=QQQ&fields=trendspotterOpinion%2Cadi7DayOpinion%2Cparabolic50DaySignal&maxRecords=10 

http://marketdata.websol.barchart.com/getData.json?key=40ae86537412ad8042cbbaca14906020&symbol=QQQ&fields=trendspotterOpinion%2Cadi7DayOpinion%2Cparabolic50DaySignal&maxRecords=10 

http://marketdata.websol.barchart.com/getProfile.json?key=40ae86537412ad8042cbbaca14906020&symbol=QQQ&type=daily

http://marketdata.websol.barchart.com/getQuote.json?key=40ae86537412ad8042cbbaca14906020&symbol=QQQ

-- Fianl value
http://marketdata.websol.barchart.com/getQuote.json?key=40ae86537412ad8042cbbaca14906020&symbols=QQQ
         * */
        public static string APIkey { get { return "40ae86537412ad8042cbbaca14906020"; } }
        public static DateTime HistoricalDailyDataStartsWithDate { get { return DateTime.Now.AddMonths(-9); } }

        public static string DateTime9MonthBack { get { return DateTime.Now.AddMonths(-9).ToString("yyyyMMdd000000"); } }

        public static string DateTime9MonthWeeksBack { get { return DateTime.Now.AddMonths(-(9 * 43)).ToString("yyyyMMdd000000"); } }

    }
}
