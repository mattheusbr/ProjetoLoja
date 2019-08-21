// Call the dataTables jQuery plugin
$(document).ready(function() {
    $('#dataTable').DataTable({
        language: {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ at\xE9 _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 at\xE9 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ Resultados por p\xE1gina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Pr\xF3ximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "\xFAltimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
});
