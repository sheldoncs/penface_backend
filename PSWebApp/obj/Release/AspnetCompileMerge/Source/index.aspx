<%@ Page Title="" Language="C#" MasterPageFile="~/Payroll.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PSWebApp.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
 <script src="Scripts/sweetalert.min.js" type="text/javascript"></script>
  <link href="css/sweetalert.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript">

     $(function () {

         var penfaceOption = "";
         var $input = $(".box__file");
         var $form = $('.box');

         var reportOption = "";
         initialize();

         function initialize() {

             $(".btnExportPenface").addClass('ui-disabled');
             $(".btnExecuteService").addClass('ui-disabled');
             $(".btnExportFinance").addClass('ui-disabled');
             $("#scroll").hide();
             $(".loading").hide();

             //$(".penface a").css("background", 'url("' + result[i].path + '") no-repeat #EDEDED');

             mobileOrPC();

             PSWebApp.Payroll.getButtonMenu(function (result) {

                 for (var i = 0; i < result.length; i++) {

                     $('.' + result[i].title + ' a').removeClass('btnReports');
                     $('.' + result[i].title + ' a').css("background", 'url("' + result[i].path + '") no-repeat #EDEDED');
                     $('.' + result[i].title + ' a').css("width", "106px");
                     $('.' + result[i].title + ' a').css("height", "106px");
                     $('.' + result[i].title + ' a').css("border-radius", "5px");
                     if (result[i].title == 'penface') {
                         $('.' + result[i].title + ' a').css("border", "2px solid #ffcc00");
                         $(".penfaceFooter").toggle();
                     }
                     else {
                         $('.' + result[i].title + ' a').css("border", "2px solid #000");
                     }
                     $('.' + result[i].title + ' a').css("background-position", 'center');



                 }

             });


         }

         var processFiles = function () {



             var ajaxData = new FormData($form.get(0));



             if (droppedFiles) {


                 $.each(droppedFiles, function (i, file) {


                     ajaxData.append($input.attr('name'), file);


                 });
             }


             $.ajax({
                 url: 'services/spreadsheet.ashx',
                 data: ajaxData,
                 processData: false,
                 contentType: false,
                 type: 'POST',
                 success: OnComplete,
                 error: OnFail
             });




         }

         function OnComplete(result) {

             swal("Success", result, "success");

             $('.loading').hide();

         }

         function OnFail(result) {
             console.log(result);
             $('.loading').hide();
             swal("Error", result, "error");
         }

         $(document).on("pageshow", "#home", function () {


             mobileOrPC();

         });

         function mobileOrPC() {

             $(".penfaceFooter").hide();
             $(".joinerFooter").hide();
             $(".bannerFooter").hide();
             $(".creditUnionFooter").hide();
             $(".fssuFooter").hide();
             $(".nisExportFooter").hide();
             $(".studyTravelFooter").hide();
             $(".superAnnFooter").hide();
             $(".btnAddLeaver").hide();
             $(".btnBannerJournal").hide();

             $("#joiner").hide();
             $("#title").text('PENFACE JOINER IMPORT');

             $('.tabJoiner').click(function () {

                 penfaceOption = "1";
                 $("#parameterPanel").show();
                 $("#title").text('PENFACE JOINER IMPORT');

                 $(".btnExportPenfaceLeaver").hide();
                 $(".btnExportFinance").show();
                 $(".btnExecuteService").show();
                 $(".btnFinalReport").hide();
                 $(".btnExportPenface").show();
                 $(".btnAddLeaver").hide();
                 $(".btnBannerJournal").hide();


                 $("#lblPayEndDate").show();
                 $("#lblStartDate").hide();
                 $("#lblEndDate").hide();
                 $("#lblFileUpload").hide();
                 $('.lblEmail').hide();
                 $('.btnExecuteFSSU').hide();
             });
             $('.bannerExport').click(function () {
                 $("#parameterPanel").show();
                 $("#title").text('BANNER EXPORT');

                 $(".btnExportPenfaceLeaver").hide();
                 $(".btnExportFinance").hide();
                 $(".btnExecuteService").hide();
                 $(".btnFinalReport").hide();
                 $(".btnExportPenface").hide();
                 $(".btnAddLeaver").hide();
                 $(".btnBannerJournal").show();

                 $("#lblPayEndDate").show();
                 $('.lblEmail').show();
                 $("#lblStartDate").hide();
                 $("#lblEndDate").hide();
                 $("#lblFileUpload").hide();
                 $('.btnExecuteFSSU').hide();
             });
             $('.bannerJournal').click(function () {

                 $("#title").text('BANNER JOURNAL');

                 $(".btnExportPenfaceLeaver").hide();
                 $(".btnExportFinance").hide();
                 $(".btnExecuteService").hide();
                 $(".btnFinalReport").hide();
                 $(".btnExportPenface").hide();
                 $(".btnAddLeaver").hide();
                 $(".btnBannerJournal").hide();

                 $("#lblPayEndDate").show();
                 $('.lblEmail').show();
                 $("#lblStartDate").hide();
                 $("#lblEndDate").hide();
                 $("#lblFileUpload").hide();
                 $('.btnExecuteFSSU').hide();
             });

             $input.on('change', function (e) {

                 e.preventDefault();
                 e.stopPropagation();
                 droppedFiles = e.target.files;

                 $('.loading').show();

                 processFiles();

             });

             $('.tabFinDetImport').click(function () {
                 penfaceOption = "3";
                 $("#joiner").show();
                 $("#title").text('FINANCIAL DETAILS IMPORT FILE - Upload Updated Spread Sheet and write to database, email final report');

                 $(".btnExportPenfaceLeaver").hide();
                 $(".btnExportFinance").hide();
                 $(".btnExecuteService").hide();
                 $(".btnFinalReport").show();
                 $(".btnExportPenface").hide();
                 $(".btnAddLeaver").hide();

                 $("#lblPayEndDate").hide();
                 $("#lblStartDate").hide();
                 $("#lblEndDate").hide();
                 $("#lblFileUpload").show();
                 $('.lblEmail').hide();

             });
             $('.tabLeaverImport').click(function () {
                 penfaceOption = "4";
                 $("#joiner").show();
                 $("#title").text('LEAVER IMPORT FILE');

                 $(".btnExportPenfaceLeaver").show();
                 $(".btnAddLeaver").show();
                 $(".btnExportFinance").hide();
                 $(".btnExecuteService").hide();
                 $(".btnFinalReport").hide();
                 $(".btnExportPenface").hide();

                 $("#lblPayEndDate").show();
                 $("#lblStartDate").show();
                 $("#lblEndDate").show();
                 $("#lblFileUpload").hide();
                 $('.lblEmail').show();


             });

             $('.tabFinanceSpreadsheet').click(function () {
                 penfaceOption = "2";
                 $("#joiner").show();
                 $("#title").text('FINANCE SPREADSHEET - Create Spreadsheet');

                 $(".btnExportPenfaceLeaver").hide();
                 $(".btnExportFinance").hide();
                 $(".btnExecuteService").hide();
                 $(".btnFinalReport").hide();
                 $(".btnExportPenface").hide();

                 $("#lblPayEndDate").show();
                 $("#lblStartDate").hide();
                 $("#lblEndDate").hide();
                 $("#lblFileUpload").hide();
                 $('.lblEmail').show();

             });

             $('.addJoiner').click(function () {
                 $("#joiner").toggle();

             });

             $('.banner').click(function () {

                 $("#title").text('BANNER');
                 $(".penfaceFooter").hide();
                 $(".bannerFooter").toggle();
                 $(".joinerFooter").hide();
                 $(".creditUnionFooter").hide();
                 $(".fssuFooter").hide();
                 $(".nisExportFooter").hide();
                 $(".studyTravelFooter").hide();
                 $(".superAnnFooter").hide();


                 $("#parameterPanel").show();
                 $(".btnExecuteReport").hide();
                 $(".btnExportPenfaceLeaver").hide();
                 $(".btnExportFinance").hide();
                 $(".btnExecuteService").hide();
                 $(".btnFinalReport").hide();
                 $(".btnExportPenface").hide();
                 $(".btnAddLeaver").hide();
                 $(".btnBannerJournal").show();

                 $("#lblPayEndDate").show();
                 $('.lblEmail').show();
                 $("#lblStartDate").hide();
                 $("#lblEndDate").hide();
                 $("#lblFileUpload").hide();
                 $('.btnExecuteFSSU').hide();

                 $('.banner a').css("border", "2px solid #ffcc00");
                 $('.penface a').css("border", "2px solid #000");
                 $('.joiner a').css("border", "2px solid #000");
                 $('.creditUnion a').css("border", "2px solid #000");
                 $('.fssu a').css("border", "2px solid #000");
                 $('.nisExport a').css("border", "2px solid #000");
                 $('.studyTravel a').css("border", "2px solid #000");
                 $('.superAnn a').css("border", "2px solid #000");

             });


             $('#email').on("blur", function () {
                 /*
                 var inputVal = $(this).val();
                 var characterReg = /^\s*[a-zA-Z0-9,\s]+\s*$/;
                 if (!characterReg.test(inputVal)) {
                 $(this).after('<span class="error error-keyup-2">No special characters allowed.</span>');
                 }*/
             });

             $(".btnBannerJournal").click(function () {

                 $(".btnExportPenface").hide();
                 $(".btnExecuteService").hide();
                 $(".btnExportFinance").hide();
                 $('.btnExecuteFSSU').hide();

                 var dte = $('#payEndDate').val();
                 var email = $('#email').val();
                 $(".loading").show();
                 PSWebApp.Payroll.ExecuteBannerJournal(dte, email, function (result) {
                     $(".loading").hide();
                 });

             });




             $('.creditUnion').click(function () {

                 $("#title").text('CREDIT UNION');
                 $(".penfaceFooter").hide();
                 $(".bannerFooter").hide();
                 $(".joinerFooter").hide();
                 $(".penfaceFooter").hide();
                 $(".creditUnionFooter").toggle();
                 $(".fssuFooter").hide();
                 $(".nisExportFooter").hide();
                 $(".studyTravelFooter").hide();
                 $(".superAnnFooter").hide();

                 $('.creditUnion a').css("border", "2px solid #ffcc00");
                 $('.penface a').css("border", "2px solid #000");
                 $('.joiner a').css("border", "2px solid #000");
                 $('.banner a').css("border", "2px solid #000");
                 $('.fssu a').css("border", "2px solid #000");
                 $('.nisExport a').css("border", "2px solid #000");
                 $('.studyTravel a').css("border", "2px solid #000");
                 $('.superAnn a').css("border", "2px solid #000");
                 reportOption = "CREDITUNION";
                 $('.btnExecuteFSSU').hide();
             });
             $('.fssu').click(function () {

                 $('.btnExecuteFSSU').show();
                 $(".penfaceFooter").hide();
                 $(".bannerFooter").hide();
                 $(".joinerFooter").hide();
                 $(".penfaceFooter").hide();
                 $(".creditUnionFooter").hide();
                 $(".fssuFooter").show();
                 $(".nisExportFooter").hide();
                 $(".studyTravelFooter").hide();
                 $(".superAnnFooter").hide();

                 $("#lblPayEndDate").show();
                 $('.lblEmail').show();
                 $("#lblStartDate").hide();
                 $("#lblEndDate").hide();
                 $("#lblFileUpload").hide();
                 reportOption = "FSSU";

                 $("#parameterPanel").show();
                 $(".btnExecuteReport").hide();
                 $(".btnExportPenfaceLeaver").hide();
                 $(".btnExportFinance").hide();
                 $(".btnExecuteService").hide();
                 $(".btnFinalReport").hide();
                 $(".btnExportPenface").hide();
                 $(".btnAddLeaver").hide();
                 $(".btnBannerJournal").hide();


                 $("#title").text('FSSU');
             });
             $('.btnExecuteFSSU').click(function () {
                 $(".loading").show();
                 var dte = $('#payEndDate').val();
                 PSWebApp.Payroll.getFSSUData(dte, function (result) {
                     /*if (result == "Success") {
                     $(".btnExportPenface").removeClass('ui-disabled');
                     $(".loading").hide();
                     }
                     */
                     swal("Success", result, "success");
                 });
             });

             $('.nisExport').click(function () {

                 $("#title").text('NIS EXPORT');

                 $(".bannerFooter").hide();
                 $(".joinerFooter").hide();
                 $(".penfaceFooter").hide();
                 $(".creditUnionFooter").hide();
                 $(".fssuFooter").hide();
                 $(".penfaceFooter").hide();
                 $(".nisExportFooter").toggle();
                 $(".studyTravelFooter").hide();
                 $(".superAnnFooter").hide();

                 $('.penface a').css("border", "2px solid #000");
                 $('.joiner a').css("border", "2px solid #000");
                 $('.banner a').css("border", "2px solid #000");
                 $('.creditUnion a').css("border", "2px solid #000");
                 $('.fssu a').css("border", "2px solid #000");
                 $('.nisExport a').css("border", "2px solid #ffcc00");
                 $('.studyTravel a').css("border", "2px solid #000");
                 $('.superAnn a').css("border", "2px solid #000");

             });
             $('.penface').click(function () {

                 $('.penface a').css("border", "2px solid #ffcc00");
                 $('.joiner a').css("border", "2px solid #000");
                 $('.banner a').css("border", "2px solid #000");
                 $('.creditUnion a').css("border", "2px solid #000");
                 $('.fssu a').css("border", "2px solid #000");
                 $('.nisExport a').css("border", "2px solid #000");
                 $('.studyTravel a').css("border", "2px solid #000");
                 $('.superAnn a').css("border", "2px solid #000");

                 $("#parameterPanel").show();

                 $(".btnExportPenfaceLeaver").show();
                 $(".btnExportFinance").show();
                 $(".btnExecuteService").show();
                 $(".btnFinalReport").show();
                 $(".btnExportPenface").show();
                 $(".btnAddLeaver").hide();
                 $(".btnExecuteReport").show();
                 $(".btnBannerJournal").hide();

                 $("#lblPayEndDate").show();
                 $('.lblEmail').show();
                 $("#lblStartDate").hide();
                 $("#lblEndDate").hide();
                 $("#lblFileUpload").hide();


                 $(".penfaceFooter").show();
                 $(".joinerFooter").hide();
                 $(".bannerFooter").hide();
                 $(".creditUnionFooter").hide();
                 $(".fssuFooter").hide();
                 $(".nisExportFooter").hide();
                 $(".studyTravelFooter").hide();
                 $(".superAnnFooter").hide();

                 $("#title").text('PENFACE JOINER IMPORT');

             });
             $('.joiner').click(function () {

                 $(".penfaceFooter").hide();
                 $(".joinerFooter").toggle();
                 $(".bannerFooter").hide();
                 $(".creditUnionFooter").hide();
                 $(".fssuFooter").hide();
                 $(".nisExportFooter").hide();
                 $(".studyTravelFooter").hide();
                 $(".superAnnFooter").hide();

                 $("#joiner").hide();
                 $("#title").text('ADD JOINER');

                 $('.joiner a').css("border", "2px solid #ffcc00");
                 $('.penface a').css("border", "2px solid #000");
                 $('.banner a').css("border", "2px solid #000");
                 $('.creditUnion a').css("border", "2px solid #000");
                 $('.fssu a').css("border", "2px solid #000");
                 $('.nisExport a').css("border", "2px solid #000");
                 $('.studyTravel a').css("border", "2px solid #000");
                 $('.superAnn a').css("border", "2px solid #000");
             });
             $('.studyTravel').click(function () {

                 $("#title").text('STUDY & TRAVEL');

                 $(".joinerFooter").hide();
                 $(".bannerFooter").hide();
                 $(".creditUnionFooter").hide();
                 $(".fssuFooter").hide();
                 $(".nisExportFooter").hide();
                 $(".penfaceFooter").hide();
                 $(".studyTravelFooter").toggle();
                 $(".superAnnFooter").hide();

                 $('.joiner a').css("border", "2px solid #000");
                 $('.penface a').css("border", "2px solid #000");
                 $('.banner a').css("border", "2px solid #000");
                 $('.creditUnion a').css("border", "2px solid #000");
                 $('.fssu a').css("border", "2px solid #000");
                 $('.nisExport a').css("border", "2px solid #000");
                 $('.studyTravel a').css("border", "2px solid #ffcc00");
                 $('.superAnn a').css("border", "2px solid #000");

             });
             $('.superAnn').click(function () {

                 $("#title").text('SUPER ANNUATION');
                 $(".joinerFooter").hide();
                 $(".bannerFooter").hide();
                 $(".creditUnionFooter").hide();
                 $(".fssuFooter").hide();
                 $(".nisExportFooter").hide();
                 $(".penfaceFooter").hide();
                 $(".studyTravelFooter").hide();
                 $(".superAnnFooter").toggle();

                 $('.joiner a').css("border", "2px solid #000");
                 $('.penface a').css("border", "2px solid #000");
                 $('.banner a').css("border", "2px solid #000");
                 $('.creditUnion a').css("border", "2px solid #000");
                 $('.fssu a').css("border", "2px solid #000");
                 $('.nisExport a').css("border", "2px solid #000");
                 $('.studyTravel a').css("border", "2px solid #000");
                 $('.superAnn a').css("border", "2px solid #ffcc00");

             });


             var isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);


             if ($(window).width() <= 760 || isMobile) {


                 $(".logout").css({
                     'position': 'absolute',
                     'left': '70%',
                     'top': '70px'
                 });

             } else {

                 $(".logout").css({
                     'position': 'absolute',
                     'left': '90%',
                     'top': '70px'
                 });
             }


             /*Penface Maintenance*/

             $(".btnExecuteReport").click(function () {

                 $(".loading").show();

                 if (penfaceOption == "1") {
                     var dte = $('#payEndDate').val();


                     if (dte != "") {
                         swal("Penface Finance Spreadsheet Process", "Begin Writing To Database", "");
                         PSWebApp.Payroll.ExecutPenfaceJoinerProcesses(dte, function (result) {
                             if (result == "Success") {
                                 $(".btnExportPenface").removeClass('ui-disabled');
                                 $(".loading").hide();
                             }

                             swal("Success", result, "success");
                         });

                     }
                     else {
                         swal("Success", "Enter Pay End Date", "error");

                     }

                 } else if (penfaceOption == "2") {

                     swal("Write To Spread Sheet", "Begin SpreadSheet Processing", "");
                     $(".loading").show();
                     var dt = $('#payEndDate').val();

                     PSWebApp.Payroll.ExecutePenfaceFinanceSpreadsheetProcess(dt, function (result) {

                         swal("Completed", result, "success");
                         $(".loading").hide();

                     });

                 } else if (penfaceOption == "3") {

                     swal("Penface Finance Data Process", "Upload To Database", "");

                     PSWebApp.Payroll.ExecutePenfaceFinanceDataProcess(function (result) {
                         swal("Penface Finance Data Process", result, "");
                         $(".loading").hide();
                     });



                 } else if (penfaceOption == "4") {
                     $(".loading").show();
                     PSWebApp.Payroll.ExecutePenfaceLeaverProcesses($('#startDate').val(), $('#endDate').val(), $('#payEndDate').val(), $('#email').val(), function (result) {
                         swal("Penface Leaver Processes", result, "");
                         $(".loading").hide();
                     });

                 }


             });
             $(".btnExportPenface").click(function () {

                 $(".loading").show();
                 PSWebApp.Payroll.ExportPenfacePersonal(function (result) {

                     if (result == "Success") {
                         $(".btnExecuteService").removeClass('ui-disabled');
                         $(".loading").hide();
                     }
                     swal("Export Penface Personal", result, "");
                 });

             });
             $(".btnExecuteService").click(function () {
                 $(".loading").show();
                 PSWebApp.Payroll.ExportServiceDetails(function (result) {



                     if (result == "success") {

                         $(".btnExportFinance").removeClass('ui-disabled');
                         $(".loading").hide();
                     }
                     swal("Export Service Details", result, "");
                 });


             });
             $(".btnExportFinance").click(function () {
                 $(".loading").show();
                 if ($("#email").val() != "" && $("#email").val().indexOf("@") && $("#email").val().indexOf(".")) {
                     PSWebApp.Payroll.ExportFinanceRates($("#email").val(), function (result) {
                         swal("Export Finance Rates", result, "");
                         if (result == "Success") {
                             $(".loading").hide();
                         }
                     });
                 } else {
                     swal("Export Finance Rates", "Invalid Email Address", "error");

                 }
             });

             $(".btnFinalReport").click(function () {
                 $(".loading").show();
                 swal("Begin To Email Financial Data Export", "Email", "");
                 if ($("#email").val() != "" && $("#email").val().indexOf("@") && $("#email").val().indexOf(".")) {
                     var dte = $('#payEndDate').val();
                     PSWebApp.Payroll.FinancialDataExport(dte, $("#email").val(), function (result) {
                         swal("Email Financial Data Export", result, "");
                         $(".loading").hide();

                     });
                 }

             });
             $(".btnExportPenfaceLeaver").click(function () {

                 $(".loading").show();

                 PSWebApp.Payroll.ExportPenfaceLeaver($('payEndDate').val(), $('#email').val(), function (result) {

                     swal("Email Financial Data Export", result, "");
                     $(".loading").hide();

                 });

             });

             $(".btnAddLeaver").click(function () {
                 var li = "";

                 $("#cover").show();

                 $("#scroll").show();

                 $("#PenfaceList").show();
                 $("#PenfaceList").empty();
                 $("#PenfaceList").append('<table width="300px">');

                 PSWebApp.Payroll.getPenfaceLeavers(function (result) {

                     if (result.length > 0) {

                         for (var i = 0; i < result.length; i++) {

                             $("#PenfaceList").append('<tr>');
                             $("#PenfaceList").append('<td  align="left" style="width: 9%;height:40px;">' + '<span class="wordClass" style="left:20px;color:#000">' + result[i].emplId + '</span></td>');
                             $("#PenfaceList").append('<td  align="left" style="width: 3%;">' + ' <input class="wordClass"    id= ' + result[i].ID + ' type="button" value="Delete" /></td>');
                             $("#PenfaceList").append('</tr>');
                         }

                         $("#PenfaceList").append('</table>');

                     }
                     else {

                         swal("Empty Leaver List", "Leaver List", "error");

                     }
                 });
                 /*
                

                 

                 for (var i = 0; i < result.length; i++) {

                 $("#PenfaceList").append
                 //<td>' + '<input id="' + result[i].emplid + '" type="button" class="Remove" value="Remove" />' + 
                 //li += '<li>' + result[i].emplId + '</li>';
                 }
                 $("#PenfaceList").html(li);
                 $("#PenfaceList").listview('refresh');

                 } else {

                 swal("Empty Leaver List", "Leaver List", "error");
                         
                 }

                 });
                 */
             });
             /*Penface Maintenance*/

         }
     });

 </script>
 <style type="text/css">
     li
     {
         display: inline;
         float: left;
         padding: 8px;
     }
     .subBanner
     {
         top: 150px;
         position: absolute;
     }
     .subCreditUnion
     {
         top: 150px;
         left: 13%;
         position: absolute;
     }
     #parameterPanel
     {
         position: absolute;
         left: 0px;
         top: 318px;
         width: 40%;
         height: 60%;
         border-radius: 5px !important;
         background: #404759 !important;
     }
     .subPenface
     {
         top: 150px;
         left: 5%;
         position: absolute;
     }
     .subSuperAnn
     {
         top: 150px;
         left: 50%;
         position: absolute;
     }
     .pMenu
     {
         position: absolute;
         top: 108px;
         background: #E3E1E1;
         width: 100%;
     }
     
     .ui-icon-myapp-settings:after
     {
         background: url("images/button/icons-small/settings@2x.png") repeat rgba(0, 0, 0, 0.4) !important;
         background-size: 18px 18px;
         background-color: black;
     }
     #cover
     {
         position: fixed;
         top: 0;
         left: 0;
         background-color: #000;
         background: rgba(0,0,0,0.6);
         z-index: 5;
         width: 100% !important;
         height: 100% !important;
         display: none;
     }
     #scroll
     {
         position: absolute;
         top: 30px;
         background: #fff;
         z-index: 10;
         height: 450px !important;
         display: none;
         display: block;
         margin: auto;
         width: 50%;
         left: 50%;
         border: 3px solid #8E929C;
         padding: 10px;
         border-radius:10px;
     }
     
     .bg
     {
         background: #818793;
         font: verdana;
         font-size: 9pt;
     }
     .apptitle
     {
         top: 282px;
         background: #545D73 !important;
         width: 100%;
         height: 40px;
         position: absolute;
         color: #fff;
         font-weight: normal;
         font: Verdana;
         font-size: 10pt;
         margin-left: auto !important;
         margin-right: auto !important;
     }
     #processButtons
     {
         position: absolute;
         top: 7px;
         left: 60%;
     }
     #joinerData
     {
         position: absolute;
         top: 10px;
         width: 350px;
         left: 10px;
         font: Verdana;
         font-size: 11pt;
         font-weight: normal;
     }
     .loading
     {
         position: absolute !important;
         top: 250px !important;
         left: 50% !important;
         border: none: 2px solid #000 !important;
         z-index: 10;
     }
 </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--<a href="#" id="checkLogout" class="btnCls ui-btn ui-btn-b" data-icon="lock">Logout</a>-->
   
   
   <div data-role="page" id="home">

      <div data-role="header" data-position="fixed" style="background:#505664;height:120px;">
           
           <div class="header" style="padding-top:21px; background: #898C9B !important;font: arial; font-size:12pt; height:35px;">
      
             <center>

               <span style="padding-top:30px; font-weight:normal; color:#fff;font-family:Verdana;font-size:10pt;">PAYROLL REPORTS</span>
               
             </center> 
         

          </div>
          
          <ul class="pMenu">
              <li class="rptmenu penface"><a href="#" class="btnReports ui-btn ui-btn-b">Penface</a></li>
              <li class="rptmenu joiner"><a href="#" class="btnReports ui-btn ui-btn-b">Add Joiner</a></li>
              <li class="rptmenu banner"><a href="#" class="btnReports ui-btn ui-btn-b">Banner</a></li>
              <li class="rptmenu creditUnion"><a href="#" class="ui-btn ui-btn-b">Credit Union</a></li>
              <li class="rptmenu fssu"><a href="#" class="btnReports ui-btn ui-btn-b">FSSU</a></li>
              <li class="rptmenu nisExport"><a href="#" class="btnReports ui-btn ui-btn-b">NIS Export</a></li>
              <li class="rptmenu studyTravel"><a href="#" class="btnReports ui-btn ui-btn-b">Study & Travel</a></li>
              <li class="rptmenu superAnn"><a href="#" class="btnReports ui-btn ui-btn-b">Super Ann.</a></li>
          </ul>
          
          <div class="apptitle textFont">
            <scan id="title" style="display: block;margin: 10px auto 10px auto;padding: 3px;width:100%;font-family:Verdana;font-size:10pt;font-weight:normal;text-align:center;"></scan>
          </div>
          
           <a href="#home" data-transition="slide" data-icon="key" data-iconpos="left" data-iconpos="text" data-mini="true" class="logout" style="width:80px;height:15px;font:verdana;font-size:11pt;font-weight:normal;" data-theme="c">Logout</a>
          
          
      </div>
      
       <div data-role="main" class="ui-content">

          
          <div id="parameterPanel">
             
              <div  id="joinerData">
                           <label for="payEndDate" id="lblPayEndDate" style="color:#fff;font-size:11pt;font-weight:normal; font-family: Verdana;">Pay End Date
                             <input type="text" data-role="date" id="payEndDate" style="width:150px;height: 10px !important;" />
                           </label>
                           <label for="startDate" id="lblStartDate" style="color:#fff;font-size:11pt;font-weight:normal; font-family: Verdana;">Start Date
                             <input type="text" data-role="date" id="startDate" style="width:220px;height: 10px !important;" />
                           </label>
                           <label for="endDate" id="lblEndDate" style="color:#fff;font-size:11pt;font-weight:normal; font-family: Verdana;">End Date
                             <input type="text" data-role="date" id="endDate" style="width:220px;height: 10px !important;" />
                           </label>
                           <label for="email" id="lblEmail" style="font:Verdana;color:#fff;font-size:11pt;font-weight:normal;">Email
                             <input type="text" id="email" style="width: 350px !important; height: 25px !important;" />
                           </label>
                           
                           <label for="fileUpload" id="lblFileUpload" style="font:Verdana;color:#fff;font-size:11pt;font-weight:normal;">File Upload
                             <div class="ui-input-text ui-shadow-inset ui-corner-all ui-btn-shadow ui-body-c">
                               <input type="file" name="files[]" class="box__file" id="fileUpload" multiple="" class="ui-input-text ui-body-c">
                             </div>
                           </label>
                           
              </div>
              
              <div id="processButtons">

              <!-- <button class="show-page-loading-msg" data-theme="a" data-textonly="false" data-textvisible="true" data-msgtext="Loading theme a" data-inline="true">A</button>
-->
                            <a href="#" data-role="button" data-theme="c" class="btnAddLeaver" data-iconpos="top" data-icon="plus-square" style="font: helvetica; font-size: 10pt;
                               width: 150px;">New Penface Leavers</a>   
                           <a href="#" data-inline="true" data-role="button" data-theme="c" class="btnExecuteReport" data-iconpos="top" data-icon="list-ul"
                               style="font: helvetica; font-size: 10pt; width: 150px;">Execute Report</a> 
                           <a href="#" data-role="button" data-theme="c" class="btnExportPenface" data-iconpos="top" data-icon="sign-out" style="font: helvetica;
                               font-size: 10pt; width: 150px;">Export Penface Personal</a> <a href="#" data-role="button"
                               data-theme="c" class="btnExecuteService" data-iconpos="top" data-icon="plus" style="font: helvetica; font-size: 10pt;
                               width: 150px;">Execute Service Details</a> 
                           <a href="#" data-role="button" data-theme="c" class="btnExportFinance" data-iconpos="top" data-icon="sign-out" style="font: helvetica; font-size: 10pt;
                               width: 150px;">Export Finance Rates</a>
                           <a href="#" data-role="button" data-theme="c" class="btnFinalReport" data-iconpos="top" data-icon="list-ul" style="font: helvetica; font-size: 10pt;
                               width: 150px;">Final Report</a>
                           <a href="#" data-role="button" data-theme="c" class="btnExportPenfaceLeaver" data-iconpos="top" data-icon="sign-out" style="font: helvetica; font-size: 10pt;
                               width: 150px;">Export Penface Leaver</a>
                           <a href="#" data-role="button" data-theme="c" class="btnBannerJournal" data-iconpos="top" data-icon="sign-out" style="font: helvetica; font-size: 10pt;
                               width: 150px;">Banner Journal</a>
                           <a href="#" data-role="button" data-theme="c" class="btnExecuteFSSU" data-iconpos="top" data-icon="sign-out" style="font: helvetica; font-size: 10pt;
                               width: 150px;">Execute FSSU</a>
              </div>

              
              <div id="scroll">
                  <ul id="PenfaceList" style="position:absolute;left:20px;top:70px;width:70%;overflow-y: auto;height:70%;">
                  </ul>
              </div>

          </div>
          <div id="cover">
          </div>

         

          <div class="loading">
             <img src="images/wait.gif" alt="" width="300px" height="200px"/>
          </div>
         

          


      </div>

     
      <div data-role="footer" data-position="fixed">

          <nav data-role="navbar" class="txtColor">
            
           <ul class="penfaceFooter">
            <li><a href="#one" class="tabJoiner" data-theme="c" data-icon="sign-in" data-iconpos="top" style="font:verdana;font-size:11pt;font-weight:normal;">Create Joiner Import File</a></li>
            <li><a href="#four" class="tabFinanceSpreadsheet" data-iconpos="top" class="bg" data-theme="c" data-icon="info"  style="font:verdana;font-size:11pt;font-weight:normal;">Finance Spread Sheet</a></li>
            <li><a href="#two" class="tabFinDetImport" data-iconpos="top" class="bg" data-theme="c" data-icon="exchange"  style="font:verdana;font-size:11pt;font-weight:normal;">Financial Details Import File</a></li>
            <li><a href="#three" class="tabLeaverImport" data-iconpos="top" class="bg" data-theme="c" data-icon="sign-out"  style="font:verdana;font-size:11pt;font-weight:normal;">Leaver Import File</a></li>
            
           </ul>

           <ul class="joinerFooter">
            <li><a href="#one" class=" bg addJoiner" data-theme="c" data-icon="sign-in" data-iconpos="top">Add Joiner</a></li>
           </ul>

           <ul class="bannerFooter">
            <!--<li><a href="#one" class=" bg bannerExport" data-theme="c" data-icon="sign-in" data-iconpos="top">Banner Export</a></li>-->
             <li><a href="#one" class=" bg bannerJournal" data-theme="c" data-icon="sign-in" data-iconpos="top">Banner Journal</a></li>
           </ul>

           <ul class="creditUnionFooter">
            <li><a href="#one" class=" bg creditUnionImport" data-theme="c" data-icon="sign-in" data-iconpos="top">Data Import</a></li>
             <li><a href="#one" class=" bg creditUnionExport" data-theme="c" data-icon="sign-in" data-iconpos="top">Data Export</a></li>
           </ul>

           <ul class="fssuFooter">
              <li><a href="#one" class=" bg pfssu" data-theme="c" data-icon="sign-in" data-iconpos="top">FSSU</a></li>
           </ul>
           <ul class="nisExportFooter">
              <li><a href="#one" class=" bg nisExp" data-theme="c" data-icon="sign-in" data-iconpos="top">NIS Export</a></li>
           </ul>
            <ul class="studyTravelFooter">
              <li><a href="#one" class=" bg SandT" data-theme="c" data-icon="sign-in" data-iconpos="top">Study and Travel</a></li>
           </ul>
           <ul class="superAnnFooter">
              <li><a href="#one" class=" bg superAnnExp" data-theme="c" data-icon="sign-in" data-iconpos="top">Data Export</a></li>
               <li><a href="#one" class=" bg superAnnSub" data-theme="c" data-icon="sign-in" data-iconpos="top">Super Ann.</a></li>
               <li><a href="#one" class=" bg superAnnImp" data-theme="c" data-icon="sign-in" data-iconpos="top">Data Import</a></li>

           </ul>

          </nav>



      </div>
  </div>
  
  <div data-role="page" id="test">

      <div data-role="header" data-position="fixed">
           
           <div class="header">
      
             <center>
                <asp:Label ID="Label1" runat="server" Text="Payroll Reports" Font-Names="Verdana"
                     Font-Size="12pt" ForeColor="#ffffff"></asp:Label>
             </center> 
         

           </div>

           
           <a href="#home" data-transition="slide" data-icon="key" data-iconpos="top" data-iconpos="text" data-mini="true" class="logout" data-theme="b" >login</a>
          
          
      </div>
      
       <div data-role="main" class="ui-content">

        

       
       </div>

      

      
      <div data-role="footer" data-position="fixed">

          <nav data-role="navbar" class="txtColor">
            
            <!--
              <ul>
                  <li><a href="#searchProperty" data-icon="search" data-transition="slide" class = "ui-btn ui-btn-b">Search</a></li>
                  <li><a href="#createLogin" data-transition="slide" class = "ui-btn ui-btn-b" data-icon="info">Register</a></li>
                  <li><a href="#login" data-icon="key" data-transition="slide" class = "ui-btn ui-btn-b">Login</a></li>
              </ul>
              -->
          </nav>



      </div>
  </div> 
   
</asp:Content>
