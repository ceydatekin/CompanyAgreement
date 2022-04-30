
$('body').on('click', '#quotaAdd', function () {

    var CompanyId = $('#CompanyId').val();
    var DepartmentId = $('#DepartmentId').val();
    var Amount = $('#Amount').val();
   
    var formdata = new FormData();
    formdata.append('CompanyId', CompanyId);
    formdata.append('DepartmentId', DepartmentId);
    formdata.append('Amount', Amount);

    $.ajax({
        url: '/API/AddQuota',
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
