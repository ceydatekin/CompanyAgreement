
$(document).ready(function () { Listele() });


$('body').on('click', '#CompanyAddQuota', function () {


    var DepartmentId = $('#DepartmentId').val();
    var Amount = $('#Amount').val();

    var formdata = new FormData();

    formdata.append('DepartmentId', DepartmentId);
    formdata.append('Amount', Amount);

    $.ajax({
        url: '/API/AddQuotaCompany',
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

    var ModalDepartmentName = $('#ModalDepartmentName').val();
    var ModalAmount = $('#ModalAmount').val();

    var formdata = new FormData();

    formdata.append('ModalDepartmentName', ModalDepartmentName);
    formdata.append('ModalAmount', ModalAmount);

    $.ajax({
        url: '/API/UpdateQuota',
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


var datatable;
function Listele() {
    $.ajax({
        type: 'GET',
        url: '/API/quotaListCompany',
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
                        data: "Kontenjan",
                    },
                    {
                        data: "departmantID",
                        className: "dt-center editor-edit"
                    },
                ],
                columnDefs: [{
                    targets: 0,
                    render: function (data, type, full, meta) {
                        return data+ 'asdasdasd';
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
                            return '<button style="border-width: inherit; border-color: white;" id="deneme" onclick="getDepartmant(' + data + ')" class="fas fa-pen" />';
                        }
                    },
                    ],

                order: [[1, "asc"]],
                colReorder: true,
                scrollX: '50px',
                select: {
                    style: 'single'
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

function getDepartmant(ID) {
    $('#quataForm').trigger("reset");

    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        processData: false,
        dataType: "json",
        url: '/API/openModal?getDepartmant=OK&ID='+ ID,
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
                $('#updateAmount').modal('show');


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

$('body').on('click', '#deneme', function (event) {
    tables = datatable.row($(this))
   
    $('#SelectQuota tbody').on('click', 'tr', function () {
       // console.log(tables.$('tr.selected').find("td").value)
        d = tables.$('tr.selected')
       // console.log(d.find("td").value)
        console.log(d[0])

    });
});

//var table = $('#SelectQuota').DataTable();

//$('#SelectQuota tbody').on('click', 'tr', function () {
//    console.log(table.row(this).data());
//});

$(document).ready(function () {


    //$("#deneme").on('click', function () {
    //    var currentRow = $(this).closest("tr");
    //    var coll = currentRow.
    //});

    var table = $('#SelectQuota').DataTable();

 

    $('#SelectQuota tbody').on('click', 'tr', function () {

        var selection = table.$('tr.selected')[0]
        /*        console.log(selection.p("td:nth-child(1)").value)*/
        console.log(table.$('tr.selected')[0])
        //console.log(table.$('tr.selected').data)
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });


});

//$('#updateAmount').on('show.bs.modal', function (event) {

//    console.log(datatable.row($(this)))
//    console.log("lkasfvls")
//    tables = datatable.row($(this))
//    d = tables.$('tr.selected')
//   // console.log(tables.find("td:nth-child(1)").value)
//    console.log(d.find("td").value)
//    // console.log(document.getElementById('updateAmount'))
//    var recipient = tables['context'][0]['aoData'][0]['_aData']['DepartmentName']
//    var amount = tables['context'][0]['aoData'][0]['_aData']['Kontenjan']
//    var modal = $(this)
//    console.log(recipient)
//    modal.find('#ModalDepartmentName').val(recipient)
//    modal.find('#ModalAmount').val(amount);
//});