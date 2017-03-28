

$(document).ready(function () {
    InitilizeMarketList();
    GetMarketChartNearTermData('daily');
    GetMarketChartNearTermData('weekly');
    $(".btnGetData").click(function () {
        var selected = $(this);
        if (selected.length > 0 && selected.text() != '') {
            RebindMarketData(selected.text());
        }
    });
});

function InitilizeMarketList() {
    $.ajax({
        type: "GET",
        url: "/Home/GetMarketData",
        dataType: "html",
        data: { 'startDate': $('#date').val(), 'Type': $('#datafrequency').val(), 'symbol': $('#code').val() },
        success: function (data) {
            if (data != 'Fail') {
                var json = $.parseJSON(data);
                if (json.results.length > 0) {
                    if ($.fn.dataTable.isDataTable('#Id_MarketDataList')) {
                        $('#Id_MarketDataList').DataTable().destroy();
                    }
                    var table = $('#Id_MarketDataList').DataTable({
                        aaData: json.results,
                        "columns": [
                                 { "data": "symbol", "name": "symbol" },
                                 { "data": "timestamp", "name": "timestamp" },
                                 { "data": "tradingDay", "name": "tradingDay" },
                                 { "data": "open", "name": "open" },
                                  { "data": "high", "name": "high" },
                                   { "data": "low", "name": "low" },
                                    { "data": "close", "name": "close" },
                                     { "data": "volume", "name": "volume" },
                                      { "data": "openInterest", "name": "openInterest" }
                        ]
                    });
                    $(".customLoader").fadeOut("slow");
                }
            } else { alert('Code is wrong'); }
        },
        error: function (error) { }
    });
}

//function GetMarketChartData() {
//    $.ajax({
//        type: "GET",
//        url: "/Home/GetMarketChartData",
//        dataType: "json",
//        data: { 'startDate': $('#date').val(), 'Type': $('#datafrequency').val(), 'symbol': $('#code').val() },
//        success: function (data) {
//            if (data != '') {
//                for (i in data) {
//                    data[i][0] = new Date(data[i][0]).getTime();
//                }

//                Highcharts.stockChart('chartcontainer', {
//                    title: {
//                        text: 'IBM'
//                    },
//                    rangeSelector : {
//                        enabled: false
//                    },
//                    navigator: {
//                        enabled: false
//                    },
//                    series: [{
//                        type: 'ohlc',
//                        name: 'IBM',
//                        data: data
//                    }]
//                });
//            } else { alert('Code is wrong'); }
//        },
//        error: function (error) { }
//    });
//}

function RebindMarketData(selectedCode) {
    $.ajax({
        type: "GET",
        url: "/Home/GetAllMarketData",
        dataType: "json",
        async: true,
        data: { 'startDate': $('#date').val(), 'Type': $('#datafrequency').val(), 'selectedCode': selectedCode },
        success: function (data) {
            if (data != null) {
                var dailyData = $.parseJSON(data.MarketDataDaily);
                bindDatatable('Id_MarketDataList', dailyData.results);

                var weeklyData = $.parseJSON(data.MarketDataWeekly);
                bindDatatable('Id_MarketDataListWeekly', weeklyData.results);

                var monthlyData = $.parseJSON(data.MarketDataMonthly);
                bindDatatable('Id_MarketDataListMonthly', monthlyData.results);

                var quarterlyData = $.parseJSON(data.MarketDataQuartely);
                bindDatatable('Id_MarketDataListQuarterly', quarterlyData.results);

                var yearlyData = $.parseJSON(data.MarketDataYearly);
                bindDatatable('Id_MarketDataListYearly', yearlyData.results);

                $("#code").val(data.Code);
                $("#Name").val(data.Name);
                $("#ClosingPrice").val(data.ClosingPrice);
            }
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function bindDatatable(selector, data) {
    if ($.fn.dataTable.isDataTable('#' + selector)) {
        $('#' + selector).DataTable().destroy();
    }
    var table = $('#' + selector).DataTable({
        aaData: data,
        "columns": [
                 { "data": "symbol", "name": "symbol" },
                 { "data": "timestamp", "name": "timestamp" },
                 { "data": "tradingDay", "name": "tradingDay" },
                 { "data": "open", "name": "open" },
                  { "data": "high", "name": "high" },
                   { "data": "low", "name": "low" },
                    { "data": "close", "name": "close" },
                     { "data": "volume", "name": "volume" },
                      { "data": "openInterest", "name": "openInterest" }
        ]
    });
}



/******************************/


