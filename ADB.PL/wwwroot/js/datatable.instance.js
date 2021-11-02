$(function () {
    $('.js-basic-example').DataTable({
        responsive: true,
        paging: true,
        pageLength: 5,
        bSort: true,
        bFilter: true,
        //bLengthChange: false,
        //bInfo: false,
        lengthMenu: [[3, 5, 10, 25, 50, -1], [3, 5, 10, 25, 50, "All"]],

    });

    var table = $('.js-basic-example').DataTable();

    new $.fn.dataTable.Buttons(table, {
        buttons: [
            'copy', 'excel', 'pdf', 'csv', 'print'
        ]
    });

    table.buttons().container().appendTo($('.col-sm-6:eq(0)', table.table().container()));
});