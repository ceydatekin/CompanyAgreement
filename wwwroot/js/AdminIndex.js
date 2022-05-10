
$('body').on('click', '#companyadd', function () {
    console.log("deneme")
    var CompanyName = $('#CompanyName').val();
    var MeetingDate = $('#MeetingDate').val();
    var PublicPrivate = $('#PublicPrivate').val();
    var Situations = $('#Situations').val();
    var Description = $('#Description').val();
    var CompanyInformation_mail = $('#CompanyInformation_mail').val();
    var CompanyInformation_GSM = $('#CompanyInformation_GSM').val();
    var CompanyInformation_Name = $('#CompanyInformation_Name').val();
    var CompanyInformation_Surname = $('#CompanyInformation_Surname').val();
    var ContractInformation_Adress = $('#ContractInformation_Adress').val();
    var ContractInformation_Province = $('#ContractInformation_Province').val();
    var ContractInformation_District = $('#ContractInformation_District').val();
    var ContractInformation_Mail = $('#ContractInformation_Mail').val();
    var ContractInformation_Gsm = $('#ContractInformation_Gsm').val();
    var formdata = new FormData();
    formdata.append('CompanyName', CompanyName);
    formdata.append('MeetingDate', MeetingDate);
    formdata.append('PublicPrivate', PublicPrivate);
    formdata.append('Situations', Situations);
    formdata.append('Description', Description);
    formdata.append('CompanyInformation_mail', CompanyInformation_mail);
    formdata.append('CompanyInformation_GSM', CompanyInformation_GSM);
    formdata.append('CompanyInformation_Surname', CompanyInformation_Surname);
    formdata.append('CompanyInformation_Name', CompanyInformation_Name);
    formdata.append('ContractInformation_Adress', ContractInformation_Adress);
    formdata.append('ContractInformation_Province', ContractInformation_Province);
    formdata.append('ContractInformation_District', ContractInformation_District);
    formdata.append('ContractInformation_Mail', ContractInformation_Mail);
    formdata.append('ContractInformation_Gsm', ContractInformation_Gsm);

    console.log("eklemei is şladmlas")
    $.ajax({
        url: '/API/AddCompanyAdmin',
        method: 'post',
        data: formdata,
        processData: false,
        contentType: false,
        success: function (resp) {
            console.log(resp)
            var jsonResp = JSON.parse(resp);
            console.log(jsonResp)
            if (jsonResp.success == true) {
                console.log("başarılı")
                setTimeout("window.location.reload(true);", 1000);
            }
                
            else if (jsonResp.success == false)
                console.log("hata olustu")
        },
        error: function (err) {
            console.log(err)
        }
    });
});
