$('body').on('click', '#login-submit', function () {

    var userName = $('#userName').val();
    var password = $('#password').val();

    var formdata = new FormData();

    formdata.append('userName', userName);
    formdata.append('password', password);


    $.ajax({
        url: '/API/CompanyLogin',
        method: 'post',
        data: formdata,
        processData: false,
        contentType: false,
        success: function (resp) {

            if (resp == true) {
                window.location.href = "/Company/Index"
                console.log("buradayız2");
            }
            else {
                alert("hatalı Giriş")
                window.location.reload(2);
                console.log("buradayız3");


            }
        },
        error: function (err) {
            console.log(err)
        }
    });
});

$('body').on('click', '#logout', function () {





    $.ajax({
        url: '/API/CompanyLogout',
        method: 'post',
        processData: false,
        contentType: false,
        success: function (resp) {

            if (resp == true) {
                window.location.href = "/Home/Index"
                console.log("buradayız2");
            }
            else {
                alert("Çıkış Başarısız")
                window.location.reload(2);
                console.log("buradayız3");


            }
        },
        error: function (err) {
            console.log(err)
        }
    });
});
