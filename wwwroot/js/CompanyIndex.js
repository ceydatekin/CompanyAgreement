
$('body').on('click', '#companyadd', function () {

    var CompanyAuthority_SGKNO = $('#CompanyAuthority_SGKNO').val();
    var CompanyAuthority_TaxNumber = $('#CompanyAuthority_TaxNumber').val();
    var CompanyAuthority_ContractDate = $('#CompanyAuthority_ContractDate').val();
    var formdata = new FormData();
    formdata.append('CompanyAuthority_SGKNO', CompanyAuthority_SGKNO);
    formdata.append('CompanyAuthority_TaxNumber', CompanyAuthority_TaxNumber);
    formdata.append('CompanyAuthority_ContractDate', CompanyAuthority_ContractDate);


    console.log("eklemei is şladmlas")
    $.ajax({
        url: '/API/CompanyPage',
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



