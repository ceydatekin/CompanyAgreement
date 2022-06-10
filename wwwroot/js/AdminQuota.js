$('body').on('click', '#quotaAdd', function () {

    var CompanyId = $('#recipient-name').val();
    var DepartmentId = $('#Department').val();
    var Amount = $('#Amount').val();
    var formdata = new FormData();
    formdata.append('CompanyId', CompanyId);
    formdata.append('DepartmentId', DepartmentId);
    formdata.append('Amount', Amount);

    $.ajax({
        url: '/API/AddQuotaAdmin',
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
                $('#selectQuota').modal('hide')
                $(document).ready(function () { Listele() });
            }

            else if (jsonResp.success == false)
                console.log("hata olustu")
        },
        error: function (err) {
            console.log(err)
        }
    });
});


$('body').on('click', '#CompanyUpdateQuoata', function () {
    var CompanyId = $('#CompanyId').val();
    var ModalDepartmentName = $('#ModalDepartmentName').val();
    var ModalAmount = $('#ModalAmount').val();

    var formdata = new FormData();
    formdata.append('CompanyId', CompanyId);
    formdata.append('ModalDepartmentName', ModalDepartmentName);
    formdata.append('ModalAmount', ModalAmount);

    $.ajax({
        url: '/API/UpdateQuotaAdmin',
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
                $('#updateAmountAdmin').modal('hide')
                $(document).ready(function () { Listele() });
            }

            else if (jsonResp.success == false)
                console.log("hata olustu")
        },
        error: function (err) {
            console.log(err)
        }
    });
});

$('#selectQuota').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget)
    console.log("lkasfvls")
    var recipient = $('#CompanyId').val();
    var modal = $(this)
    console.log(recipient)
    modal.find('#recipient-name').val(recipient)
})



$('body').on('change', '#Department', function () {
    var DepartmentId = $('#Department').val();
    var CompanyId = $('#recipient-name').val();

    console.log(DepartmentId)

    $.ajax({
        url: '/API/selectDepertmentAdmin',
        type: 'GET',
        contentType: 'json',
        data: { companyId: CompanyId, departmentId: DepartmentId },
        dataType: 'json',
        success: function (resp) {
            console.log(resp)
            $('#Amount').val(resp)


        }
    });

});



var datatable;
function Listele() {
    var companyId = $('#CompanyId').val();
    $.ajax({
        type: 'GET',
        url: '/API/quotaListAdmin?companyId=' + companyId,
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: "json",
        success: function (resp) {
            console.log(resp)
            $('#SelectQuota').DataTable().clear().destroy();
            datatable = $('#SelectQuota').DataTable({
                orderCellsTop: true,
                fixedHeader: true,
                searching: false,
                lengthChange: false,
                bInfo: false,
                ordering: true,
                data: resp.data,
                columns: [
                    {
                        data: "DepartmentName"
                    },
                    {
                        data: "Kontenjan"
                    },
                    {
                        data: "departmantID",
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
                        return '<button style="border-width: inherit; border-color: white;" id="deneme" onclick="getDepartmant(' + data +',' +companyId  + ')" class="fas fa-pen" />';
                    }
                },
                ],
                order: [[1, "asc"]],
                colReorder: true,
                scrollX: '50px',
                select: {
                    style: 'multi'
                },
            });
        }
    });
};


function getDepartmant(ID, companyId) {
    $('#quataForm').trigger("reset");

    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        processData: false,
        dataType: "json",
        url: '/API/openModalAdmin?getDepartmant=OK&ID=' + ID + '&companyId=' + companyId,
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
                $('#ModalDepartmentName').val(respond.data[0].DepartmentName);
                $('#ModalAmount').val(respond.data[0].Kontenjan);
                $('#CompanyId').val(respond.data[0].CompanyId);
                $('#updateAmountAdmin').modal('show');


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

$('body').on('change', '#CompanyId', function () {
    $(document).ready(function () { Listele() });
});



//$('#updateAmountAdmin').on('show.bs.modal', function (event) {
//    var button = $(event.relatedTarget)
//    console.log("lkasfvls")
//    tables = datatable.row($(this))
//    var recipient = tables['context'][0]['aoData'][0]['_aData']['DepartmentName']
//    var amount = tables['context'][0]['aoData'][0]['_aData']['Kontenjan']
//    var companyId = $('#CompanyId').val();
//    var modal = $(this)
//    console.log(recipient)
//    modal.find('#ModalDepartmentName').val(recipient)
//    modal.find('#ModalAmount').val(amount);
//    modal.find('#recipient-name').val(companyId)
//})
