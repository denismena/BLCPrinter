$(function () {
    $('.datefield').datepicker({
        changeMonth: true,
        changeYear: true,
        showOtherMonths: true,
        selectOtherMonths: true,
        numberOfMonths: 1,
        showOn: 'button',        
        dateFormat: 'dd/M/yy',
        showAnim: ''
    });    
    //$(this).attr("data-autocomplete")
    //$("#PERS_NAME").autocomplete({
    //    source: '/Contracts/Autocomplete',
    //    minChars: 2,
    //    mustMatch: true,
    //    select: function (event, ui) {
    //        var zipCodeID = parseInt(ui.item.value, 1);
    //        $(this).val(ui.item.label);
    //        $("#C_PERSOANA_ID").val(ui.item.value);
    //        return false;
    //    }
    //});
    $('.tbList').dataTable({
        "order": [[1, "desc"]],
        "iDisplayLength": 20,
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bStateSave": false,
        "bProcessing": true,
        "asStripeClasses": ['odd', 'even'],
    });
    $('.tbList2').dataTable({
        "searching": false,
        "paging": false,
        "bLengthChange": false,
        "bStateSave": false,
        "bProcessing": true,
        "asStripeClasses": ['odd', 'even'],
    });
/* formatDate plug-in
*
* Parameters:
*      selector : the jquery element selector 
*      
*
* Usage:
*        $("#datefieldID").formatDate();  or  $(".datefieldClass").formatDate();
* Dependencies:
*
*      jquery.js
*/
    jQuery.fn.formatDate = function () {
        $(this).change(function () {
            var rezultat = true;
            var stringdate = $(this).val();
            if (stringdate != "") {
                var slash1 = stringdate.indexOf("/");
                var slash2 = stringdate.lastIndexOf("/");
                var months = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
                var days = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);

                if (slash1 == -1 || slash2 == slash1) {
                    rezultat = false;
                }
                else {
                    var day;
                    var month;
                    var year;

                    day = stringdate.substring(0, slash1);
                    month = stringdate.substring(slash1 + 1, slash2);
                    year = stringdate.substring(slash2 + 1);

                    if (!$.isNumeric(day) || !$.isNumeric(year)) {
                        rezultat = false;
                    }
                    else {
                        if ($.isNumeric(month)) {
                            while (month.charAt(0) == 0 && month != "0")
                                month = month.substring(1);

                            if (month > 0 && month < 13)
                                month = months[month - 1];
                        }

                        var monthUpperCase = month.toUpperCase();
                        var found = -1;

                        for (var i = 0; i < months.length; i++) {
                            if (months[i].toUpperCase() == monthUpperCase)
                                found = i;
                        }

                        if (found == -1)
                            rezultat = false;
                        else {
                            if (year % 4 == 0)
                                days[1] = 29;
                            if (day < 1 || day > days[found])
                                rezultat = false;
                            else {
                                if (day.length > 1)
                                    day = parseInt(day, 10).toString();
                                if (day < 10 && day.charAt(0) != 0)
                                    day = "0" + day;
                                var yearInt = parseInt(year * 1, 10);
                                if (yearInt < 26)
                                    yearInt = 2000 + yearInt;
                                else
                                    if (yearInt < 100)
                                        yearInt = 1900 + yearInt;
                                if (yearInt < 9999 && yearInt > 1752)
                                    rezultat = true;
                                else
                                    rezultat = false;
                                $(this).val(day + "/" + months[found] + "/" + yearInt);
                            }
                        }
                    }
                }
            }
            else {
                rezultat = false;
            }
            if (!rezultat)
                $(this).val('');
            return rezultat;
        });
    }



    $(".datefield").formatDate();

});