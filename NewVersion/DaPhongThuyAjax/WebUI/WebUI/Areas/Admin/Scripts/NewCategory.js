

var NewCategoryController = {
    init: function () {
     
        NewCategoryController.loadData();
        NewCategoryController.loadDropDown();
        NewCategoryController.registerEvent();


    },
    loadDropDown: function (id) {
        $.ajax({
            url: 'LoadDropDown',
            type: 'GET',
            data: { id: id },
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var html = '';
                    var data = response.data;
                    var template = $('#data-dropdownParent').html();
                    $('.drd-IsParent').html('<option value="0">Không Có Nhóm Cha</option>');
                    $.each(data, function (i, item) {
                        html = Mustache.render(template, {
                            Id: item.NewCategoryID,
                            Name: item.Ten
                        });
                        $('.drd-IsParent').append(html);
                    });
                }
            }
        })
    },

    loadData: function () {
        $.ajax({
            url: 'LoadData',
            type: 'GET',
            dataType: 'json',
            success: function (response) {

                var html = '';
                var data = response.data;
                var template = $('#data-template-Active').html();
                var template_Lock = $('#data-template-Locked').html();
                var table = $("#Table").DataTable({
                    dom: "Blfrtip",
                    buttons: [
                    {
                        extend: "copy",
                        className: "btn-sm",

                    },
                    {
                        extend: "csv",
                        className: "btn-sm",
                        text: "File Excel"
                    },
                    {
                        extend: "pdf",
                        className: "btn-sm"
                    },
                    {
                        extend: "pdfHtml5",
                        className: "btn-sm",
                        text: "In"
                    },
                    {
                        extend: "print",
                        className: "btn-sm",
                        text: "In"
                    },
                    ],

                    "pagingType": "full_numbers",
                    destroy: true,
                    "ordering": false,
                    responsive: true,
                    "language": {
                        "lengthMenu": "Hiển Thị _MENU_ Bản Ghi Trên Trang",
                        "search": "Tìm Kiếm:",
                        "info": "Hiển Thị _START_ Tới _END_ Của _TOTAL_ Bản Ghi",
                        "paginate": {
                            "first": "Đầu",
                            "last": "Cuối",
                            "next": "Tới",
                            "previous": "Lui"
                        },
                        "zeroRecords": "Không Có Bản Ghi Nào Được Tìm Thấy",
                        "infoEmpty": "Không Có Bản Ghi Nào Hiển Thị",
                        "infoFiltered": "(Trong Tổng Cộng _MAX_ Bản Ghi)",
                    },
                    "fnDrawCallback": function (oSettings) {
                        NewCategoryController.registerEvent();
                    }
                });

                table.clear();
                $.each(data, function (i, item) {

                    if (item.TrangThai == true) {
                        html = Mustache.render(template, {
                            Id: item.NewCategoryID,
                            Name: item.Ten,
                            SortOrder: item.ThuTuHienThi,
                            ParentId: item.ParentID,
                            CreatedDate: item.NgayTao,
                            ModifiedDate: item.NgayChinhSua
                        });

                    }
                    else {
                        html = Mustache.render(template_Lock, {
                            Id: item.NewCategoryID,
                            Name: item.Ten,
                            SortOrder: item.ThuTuHienThi,
                            ParentId: item.ParentID,
                            CreatedDate: item.NgayTao,
                            ModifiedDate: item.NgayChinhSua
                        });
                    }
                    table.row.add($(html)).draw();

                });
                $(document).ready(function () {
                    $("#Table").treetable({ expandable: true }, "unloadBranch");
                });
                NewCategoryController.registerEvent();
            }
        });
    },

    resetFormDelete: function () {
        $('#submitDelete').show();
        $('#resultDelete').removeClass()
            .addClass('btn btn-warning fa fa-exclamation-triangle')
                           .text('Bạn Muốn Xóa Danh Mục Này?');
    },

    resetForm: function () {  
            NewCategoryController.loadDropDown();
            $('#btn-ctnCreate').hide();
            $('#modalCrUdTitle').removeClass().addClass('btn btn-info').text('Thêm Mới Danh Mục Tin Tức');
            $('#hid-Id').val('0');
            $('#txt-Name').val('');
            $('#txt-SortOrder').val('');
            $('#ckstatus').prop('checked', true);
            $('#btn-crOrUd').text('Thêm Mới');
            $('#dismisModal').show();
            $('#btn-crOrUd').show();
    },

    loadDetail: function (id) {

        $.ajax({
            url: 'LoadDetails',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#modalCrUdTitle').text('Cập Nhật Danh Mục Tin Tức');
                    $('#hid-Id').val(data.NewCategoryID);
                    $('#txt-Name').val(data.Ten);
                    $('#txt-SortOrder').val(data.ThuTuHienThi);
                    $('#ckstatus').prop('checked', data.TrangThai);
                    $('.drd-IsParent').val(parseInt(data.ParentID));
                    $('#btn-crOrUd').text('Cập Nhật');
                    $('#dismisModal').show();
                    $('#btn-crOrUd').show();
                    $('.btn-finish').hide();
                    $('#btn-ctnCreate').hide();
                }
                else {
                    alert(response.message);
                }
            }
        });
    },


    registerEvent: function () {
        $(document).off('submit').on('submit', '#frm-Delete', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'GET',
                url: 'DeleteNewCategory',
                data: $(this).serialize(),
                success: function (response) {
                    if (response.status == true) {
                        $('#resultDelete').addClass('btn-success fa fa-check-square')
                            .text(response.message);
                        $('#submitDelete').hide();
                        $('.btn-reload').removeClass('btn-default')
                            .addClass('btn-info')
                        NewCategoryController.loadData();
                    }
                    if (response.status == false) {
                        $('#resultDelete').addClass('btn-danger fa fa-exclamation-triangle')
                            .text(response.message);
                    }
                },

            });
        });


        $(document).on('submit', '.frm-Create', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: 'SaveNewCategory',
                data: $(this).serialize(),
                success: function (response) {
                    if (response.status == false) {
                        $('#modalCrUdTitle').removeClass()
                            .addClass('btn-danger fa fa-exclamation-triangle')
                            .html(response.failReason);
                    }
                    else {
                        $('#modalCrUdTitle').html(response.success)
                         .addClass('btn-success fa fa-check-square');
                        $('#btn-ctnCreate').show();
                        $('#btn-crOrUd').hide();
                        NewCategoryController.loadData();
                    }

                }
            });

        });
        $('#btn-Add').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            NewCategoryController.loadDropDown();
            NewCategoryController.resetForm();
        });

        $('.btn-edit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
  
            NewCategoryController.loadDetail(id);
        });

        $('.btnDelete').off('click').on('click', function () {
            $('#modalDelete').modal('show')
            var id = $(this).data('id');
            $('#hid-IdDelete').val(id);
            NewCategoryController.resetFormDelete();
        });

        $('#btn-ctnCreate').off('click').on('click', function () {
            NewCategoryController.resetForm();
   
            $('.btn-finish').show();
        });
    }
}

NewCategoryController.init();

