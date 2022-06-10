$(document).ready(function () { ShowList() });

var datatable;
function ShowList() {
    $.ajax({
        type: 'GET',
        url: '/API/ListAdminCompany',
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: "json",
        success: function (resp) {
            console.log(resp)
            $('#CompanyListele').DataTable().clear().destroy();
            datatable = $('#CompanyListele').DataTable({
                orderCellsTop: true,
                fixedHeader: true,
                searching: false,
                lengthChange: false,
                bInfo: false,
                ordering: true,

                data: resp.data,
                columns: [
                    {
                        data: "CompanyName"
                    },
                    {
                        data: "PublicPrivate"
                    },

                    {
                        data: "Situation"
                    },
                    {
                        data: "MeetingDate"
                    },
                    {
                        data: "NameSurname"
                    },
                    {
                        data: "location"
                    },
                    {
                        data: "companyId",
                        className: "dt-center editor-edit"
                    },
                ],
                columnDefs: [{
                    targets: 0,
                    render: function (data, type, full, meta) {
                        return data;
                    }
                },
                {
                    targets: 1,
                    render: function (data, type, full, meta) {
                        return data;
                    }
                },
                {
                    targets: 2,
                    render: function (data, type, full, meta) {
                        return data;
                    }
                }, {
                    targets: 3,
                    render: function (data, type, full, meta) {
                        return data;
                    }
                }, {
                    targets: 4,
                    render: function (data, type, full, meta) {
                        return data;
                    }
                }, {
                    targets: 5,
                    render: function (data, type, full, meta) {
                        return data;
                    }
                },
                {
                    targets: 6,
                    render: function (data, type, full, meta) {
                        return '<button style="border-width: inherit; border-color: white;" id="deneme" onclick="getCompany(' + data + ')" class="fas fa-pen" />';
                    }
                },
                ],
                order: [[1, "asc"]],
                colReorder: true,
                scrollX: '50px',
                select: {
                    style: 'multi'
                },
                paginate: {
                    first: "İlk",
                    previous: "Önceki",
                    next: "Sonraki",
                    last: "Son",
                },

            });
        }
    });
};

function getCompany(ID) {

    $('#updateCompanyForm').trigger("reset");

    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        processData: false,
        dataType: "json",
        url: '/API/openModalCompay?getCompany=OK&ID=' + ID,
        /*data: {
            ID: ID, getDepartmant: 'OK'
        },*/
        //data: formdata,
        beforeSend: function () {
            Swal.fire({
                title: 'Lütfen bekleyin',
                html: 'Bilgiler getriliyor..',
                allowEscapeKey: false,
                allowOutsideClick: false,
                didOpen: () => {
                    Swal.showLoading();
                }
            });
        },
        success: function (respond) {
            Swal.close();
            if (respond.status === true) {
                //Swal.fire({ icon: 'success', title: 'Başarılı', text: respond.message, });
                $('#CompanyInformation_Name').val(respond.data[0].CompanyInformation_Name);
                $('#CompanyInformation_Surname').val(respond.data[0].CompanyInformation_Surname);
                $('#CompanyInformation_mail').val(respond.data[0].CompanyInformation_mail);
                $('#CompanyInformation_GSM').val(respond.data[0].CompanyInformation_GSM);
                $('#CompanyAuthority_SGKNO').val(respond.data[0].CompanyAuthority_SGKNO);
                $('#CompanyAuthority_TaxNumber').val(respond.data[0].CompanyAuthority_TaxNumber);
                $('#CompanyAuthority_ContractDate').val(respond.data[0].CompanyAuthority_ContractDate);
                $('#CompanyName').val(respond.data[0].CompanyName);
                $('#Description').val(respond.data[0].Description);
                $('#ContractInformation_Mail').val(respond.data[0].ContractInformation_Mail);
                $('#ContractInformation_GSM').val(respond.data[0].ContractInformation_GSM);
                $('#ContractInformation_Adress').val(respond.data[0].ContractInformation_Adress);
                $('#ContractInformation_Province').val(respond.data[0].ContractInformation_Province);
                $('#ContractInformation_District').val(respond.data[0].ContractInformation_District);
                $('#PublicPrivate').val(respond.data[0].PublicPrivate);
                $('#Situation').val(respond.data[0].Situation);
                $('#CompanyId').val(respond.data[0].CompanyId);
                $('#MeetingDate').val(respond.data[0].MeetingDate);
                $('#updateCompany').modal('show');


            }
            else {
                Swal.fire({ icon: 'error', title: 'Hata', text: respond.message, });
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            Swal.close();
            console.log(xhr.status, ajaxOptions, thrownError);
            Swal.fire({ icon: 'error', title: 'Hata', text: 'Sistem hatası, sistem yöneticinizle görüşünüz.', });
        }
    });

}


$('body').on('click', '#filterCompanyAdmin', function () {

    var Situations = $('#Situations').val();
    var PublicPrivate = $('#PublicPrivate').val();
    var Location = $('#Location').val();

    var formdata = new FormData();
    formdata.append('Situations', Situations);
    formdata.append('PublicPrivate', PublicPrivate);
    formdata.append('Location', Location);

    $.ajax({
        url: '/API/FilterCompany',
        method: 'post',
        data: formdata,
        processData: false,
        contentType: false,
        dataType: "json",
        success: function (resp) {
            console.log(resp)
            // var jsonResp = JSON.parse(resp);
            // console.log(jsonResp)
            if (resp.success == true) {
                console.log("başarılı")

                $('#CompanyListele').DataTable().clear().destroy();
                datatable = $('#CompanyListele').DataTable({
                    orderCellsTop: true,
                    fixedHeader: true,
                    searching: false,
                    lengthChange: false,
                    bInfo: false,
                    ordering: true,

                    data: resp.data,
                    columns: [
                        {
                            data: "CompanyName"
                        },
                        {
                            data: "PublicPrivate"
                        },

                        {
                            data: "Situation"
                        },
                        {
                            data: "MeetingDate"
                        },
                        {
                            data: "NameSurname"
                        },
                        {
                            data: "location"
                        },
                        {
                            data: "companyId",
                            className: "dt-center editor-edit"
                        },
                    ],
                    columnDefs: [{
                        targets: 0,
                        render: function (data, type, full, meta) {
                            return data;
                        }
                    },
                    {
                        targets: 1,
                        render: function (data, type, full, meta) {
                            return data;
                        }
                    },
                    {
                        targets: 2,
                        render: function (data, type, full, meta) {
                            return data;
                        }
                    }, {
                        targets: 3,
                        render: function (data, type, full, meta) {
                            return data;
                        }
                    }, {
                        targets: 4,
                        render: function (data, type, full, meta) {
                            return data;
                        }
                    }, {
                        targets: 5,
                        render: function (data, type, full, meta) {
                            return data;
                        }
                    },
                    {
                        targets: 6,
                        render: function (data, type, full, meta) {
                            return '<button style="border-width: inherit; border-color: white;" id="deneme" onclick="getDepartmant2(' + data + ')" class="fas fa-pen" />';
                        }
                    },
                    ],
                    order: [[1, "asc"]],
                    colReorder: true,
                    scrollX: '50px',
                    select: {
                        style: 'multi'
                    },
                    paginate: {
                        first: "İlk",
                        previous: "Önceki",
                        next: "Sonraki",
                        last: "Son",
                    },

                });

            }

            else if (jsonResp.success == false)
                console.log("hata olustu")
        },
        error: function (err) {
            console.log(err)
        }
    });
});




$('body').on('click', '#CompanyUpdateButton', function () {

    var CompanyName = $('#CompanyName').val();
    var MeetingDate = $('#MeetingDate').val();
    var PublicPrivate = $('#PublicPrivate').val();
    var Situation = $('#Situation').val();
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
    var CompanyId = $('#CompanyId').val();

    var formdata = new FormData();
    formdata.append('CompanyName', CompanyName);
    formdata.append('MeetingDate', MeetingDate);
    formdata.append('PublicPrivate', PublicPrivate);
    formdata.append('Situation', Situation);
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
    formdata.append('CompanyId', CompanyId);

    console.log("Ekleme Tamamlandı")
    $.ajax({
        url: '/API/UpdateCompany',
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
                $('#updateCompany').modal('hide');
                $(document).ready(function () { ShowList() });
                //setTimeout("window.location.reload(true);", 1000);
            }

            else if (jsonResp.success == false)
                console.log("hata olustu")
        },
        error: function (err) {
            console.log(err)
        }
    });
});
