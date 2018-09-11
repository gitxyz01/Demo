

var CategoryController = {
    init: function () {

        CategoryController.loadData();

        CategoryController.registerEvent();


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
                var table = $("#ProviderTable").DataTable({

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
                        CategoryController.registerEvent();
                    }

                });

                table.clear();
                $.each(data, function (i, item) {

                    if (item.TrangThai == true) {
                        html = Mustache.render(template, {
                            SortOrder: item.ThuTuHienThi,
                            CategoryName: item.Ten,
                            Quantity: item.SoLuongSanPham,                         
                            CreatedDate: item.NgayTao,
                            ModifiedDate: item.NgayChinhSua,
                            CategoryId: item.CategoryId
                        });

                    }
                    else {
                        html = Mustache.render(template_Lock, {
                            SortOrder: item.ThuTuHienThi,
                            CategoryName: item.Ten,
                            Quantity: item.SoLuongSanPham, 
                            CreatedDate: item.NguoiTao,
                            ModifiedDate: item.NgayChinhSua,
                            CategoryId: item.CategoryId
                        });
                    }
                    table.row.add($(html)).draw();

                });
                CategoryController.registerEvent();
            }
        });
    },

    resetFormDelete: function () {
        $('#submitDelete').show();
        $('#resultDelete').removeClass()
            .addClass('btn btn-warning fa fa-exclamation-triangle')
                           .text('Bạn Muốn Xóa Danh Mục Sản Phẩm Này?');
    },

    resetForm: function () {
        $('#btn-ctnCreate').hide();
        $('#modalCrUdTitle').removeClass().addClass('btn btn-info').text('Thêm Mới Danh Mục Sản Phẩm');
        $('#hid-CategoryID').val('0');
        $('#txt-Ten').val('');
        $('#ckstatus').prop('checked', true);
        $('#txt-ThuTuHienThi').val('');
        $('#btn-crOrUd').text('Thêm Mới');
        $('#dismisModal').show();
        $('#btn-crOrUd').show();
    },

    loadDetail: function (id) {

        $.ajax({
            url: 'EditCategory',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#modalCrUdTitle').text('Cập Nhật Danh Mục Sản Phẩm').removeClass().addClass('btn btn-info');
                    $('#hid-CategoryProductID').val(data.CategoryId);
                    $('#txt-Ten').val(data.Ten);
                    $('#txt-ThuTuHienThi').val(data.ThuTuHienThi);
                    $('#ckstatus').prop('checked', data.TrangThai);
  
                    $('#btn-crOrUd').text('Cập Nhật');
                    $('#dismisModal').show();
                    $('#btn-crOrUd').show();
                    $('#btn-ctnCreate').hide();
                }
                else {
                    alert(response.message);
                }
            }
        });
    },
    registerEvent: function () {
        $(document).off('submit').on('submit', '#frm-DeleteProvider', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'GET',
                url: 'DeleteCategory',
                data: $(this).serialize(),
                success: function (response) {

                    if (response.status == true) {
                        $('#resultDelete').addClass('btn-success fa fa-check-square')
                            .text(response.message);
                        $('#submitDelete').hide();
                        $('.btn-reload').removeClass('btn-default')
                            .addClass('btn-info')
                        CategoryController.loadData();
                    }
                    else {
                        $('#resultDelete').addClass('btn-danger fa fa-exclamation-triangle')
                            .text(response.message);

                    }
                },

            });
        });


        $(document).on('submit', '.frm-CreateProvider', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: 'SaveCategory',
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
                        CategoryController.loadData();
                    }
                }
            });

        });
        $('#btn-AddProvider').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            CategoryController.resetForm();
        });

        $('.btn-Edit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            CategoryController.loadDetail(id);
        });

        $('.btn-Delete').off('click').on('click', function () {
            $('#modalDelete').modal('show')
            var id = $(this).data('id');
            $('#hid-ProviderIdDelete').val(id);
            CategoryController.resetFormDelete();
        });

        $('#btn-ctnCreate').off('click').on('click', function () {
            CategoryController.resetForm();
            $('.btn-finish').show();
        });
    }
}

CategoryController.init();

