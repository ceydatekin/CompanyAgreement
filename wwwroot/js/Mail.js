
$('body').on('click', '#AsigmentPassword', function () {

    var Name = $('#Name').val();
    var Surname = $('#Surname').val();
    var UserName = $('#UserName').val();
    var DepartmentId = $('#DepartmentId').val();

    var formdata = new FormData();

    formdata.append('Name', Name);
    formdata.append('Surname', Surname);
    formdata.append('UserName', UserName);
    formdata.append('DepartmentId', DepartmentId);

    $.ajax({
        url: '/API/mailSend',
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
