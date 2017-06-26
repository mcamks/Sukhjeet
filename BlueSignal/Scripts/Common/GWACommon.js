$(document).ready(function () {
    GetToggleTickerState();
});

function ShowLoader()
{ $(".customLoader").fadeIn("slow"); }


function HideLoader()
{ $(".customLoader").fadeOut("slow"); }

function ShowLoaderCustom() {
    $('#loading').modal('show');
    //$('#ModelBackGround_Custom').show(500);

    $('#loading').show();
    $('#ModelBackGround_Custom').show();

}

function HideLoaderCustom() {
    $('#loading').hide();
    $('#loading').modal('hide');
    $('#ModelBackGround_Custom').hide();
    $('.modal-backdrop').hide();

}

function OpenGWAModelPopup(modelTitle, Body) {
    $('#modalTitle').text(modelTitle);

    $('#modalBody').html('');
    $('#modalBody').html(Body);

    $('#myModal').modal('show');
}

function CloseGWAModelPopup(id) {

    $('#' + id).modal('hide');

}



function BindDropDown(filterData, dropdownID) {
    var url = "/Home/GetRecordsForSelection";
    dropdownID.html('');
    $.get(url, filterData, function (data) {
        if (data.Result == 200 && data.Data.length > 0) {
            $.each(data.Data, function () {
                dropdownID.append($("<option/>").val(this.Value).text(this.Text));
            });
        }
    });
}




function DisableElement(item) {
    if (item != undefined) {
        item.disabled = true;
    }
}
function EnableElement(item) {
    if (item != undefined) {
        item.disabled = false;
    }
}


/*****************/
function AddContact(btn) {

    if (btn != undefined) {
        btn.disabled = true;
    }

    var data = {
        'Name': $('#Name').val(),
        'Message': $('#Message').val(),
        'Subject': $('#Subject').val(),
        'Email': $('#Email').val()
    };

    $.ajax({
        type: "Post",
        url: "/Home/Contact",
        dataType: "html",
        async: true,
        data: data,
        success: function (data) {
            if (data != null) {
                $('#ContactLog_Container').html('');
                $('#ContactLog_Container').html(data);

                btn.disabled = false;
            }
        },
        error: function (error) {
            //console.log(error);
            btn.disabled = false;
            //alert('Error');
        }
    });
}

//
function LoginWithNew(btn) {


    if (btn != undefined) {
        btn.disabled = true;
    }

    var data = {
        'pwd': $('#pwd').val(),
        'cpwd': $('#cpwd').val()
    };

    $.ajax({
        type: "Post",
        url: "/Home/SetPassword",
        dataType: "html",
        async: true,
        data: data,
        success: function (data) {
            if (data != null) {
                $('#_loginBSPartial').html('');
                $('#_loginBSPartial').html(data);
                //$('#LoginForm').hide();

                btn.disabled = false;
            }
        },
        error: function (error) {
            //console.log(error);
            btn.disabled = false;
            //alert('Error');
        }
    });
}
function CloseThisLoginPopup() {
    $('#login_window').modal('hide');
}


/**********************Main JS***************************/

function activaTab(tab) {
    //if (!ValidateTheCodeInTextBox()) {
    //    return false;
    //}
    $('.nav-tabs a[href="#blufractal"]').tab('show');


};

function ValidateTheCodeInTextBox() {
    if ($('#code').val().trim().length == 0) {
        alert('Please select a symbol first.');
        $('#code').focus();
        return false;
    }


    return true;

}

function ValidateTheCodeInTextBoxWithBox(item) {
    if (item.val().trim().length == 0) {
        alert('Please select a symbol first.');
        item.focus();
        return false;
    }


    return true;

}

function InitilizeMarketList() {
    if (!ValidateTheCodeInTextBox()) {
        return false;
    }
    RebindMarketData($('#code').val(),1);
}

function InitilizeMarketListWithCode(item,id) {
    if (!ValidateTheCodeInTextBoxWithBox(item)) {
        return false;
    }

    RebindMarketData(item.val(),id);
}


function IntializeOnlyBlueQuante() {



    if ($('#BlueQuanteCode').val().trim().length == 0) {
        alert('Please select a symbol first.');
        $('#code').focus();
        return false;
    }

    RebindBuleQuentData($('#BlueQuanteCode').val());
}

function IntializeOnlyBlueQuanteWithCode(code) {
    //if ($('#BlueQuanteCode').val().trim().length == 0) {
    //    alert('Please select a symbol first.');
    //    $('#code').focus();
    //    return false;
    //}
    RebindBuleQuentData(code);
}


function GetMarketChartNearTermData(typeID) {
    var symb = $('#code').val();

    var customColor = ['#7787D0'];
    if (typeID == "dailySecond" || typeID == "dailyBulQuantData" || typeID == "dailyBlueNeural") {
        customColor = ['#f45b5b', '#8085e9', '#8d4654', '#7798BF', '#aaeeee', '#ff0066', '#eeaaee', '#55BF3B', '#DF5353', '#7798BF', '#aaeeee'];
    }

    ShowLoaderCustom();


    var rangeEnabled = true;

    var containterID = 'chartcontainer_' + typeID;
    $.ajax({
        type: "GET",
        url: "/Home/GetMarketChartData",
        dataType: "json",
        data: { 'startDate': $('#date').val(), 'Type': typeID, 'symbol': symb },
        success: function (data) {
            if (data != '') {
                for (i in data) {
                    data[i][0] = new Date(data[i][0]).getTime();
                }

                Highcharts.stockChart(containterID, {
                    title: {
                        text: symbolTitle
                    },
                    //rangeSelector: {
                    //    enabled: false
                    //},

                    colors: customColor,

                    rangeSelector: {
                        enabled: false
                    },

                    //rangeSelector: {
                    //    selected: 0
                    //},


                    navigator: {
                        enabled: false
                    },
                    series: [{
                        type: 'ohlc',
                        name: symb,
                        data: data,
                        pointInterval: 24 * 3600 * 1000
                    }],

                    exporting: { enabled: false },
                    credits: {
                        enabled: false
                    },

                    yAxis: {
                        lineWidth: 2,
                        offset: 20,
                        tickWidth: 1
                    },

                    xAxis: {
                        scrollbar: {
                            enabled: false
                        }
                    }


                });



                $('.highcharts-scrollbar').hide();
            } else { alert('Code is wrong'); }
            CloseGWAModelPopup2('myModal');


        },
        error: function (error) { }
    });
}

function GetAndBindMarketAllChartsData() {
    
    var symb = $('#code').val();
    //ShowLoaderCustom();
    var rangeEnabled = true;
    $.ajax({
        async: false,
        type: "GET",
        url: "/Home/GetMarketChartDataOnLoad",
        dataType: "json",
        data: { 'startDate': $('#date').val(), 'symbol': symb },
        success: function (response) {
            console.log(response);
            

            var data = response.dailyData;
            //var dailydataExmple = response.dailyData;
            //var tempdate = '6/23/2017 12:00:00 AM';
            //$.each(dailydataExmple, function (key, value) {
            //    debugger;
            //    tempdate, dateTimeParts = dateString.split(' '),
            //    timeParts = dateTimeParts[1].split(':'),
            //    dateParts = dateTimeParts[0].split('-'),
            //    date;
            //      dailydataExmple.tradingDay = new Date(dateParts[2], parseInt(dateParts[1], 10) - 1, dateParts[0], timeParts[0], timeParts[1]);

            //});
            console.log('--------------------------');
           // console.log(data.tradingDay);
            var data1 = response.weeklyData;
            var dataDaily = response.marketDataDaily;
            var avgVolume = response.avgVolume;
            var closingPrice = response.closingPrice;
            var prePricePercent = response.finalPercent;

            if (dataDaily != null) {
                //if ($("#Id_MarketDataListDaily").length > 0) {
                //    var dailyData = $.parseJSON(dataDaily);
                //    bindDatatable('Id_MarketDataListDaily', dailyData);
                //}

                if (avgVolume != "") {
                    var avgVol = CommaFormatted(parseFloat(avgVolume).toFixed(0));
                    if ($("#AvgVolume").length > 0)
                        $("#AvgVolume").val(avgVol);

                    if ($(".AvgVolumeClass").length > 0) {
                        $(".AvgVolumeClass").text(avgVol);
                        $(".AvgVolumeClass").val(avgVol);
                    }

                    if ($(".AvgVolumeClassbluequont").length > 0)
                        $(".AvgVolumeClassbluequont").text(avgVol);

                    if ($("#AvgVolumeQW").length > 0)
                        $("#AvgVolumeQW").val(avgVol);
                }

                if (closingPrice != "") {
                    if ($("#ClosingPriceQW").length > 0)
                        $("#ClosingPriceQW").val(closingPrice);

                    if ($("#ClosingPrice").length > 0)
                        $("#ClosingPrice").val(closingPrice);

                    if ($(".ClosingPriceClass").length > 0) {
                        $(".ClosingPriceClass").text(closingPrice);
                        $(".ClosingPriceClass").val(closingPrice);
                    }

                    if ($(".ClosingPriceClassbluequont").length > 0)
                        $(".ClosingPriceClassbluequont").text(closingPrice);
                }

                if (prePricePercent != "") {
                    $("#PreviousDayPercent").val(prePricePercent + "%");
                    $("#PreviousDayPercentQuant").val(prePricePercent + "%");
                    $("#PreviousDayPercentNatural").val(prePricePercent + "%");
                    $("#PreviousDayPercentCombo").val(prePricePercent + "%");
                }
                if(prePricePercent > 0)
                {
                   
                    $("#PreviousDayPercent").css('color', '#0c9215');
                    $("#PreviousDayPercentQuant").css('color', '#0c9215');
                    $("#PreviousDayPercentNatural").css('color', '#0c9215');
                    $("#PreviousDayPercentCombo").css('color', '#0c9215');

                    //$("#PreviousDayPercent").addClass('green_text');
                    //$("#PreviousDayPercentQuant").addClass('green_text');
                    //$("#PreviousDayPercentQuant").addClass('green_text');
                    //$("#PreviousDayPercentQuant").addClass('green_text');
                    
                }
                else {
                    $("#PreviousDayPercent").css('color', '#d31d0f');
                    $("#PreviousDayPercentQuant").css('color', '#d31d0f');
                    $("#PreviousDayPercentNatural").css('color', '#d31d0f');
                    $("#PreviousDayPercentCombo").css('color', '#d31d0f');
                    //$("#PreviousDayPercent").addClass('red_text');
                    //$("#PreviousDayPercentQuant").addClass('red_text');
                    //$("#PreviousDayPercentNatural").addClass('red_text');
                    //$("#PreviousDayPercentCombo").addClass('red_text');
                }

            }


            if (data != null) {
                for (i in data) {
                    data[i][0] = new Date(data[i][0]).getTime();
                }

                var customColor = ['#7787D0'];

                for (i in data1) {
                    data1[i][0] = new Date(data1[i][0]).getTime();
                }

                if ($("#chartcontainer_daily").length > 0)
                    BindCharts("chartcontainer_daily", data, symbolTitle, customColor, 1, false, true, true);
                    //BindCharts("chartcontainer_daily", data1, symbolTitle, customColor, 1, false, true, true);

                if ($("#chartcontainer_weekly").length > 0)
                    BindCharts("chartcontainer_weekly", data1, symbolTitle, customColor, 2, false, true, true);
                if ($("#chartcontainer2_weekly").length > 0)
                    BindCharts("chartcontainer2_weekly", data1, symbolTitle, customColor, 2, false, true, true);

                customColor = ['#f45b5b', '#8085e9', '#8d4654', '#7798BF', '#aaeeee', '#ff0066', '#eeaaee', '#55BF3B', '#DF5353', '#7798BF', '#aaeeee'];

                if ($("#chartcontainer_dailySecond").length > 0)
                    BindCharts("chartcontainer_dailySecond", data, symbolTitle, customColor, 1, false, true, true);
                if ($("#chartcontainer_dailySecond1").length > 0)
                    BindCharts("chartcontainer_dailySecond1", data, symbolTitle, customColor, 1, false, true, true);
                if ($("#chartcontainer_dailyBulQuantData").length > 0)
                    BindCharts("chartcontainer_dailyBulQuantData", data, symbolTitle, customColor, 1, false, true, true);
                if ($("#chartcontainer_dailyBuleFractal").length > 0)
                    BindCharts("chartcontainer_dailyBuleFractal", data, symbolTitle, customColor, 1, false, true, true);
                if ($("#chartcontainer_dailyBlueNeural").length > 0)
                    BindCharts("chartcontainer_dailyBlueNeural", data, symbolTitle, customColor, 1, false, true, true);

                $('.highcharts-scrollbar').hide();
            }
            else {
                alert('Code is wrong');
            }
            CloseGWAModelPopup2('myModal');
        },
        error: function (error) { }
    });
}

function BindCharts(containterID, data, symbolTitle, customColor, symb, rangeselectorValue, isRangeSelector, isNavigator, isexporting) {
   
        // split the data set into ohlc and volume
        //var ohlc = [],
        //    volume = [],
        //    dataLength = data.length,
        //    // set the allowed units for data grouping
        //    groupingUnits = [[
        //        'week',                         // unit name
        //        [1]                             // allowed multiples
        //    ], [
        //        'month',
        //        [1, 2, 3, 4, 6]
        //    ]],

        //    i = 0;

        //for (i; i < dataLength; i += 1) {
          
        //    ohlc.push([
        //        data[i][0], // the date
        //        data[i][1], // open
        //        data[i][2], // high
        //        data[i][3], // low
        //        data[i][4] // close
        //    ]);

        //    volume.push([
        //        data[i][0], // the date
        //        data[i][5] // the volume
        //    ]);
        //}


        //// create the chart
        //Highcharts.stockChart(containterID, {

        //    rangeSelector: {
        //        selected: 1
        //    },

        //    title: {
        //        text: symbolTitle
        //    },

        //    yAxis: [{
        //        labels: {
        //            align: 'right',
        //            x: -3
        //        },
        //        title: {
        //            text: 'OHLC'
        //        },
        //        height: '60%',
        //        lineWidth: 2
        //    }, {
        //        labels: {
        //            align: 'right',
        //            x: -3
        //        },
        //        title: {
        //            text: 'Volume'
        //        },
        //        top: '65%',
        //        height: '35%',
        //        offset: 0,
        //        lineWidth: 2
        //    }],

        //    tooltip: {
        //        split: true
        //    },

        //    series: [{
        //        type: 'candlestick',
        //        name: 'AAPL',
        //        data: ohlc,
        //        //dataGrouping: {
        //        //    units: groupingUnits
        //        //}
        //    }, {
        //        type: 'column',
        //        name: 'Volume',
        //        data: volume,
        //        yAxis: 1,
        //        //dataGrouping: {
        //        //    units: groupingUnits
        //        //}
        //    }]
        //});
  //  });

    Highcharts.stockChart(containterID, {
        chart: {
            zoomType: 'x',
            //type: 'column',
        },
        title: {
            text: symbolTitle
        },
        colors: customColor,
        //rangeSelector: {
        //    enabled: isRangeSelector,
        //    selected: rangeselectorValue
        //},
        rangeSelector: {

            buttons: [{
                type: 'day',
                count: 3,
                text: '3d'
            }, {
                type: 'week',
                count: 1,
                text: '1w'
            }, {
                type: 'month',
                count: 1,
                text: '1m'
            }, {
                type: 'month',
                count: 6,
                text: '6m'
            }, {
                type: 'year',
                count: 1,
                text: '1y'
            }, {
                type: 'all',
                text: 'All'
            }],
            selected: 5
        },
        navigator: {
            enabled: isNavigator,
            series: {
                data: data
            }
        },
        series: [{
            type: 'ohlc',
            name: symb,
            data: data,
           // pointInterval: 24 * 3600 * 1000,
            dataGrouping: {
                enabled: false
            }
        }],

        exporting: { enabled: isexporting },
        credits: {
            enabled: false
        },

        yAxis: {
            //lineWidth: 2,
            //offset: 20,
            //tickWidth: 1
            type: 'logarithmic',
            minorTickInterval: 0.1
        },

        xAxis: {
            scrollbar: {
                enabled: true
            }
        }
    });
}

function clearChart(typeID) {
    var containterID = '#chartcontainer_' + typeID;
    $(containterID).html('');
}




function CloseGWAModelPopup2(id) {

    $('#' + id).modal('hide');
    $('.close').click();
}
function RebindMarketData(selectedCode,id) {
    var a = $('#date').val(); var Type = $('#datafrequency').val();
    ShowLoaderCustom();
    $.ajax({
        type: "GET",
        url: "/Home/GetAllMarketData",
        dataType: "json",
        async: true,
        data: { 'startDate': $('#date').val(), 'Type': $('#datafrequency').val(), 'selectedCode': selectedCode, tabType:id },
        success: function (data) {
            if (data != null) {
               
                $("#txtMDDailyCode").val(selectedCode);
                ReloadDailyMarketData(false);
                //if ($("#Id_MarketDataListDaily").length > 0) {
                //    var dailyData = $.parseJSON(data.MarketDataDaily);
                //    $.each(dailyData.results, function (i, d) {
                //        d.volume = CommaFormatted(parseFloat(d.volume).toFixed(0));
                //    });

                //    bindDatatable('Id_MarketDataListDaily', dailyData.results);
                //}

                /* $$$$$$$$$$$$--Commented for now, by AJ ---------$$$$$$$$$$$$$$$$$ */

                //var weeklyData = $.parseJSON(data.MarketDataWeekly);
                //$.each(weeklyData.results, function (i, d) {
                //    d.volume = CommaFormatted(parseFloat(d.volume).toFixed(0));
                //});

                //bindDatatable('Id_MarketDataListWeekly', weeklyData.results);

                //var monthlyData = $.parseJSON(data.MarketDataMonthly);
                //$.each(monthlyData.results, function (i, d) {
                //    d.volume = CommaFormatted(parseFloat(d.volume).toFixed(0));
                //});
                //bindDatatable('Id_MarketDataListMonthly', monthlyData.results);

                //var quarterlyData = $.parseJSON(data.MarketDataQuartely);
                //$.each(quarterlyData.results, function (i, d) {
                //    d.volume = CommaFormatted(parseFloat(d.volume).toFixed(0));
                //});
                //bindDatatable('Id_MarketDataListQuarterly', quarterlyData.results);

                //var yearlyData = $.parseJSON(data.MarketDataYearly);
                //$.each(yearlyData.results, function (i, d) {
                //    d.volume = CommaFormatted(parseFloat(d.volume).toFixed(0));
                //});
                //bindDatatable('Id_MarketDataListYearly', yearlyData.results);

                /* $$$$$$$$$$$$--Commented for now, by AJ ---------$$$$$$$$$$$$$$$$$ */

                $("#liBluFractal").attr('data-original-title', data.FirstTabSymbol);
                $("#liBluNeural").attr('data-original-title', data.SecondTabSymbol);
                $("#liBluFractal_BluNeural").attr('data-original-title', data.ThirdTabSymbol);
                $("#liBluQuant").attr('data-original-title', data.ForthTabSymbol);

                var avgVol = CommaFormatted(parseFloat(data.AverageVolumn).toFixed(0));

                $("#code").val(data.Code);

                $(".codeValue").val(data.Code);
                $("#Name").val(data.Name);
                $("#ClosingPrice").val(data.ClosingPrice);
                $("#AvgVolume").val(avgVol);


                $(".codeClass").text(data.Code);
                $(".NameClass").text(data.Name);
                $(".ClosingPriceClass").text(data.ClosingPrice);
                $(".AvgVolumeClass").text(avgVol);


                $(".codeClass").val(data.Code);
                $(".NameClass").val(data.Name);

                $(".ClosingPriceClass").val(data.ClosingPrice);
                $(".AvgVolumeClass").val(avgVol);

                $(".codeClassbluequont").text(data.Code);
                $(".NameClassbluequont").text(data.Name);
                $(".ClosingPriceClassbluequont").text(data.ClosingPrice);
                $(".AvgVolumeClassbluequont").text(avgVol);



                if (data.SymbolNameData != undefined && data.SymbolNameData != null && data.SymbolNameData != '') {
                    var symbolData = $.parseJSON(data.SymbolNameData);
                    if (symbolData.status.code == 200 && symbolData.results.length > 0) {
                        symbolTitle = symbolData.results[0].name;
                        var sysmbolName = symbolData.results[0].name.replace(symbolData.results[0].symbol, "").trim();
                        $("#Name").val(sysmbolName);
                        $(".NameClass").text(sysmbolName);
                        $(".NameClass").val(sysmbolName);
                        $(".NameClassbluequont").text(sysmbolName);


                        $(".NameClassbluequont").val(sysmbolName);
                       
                        //var startDate = new Date(symbolData.results[0].tradeTimestamp);
                        //var formattedStartDate = startDate.getMonth() + "/" + startDate.getDate() + "/" + startDate.getFullYear();


                        $(".LastTradingDay").val(ConvertJsonDateString(symbolData.results[0].tradeTimestamp));
                        $(".LastTradingDay").text(ConvertJsonDateString(symbolData.results[0].tradeTimestamp));

                    }


                    if (symbolData.status.code == 204 && symbolData.status.message == "Success, but no content to return.") {
                        clearChart('daily');
                        clearChart('weekly');
                        clearChart('monthly');
                        clearChart('dailySecond');
                        clearChart('dailyBulQuantData');


                        clearChart('dailyBuleFractal');
                        clearChart('dailyBlueNeural');

                        alert(symbolData.status.message);
                        HideLoaderCustom();
                    } else {
                        //GetMarketChartNearTermData('daily');
                        //GetMarketChartNearTermData('weekly');
                        //GetMarketChartNearTermData('monthly');
                        //GetMarketChartNearTermData('dailySecond');
                        //GetMarketChartNearTermData('dailyBulQuantData');
                        //GetMarketChartNearTermData('dailyBuleFractal');
                        //GetMarketChartNearTermData('dailyBlueNeural');

                        HideLoaderCustom();
                        setTimeout(GetAndBindMarketAllChartsData, 1000);
                    }
                }


                BindBluFractalMarketsPopup(data, "tblCategoriesList2");
            }
        },
        error: function (error) {
            console.log(error);
            alert('Error');
        }
    });
}



function RebindBuleQuentData(selectedCode) {
    var a = $('#date').val(); var Type = $('#datafrequency').val();

    ShowLoaderCustom();
    $.ajax({
        type: "GET",
        url: "/Home/GetAllMarketData",
        dataType: "json",
        async: true,
        data: { 'startDate': $('#date').val(), 'Type': $('#datafrequency').val(), 'selectedCode': selectedCode },
        success: function (data) {
            $("#txtMDDailyCode").val(selectedCode);
            ReloadDailyMarketDataBluQuant(false,selectedCode);
            if (data != null) {
                $("#hfGlobalSymbolName").val(selectedCode);
                $("#BlueQuanteCode").val(data.Code);

                $(".codeClassbluequont").text(data.Code);
                $(".codeClassbluequont").val(data.Code);

                $(".NameClassbluequont").text(data.Name);
                $(".NameClassbluequont").val(data.Name);

                $(".ClosingPriceClassbluequont").text(data.ClosingPrice);
                var avgVol = CommaFormatted(parseFloat(data.AverageVolumn).toFixed(0));
                $(".AvgVolumeClassbluequont").text(avgVol);


                if (data.SymbolNameData != undefined && data.SymbolNameData != null && data.SymbolNameData != '') {
                    var symbolData = $.parseJSON(data.SymbolNameData);
                    if (symbolData.status.code == 200 && symbolData.results.length > 0) {
                        symbolTitle = symbolData.results[0].name;
                        var sysmbolName = symbolData.results[0].name.replace(symbolData.results[0].symbol, "").trim();
                        $("#BlueQuanteCode").val(data.Code);
                        $(".NameClassbluequont").text(sysmbolName);
                        $(".NameClassbluequont").val(sysmbolName);

                    }


                    if (symbolData.status.code == 204 && symbolData.status.message == "Success, but no content to return.") {

                        clearChart('dailyBulQuantData');
                        alert(symbolData.status.message);
                        //HideLoaderCustom();
                    } else {
                        GetMarketChartNearTermData('dailyBulQuantData');
                        

                    }



                }



              

            }
            HideLoaderCustom();
        },
        error: function (error) {
            console.log(error);
            alert('Error');
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
                 //{ "data": "symbol", "name": "symbol" },
                 //{ "data": "timestamp", "name": "timestamp" },
                 { "data": "tradingDay", "name": "TradingDate" },
                 { "data": "open", "name": "open" },
                  { "data": "high", "name": "high" },
                   { "data": "low", "name": "low" },
                    { "data": "close", "name": "close" },
                     { "data": "volume", "name": "volume" }
                      //,{ "data": "openInterest", "name": "openInterest" }
        ],
        "order": [[0, "desc"]]
    });
}


function ConvertJsonDateString(dateString) {
    var shortDate = null;
    if (dateString) {
        // var regex = /-?\d+/;
        // var matches = regex.exec(jsonDate);
        var dt = new Date(dateString);
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear();
        shortDate = monthString + '/' + dayString + '/' + year;
    }
    return shortDate;
}


function OpenPopupWithTheQuote() {
    // window.open('http://finance.yahoo.com/quote/QQQ?p=QQQ');
    var element = $("ul#AllTables li.active>a").attr('id');
    if (element != undefined || element != '') {

        var idOfTextBox = element.split("_");
        if (idOfTextBox.length > 1) {
            var code = $('#' + idOfTextBox[1]).val();


            if (code.trim() != '') {

                var x = screen.width / 2 - 700 / 2;
                var y = screen.height / 2 - 450 / 2;
                window.open('http://finance.yahoo.com/quote/' + code.trim() + '?p=' + code.trim(), 'sharegplus', "resizable=1,width=900,height=500,scrollbars=1,menubar=0,location=0");
            }

            else {
                alert('Please select a symbol first.');
                $('#' + idOfTextBox[1]).focus();
            }

        }

    }
    //return false;
}

function OpentheMarkeResourceDataFromDashboard(code) {
    if (code.trim() != '') {

        var x = screen.width / 2 - 700 / 2;
        var y = screen.height / 2 - 450 / 2;
        window.open('http://finance.yahoo.com/quote/' + code.trim() + '?p=' + code.trim(), 'sharegplus', "resizable=1,width=900,height=500,scrollbars=1,menubar=0,location=0");
    }
}




function CommaFormatted(amount) {
    var delimiter = ","; // replace comma if desired
    amount = new String(amount);
    var a = amount.split('.', 2)
    var d = a[1];
    var i = parseInt(a[0]);
    if (isNaN(i)) { return ''; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    var n = new String(i);
    var a = [];
    while (n.length > 3) {
        var nn = n.substr(n.length - 3);
        a.unshift(nn);
        n = n.substr(0, n.length - 3);
    }
    if (n.length > 0) { a.unshift(n); }
    n = a.join(delimiter);
    //if (d.length < 1) { amount = n; }
    //else { amount = n + '.' + d; }
    //amount = minus + amount;
    return n;
}



function BindMarketDataIndividual(selectedCode) {
    var a = $('#date').val(); var Type = $('#datafrequency').val();
    ShowLoaderCustom();
    $.ajax({
        type: "GET",
        url: "/Home/GetAllMarketData",
        dataType: "json",
        async: true,
        data: { 'startDate': $('#date').val(), 'Type': $('#datafrequency').val(), 'selectedCode': selectedCode },
        success: function (data) {
            //if (data != null && $("#Id_MarketDataListDaily").length > 0) {
            //    var dailyData = $.parseJSON(data.MarketDataDaily);
            //    $.each(dailyData.results, function (i, d) {
            //        d.volume = CommaFormatted(parseFloat(d.volume).toFixed(0));
            //    });
            //    bindDatatable('Id_MarketDataListDaily', dailyData.results);
            //}

            HideLoaderCustom();
        },
        error: function (error) {
            console.log(error);
            HideLoaderCustom();
        }
    });
}


function BindBluFractalMarketsPopup(data, selector) {
    $("#" + selector).empty();
    var htmlContent = '<thead><tr>';
    var trHtml = '';
    var newInnerTable = '';
    var categoryId = 0;
    $.each(data.Categories, function (i, obj) {
        htmlContent += '<th>' + obj.Text + '</th>';

        var items = $.grep(data.MarketLists, function (i, obj1) {
            return (i.CategoryId == obj.Value);
        });

        trHtml += categoryId == 0 ? '<tbody><tr><td>' : "<td>";

        var newInnerTable = "<table class='table table-bordered table-striped'>";
        if (items.length > 0) {
            $.each(items, function (i, item) {
                //RebindMarketData($('#code').val()
                //newInnerTable += '<tr><td><a onclick="IntializeOnlyBlueQuanteWithCode("' + item.SymbolCode+ '")" class="btnGetData">' + item.SymbolName + ' </td></tr>';
                newInnerTable += "<tr><td><a onclick=\"RebindMarketData('" + item.SymbolCode + "')\" class=\"btnGetData\">" + item.SymbolName + " </td></tr>";

            });
        }

        newInnerTable += "</table>";
        trHtml += newInnerTable + '</td>';

        categoryId = obj.Value;
    });

    trHtml += '</tr>';
    htmlContent += '</thead>';

    if (trHtml != '')
        trHtml += '</tbody>';

    htmlContent += trHtml;
    $("#" + selector).html(htmlContent);

    if ($("#tblCategoriesListCombo").length > 0)
        $("#tblCategoriesListCombo").html(htmlContent);

    if ($("#tblCategoriesListNeural").length > 0)
        $("#tblCategoriesListNeural").html(htmlContent);

}


function GetToggleTickerState() {
    $.get("/Charts/GetTickerToggleState", null, function (data) {
        ToggleTickerIconOnLoad(data);
    });
}

