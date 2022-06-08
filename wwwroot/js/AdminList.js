$(document).ready(function () {ShowList()});

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
                        data: null,
                        className: "dt-center editor-edit",
                        defaultContent: '<button style= "border-width: inherit; border-color: white;" data-toggle="modal" data-target="#updateAmount" class="fas fa-edit"/>',
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
                            data: null,
                            className: "dt-center editor-edit",
                            defaultContent: '<button style= "border-width: inherit; border-color: white;" data-toggle="modal" data-target="#updateAmount" class="fas fa-edit"/>',
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

            else if (jsonResp.success == false)
                console.log("hata olustu")
        },
        error: function (err) {
            console.log(err)
        }
    });
});
