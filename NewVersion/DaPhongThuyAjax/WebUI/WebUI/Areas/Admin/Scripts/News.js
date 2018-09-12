

var NewController = {
    init: function () {
        NewController.loadData();
        NewController.registerEvent();
    },

    loadData: function (categoryId) {
        $.ajax({
            url: 'LoadData',
            data: {
                id: categoryId
            },
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
                        NewController.registerEvent();
                    }

                });
                var x = ''
                table.clear();
                $.each(data, function (i, item) {
                    if (item.TrangThai == true) {

                        html = Mustache.render(template, {
                            Name: item.Ten,
                            Image: item.HinhAnh,
                            SortOrder: item.ThuTuHienThi,
                            CategoryName: item.NewCategoryName,
                            CreatedDate: item.NgayTao,
                            ModifiedDate: item.NgayChinhSua,
                            Id: item.NewID,
                        });
                    }
                    else {
                        html = Mustache.render(template_Lock, {
                            Name: item.Ten,
                            Image: item.HinhAnh,
                            SortOrder: item.ThuTuHienThi,
                            CategoryName: item.NewCategoryName,
                            CreatedDate: item.NgayTao,
                            ModifiedDate: item.NgayChinhSua,
                            Id: item.NewID,
                        });
                    }
                    table.row.add($(html)).draw();
                });
                NewController.registerEvent();
            }
        });
    },

    resetFormDelete: function () {
        $('#submitDelete').show();
        $('#resultDelete').removeClass()
            .addClass('btn btn-warning fa fa-exclamation-triangle')
                           .text('Bạn Muốn Xóa Tin Tức Này?');
    },

    resetForm: function () {
        $('#btn-ctnCreate').hide();
        $('#modalCrUdTitle').removeClass().addClass('btn btn-info').text('Thêm Mới Sản Phẩm');
        $('#hid-Id').val('0');
        $('#txt-Name').val('');
        $('#HinhAnh').val('');
        $('.DrdCategory').val('');
        $('#txt-SortOrder').val('');
        $('#ckeContent').val('');
        $('#ckstatus').prop('checked', true);
        $('#btn-crOrUd').text('Thêm Mới');
        $('#dismisModal').show();
        $('#btn-crOrUd').show();
    },

    loadDetail: function (id) {

        $.ajax({
            url: 'LoadDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#modalCrUdTitle').text('Cập Nhật Tin Tức').removeClass().addClass('btn btn-info');
                    $('#hid-Id').val(data.NewID);
                    $('#txt-Name').val(data.Ten);
                    $('#HinhAnh').val(data.HinhAnh);
                    $('.DrdCategory').val(parseInt(data.NewCategoryID));
                    $('#txt-SortOrder').val(data.ThuTuHienThi);
                    CKEDITOR.instances['ckeContent'].setData(data.NoiDungTinTuc);   
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
        $(document).off('submit').on('submit', '#frm-Delete', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'GET',
                url: 'DeleteNew',
                data: $(this).serialize(),
                success: function (response) {

                    if (response.status == true) {
                        $('#resultDelete').addClass('btn-success fa fa-check-square')
                            .text(response.message);
                        $('#submitDelete').hide();
                        $('.btn-reload').removeClass('btn-default')
                            .addClass('btn-info')
                        NewController.loadData();
                    }
                    else {
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
                url: 'SaveNew',
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
                        NewController.loadData();
                    }
                }
            });

        });
        $('#btn-Add').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            NewController.resetForm();
        });

        $('.btn-Edit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            NewController.loadDetail(id);
        });

        $('.btn-Delete').off('click').on('click', function () {
            $('#modalDelete').modal('show');
            var id = $(this).data('id');
            $('#hid-ProviderIdDelete').val(id);
            NewController.resetFormDelete();
        });

        $('#btn-ctnCreate').off('click').on('click', function () {
            NewController.resetForm();
            $('.btn-finish').show();
        });
        $('#DrdListNewCategory').off('change').on('change', function () {
            var categoryId = $(this).val();
            NewController.loadData(categoryId);
        });

    }
}

NewController.init();

