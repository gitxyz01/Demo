

var ProductController = {
    init: function () {

        ProductController.loadData();

        ProductController.registerEvent();


    },

    loadData: function (categoryId) {
        $.ajax({
            url: 'LoadData',
            data:{
                categoryId: categoryId
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
                        ProductController.registerEvent();
                    }

                });
                var x=''
                table.clear();
                $.each(data, function (i, item) {
                    if (item.SPHot == true) {
                        x = 'Hot';
                    }
                    else {
                        x = '';
                    }
                    if (item.TrangThai == true) {
                        
                        html = Mustache.render(template, {
                            ProductCode: item.MaSP,
                            ProductName: item.Ten,
                            Image: item.HinhAnh,
                            Quantity: item.SoLuongNhap,
                            Price: item.Gia,
                            Description: item.Mota,
                            Hot:x,
                            CategoryName:item.CategoryProduct,  
                            CreatedDate:item.NgayTao,
                            ModifiedDate:item.NgayChinhSua,
                            ProductId:item.ProductID
                        });
                    }
                    else {
                        html = Mustache.render(template_Lock, {
                            ProductCode: item.MaSP,
                            ProductName: item.Ten,
                            Image: item.HinhAnh,
                            Quantity: item.SoLuongNhap,
                            Price: item.Gia,
                            Description: item.Mota,
                            Hot: x,
                            CategoryName: item.CategoryProduct,
                            CreatedDate: item.NgayTao,
                            ModifiedDate: item.NgayChinhSua,
                            ProductId: item.ProductID
                        });
                    }
                    table.row.add($(html)).draw();
                });
                ProductController.registerEvent();
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
        $('#modalCrUdTitle').removeClass().addClass('btn btn-info').text('Thêm Mới Sản Phẩm');
        $('#hid-ProductID').val('0');
        $('#txt-ProductCode').val('');
        $('#txt-ProductName').val('');
        $('#HinhAnh').val('');
        $('.DrdCategory').val('');
        $('#txt-Quantity').val('');
        $('#txt-Price').val('');
        $('#txt-Description').val('');
        $('#ckeContent').val('');
        $('#ckstatus').prop('checked', true);
        $('#btn-crOrUd').text('Thêm Mới');
        $('#dismisModal').show();
        $('#btn-crOrUd').show();
    },

    loadDetail: function (id) {

        $.ajax({
            url: 'GetDetail',
            data: {
                id: id
            },
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status == true) {
                    var data = response.data;
                    $('#modalCrUdTitle').text('Cập Nhật Sản Phẩm').removeClass().addClass('btn btn-info');
                    $('#hid-ProductID').val(data.ProductID);
                    $('#txt-ProductCode').val(data.MaSP);
                    $('#txt-ProductName').val(data.Ten);
                    $('#HinhAnh').val(data.HinhAnh);                    
                    $('.DrdCategory').val(parseInt(data.CategoryId));
                    $('#txt-Quantity').val(data.SoLuongNhap);
                    $('#txt-Price').val(data.Gia);
                    $('#txt-Description').val(data.Mota);

                    CKEDITOR.instances['ckeContent'].setData(data.NoiDungSanPham);
                    
                    $('#cksHot').prop('checked', data.SPHot);
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
                url: 'DeleteProduct',
                data: $(this).serialize(),
                success: function (response) {

                    if (response.status == true) {
                        $('#resultDelete').addClass('btn-success fa fa-check-square')
                            .text(response.message);
                        $('#submitDelete').hide();
                        $('.btn-reload').removeClass('btn-default')
                            .addClass('btn-info')
                        ProductController.loadData();
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
                url: 'SaveProduct',
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
                        ProductController.loadData();
                    }
                }
            });

        });
        $('#btn-Add').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            ProductController.resetForm();
        });

        $('.btn-Edit').off('click').on('click', function () {
            $('#modalAddUpdate').modal('show');
            var id = $(this).data('id');
            ProductController.loadDetail(id);
        });

        $('.btn-Delete').off('click').on('click', function () {
            $('#modalDelete').modal('show');
            var id = $(this).data('id');
            $('#hid-ProviderIdDelete').val(id);
            ProductController.resetFormDelete();
        });

        $('#btn-ctnCreate').off('click').on('click', function () {
            ProductController.resetForm();
            $('.btn-finish').show();
        });
        $('#DrdListCategory').off('change').on('change', function () {
            var id = $(this).val();
            ProductController.loadData(id);
        });
    }
}

ProductController.init();

