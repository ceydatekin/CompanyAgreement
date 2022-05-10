
$('body').on('click', '#quotaAdd', function () {

    var CompanyId = $('#recipient-name').val();
    var DepartmentId = $('#Department').val();
    var Amount = $('#Amount').val();

    var formdata = new FormData();
    formdata.append('CompanyId', CompanyId);
    formdata.append('DepartmentId', DepartmentId);
    formdata.append('Amount', Amount);

    $.ajax({
        url: '/API/AddQuotaAcademician',
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
        url: '/API/selectDepertmentAcademician',
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
        url: '/API/quotaListAcademician?companyId=' + companyId,
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
                bInfo: true,
                ordering: true,
                data: resp.data,
                columns: [
                    {
                        data: "DepartmentName"
                    },
                    {
                        data: "Kontenjan"
                    },
                ],
                columnDefs: [],
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
$('body').on('change', '#CompanyId', function () {
    $(document).ready(function () { Listele() });
});