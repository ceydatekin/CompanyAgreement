$(document).ready(function () { ShowListCompany() });


var datatable;
function ShowListCompany() {
    $.ajax({
        type: 'GET',
        url: '/API/ListAcademicianCompany',
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        dataType: "json",
        success: function (resp) {
            console.log(resp)
            $('#CompanyListAcademician').DataTable().clear().destroy();
            datatable = $('#CompanyListAcademician').DataTable({
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




$('body').on('click', '#filterCompanyAcademician', function () {

    var Situations = $('#Situations').val();
    var PublicPrivate = $('#PublicPrivate').val();
    var Location = $('#Location').val();

    var formdata = new FormData();
    formdata.append('Situations', Situations);
    formdata.append('PublicPrivate', PublicPrivate);
    formdata.append('Location', Location);

    $.ajax({
        url: '/API/FilterCompanyAcademician',
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

                $('#CompanyListAcademician').DataTable().clear().destroy();
                datatable = $('#CompanyListAcademician').DataTable({
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
