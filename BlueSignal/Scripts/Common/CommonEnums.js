var PageSetting = window.location.protocol + "//" + window.location.host;

RedirectToUrl = {
    TCPProductCountryFeatureMappingsEdit: PageSetting + '\/TCPProductCountryFeatureMappings/Edit/',
    EditUserPage: PageSetting + '\/Default/EditUser?Id=',
    UserListPage: PageSetting + '\/Default/UserList',
    AddEvent: PageSetting + '\/Event/AddEvent',
    DisplayEvent: PageSetting + '\/Event/DisplayEvents',//  /Jobdetails/Edit/
    EditFullDayCalendar: PageSetting + '\/Event/EditFullDayEvent',//  /Jobdetails/Edit/
    EditVacations: PageSetting + '\/Event/EditVacations',
    EditAcademic: PageSetting + '\/Event/EditAcademic',
    AddTmEvent: PageSetting + '\/Event/AddTmEvent',
    CalendarFull: PageSetting + '\/Calendar/Calendar',
    CalendarVacation: PageSetting + '\/Calendar/CalendarVacation',
    CalendarTemplate: PageSetting + '\/Calendar/CalendarTemplate',
    CalendarAcademic: PageSetting + '\/Calendar/AcademicEvents',
    UserList: PageSetting + '\/Default/UserList',
    DisplayAcademicEvent: PageSetting + '\/AcademicNames/DisplayAcademicCalendarList',
    CopySuccess: PageSetting + '\/ShiftNames/CopySuccess',
    AddLearnerCalendar: PageSetting + '\/Event/AddLearnerEvent',

};

//function is used to redirect any url
function RedirectToPage(url) {
    window.location = url;
}

function RedirectToPageWithQueryString(url, event) {    
    var todayDate = new Date();     
    /*todayDate.setMonth(event.get);*/
    var returndate = new Date(event);
    returndate = new Date(returndate.setDate(todayDate.getDate()));
    window.location = url + '?cdate=' + ConvertToMMDDYYYY(event) + '&retdt=' + ConvertToMMDDYYYY(returndate);
}


//Enum for Calendar Redirect events
CalendarRedirectUrl = {
    DisplayEvent: "href='/Event/DisplayEvents?id=",
    TemplateDisplayEvent: "href='/Event/TemplateDisplayEvents?id=",
    DisplayAcademicEvent: "href='/AcademicNames/DisplayAcademicCalendarList?",
    DisplayLearnerEvent: "href='/Event/DisplayEventLearners?id="
}


//Function Called On Calendar evnets
function GetRedirectUrlForDisplayEvent(event,fromScreen) {
    //window.location = 'href='+ RedirectToUrl.DisplayEvent + '?id=' + event.id + '&retdt=' + ConvertToMMDDYYYY(event.start);   
    return CalendarRedirectUrl.DisplayEvent + event.id + '&retdt=' + ConvertToMMDDYYYY(event.start) +'&FromScreen='+fromScreen + "'";
}


//Function Called On Calendar evnets
function GetRedirectUrlForDisplayLearnerEvent(event, fromScreen) {
    //window.location = 'href='+ RedirectToUrl.DisplayEvent + '?id=' + event.id + '&retdt=' + ConvertToMMDDYYYY(event.start);   
    return CalendarRedirectUrl.DisplayLearnerEvent + event.id + '&retdt=' + ConvertToMMDDYYYY(event.start) + '&FromScreen=' + fromScreen + "'";
}


//Function Called On Calendar evnets
function GetRedirectUrlForDisplayAcademicEvent(event) {
    var todayDate = new Date();
    var returndate = new Date(event);
    returndate = new Date(returndate.setDate(todayDate.getDate()));     
    return CalendarRedirectUrl.DisplayAcademicEvent + 'cdate=' + ConvertToMMDDYYYY(returndate) + '&retdt=' + ConvertToMMDDYYYY(event.start) + "'";
}


//Function Called On Calendar evnets
function GetRedirectUrlForTemplateDisplayEvent(event, fromScreen) {
    //window.location = 'href='+ RedirectToUrl.DisplayEvent + '?id=' + event.id + '&retdt=' + ConvertToMMDDYYYY(event.start);
    
    return CalendarRedirectUrl.TemplateDisplayEvent + event.id + '&retdt=' + ConvertToMMDDYYYY(event.start) + '&FromScreen=' + fromScreen + "'";
}



function ConvertToMMDDYYYY(date) {
   // var d = date.slice(0, 10).split('-');   
    //return d[1] +'/'+ d[2] +'/'+ d[0]; // 
    return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear();
    //return '10/30/2010';
}