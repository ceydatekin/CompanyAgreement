
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
                        data: "Kontenjan"
                    },
                    {
                        data: null,
                        className: "dt-center editor-edit",
                        defaultContent: '<button style= "border-width: inherit; border-color: white;" data-toggle="modal" data-target="#updateAmount" class="fas fa-pen"/>',
                        orderable: false
                    },
                ],
                columnDefs: [],
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


$('#updateAmount body').on('click', 'tr', function () {
    var id = this.id;
    var index = $.inArray(id, selected);

    if (index === -1) {
        selected.push(id);
    } else {
        selected.splice(index, 1);
    }

    $(this).toggleClass('selected');
});

$('#updateAmount').on('show.bs.modal', function (event) {

    console.log(datatable.row($(this)))
    var button = $()
    console.log("lkasfvls")
    tables = datatable.row($(this))
    var recipient = tables['context'][0]['aoData'][0]['_aData']['DepartmentName']
    var amount = tables['context'][0]['aoData'][0]['_aData']['Kontenjan']
    var modal = $(this)
    console.log(recipient)
    modal.find('#ModalDepartmentName').val(recipient)
    modal.find('#ModalAmount').val(amount);
})
