

var homeController = {
    init: function () {                
        
        homeController.registerEvent();
        homeController.loadData();
      
    },
    registerEvent: function() {
        $(document).on('submit', '.frmUpdateCart', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: 'Update',
                data: $(this).serialize(),
                success: function (data)
                {
                    homeController.loadData();       

                }
            });
        });
        

        $(document).on('submit', '#deletedProduct', function (e) {
            e.preventDefault();
            $.ajax({
                type: 'POST',
                url: 'Delete',
                data: $(this).serialize(),
                success: function (data) {
                    homeController.loadData();

                }
            });
        });
    },
    loadData: function () {
        $.ajax({
            url: 'AjaxCartIndex',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                   var html = '';
                   var data = response.data;
                   var template = $('#data-template').html();
                   var totalQuantity = 0;
                   var totalPrice = 0;
                   var html2 = '';
                   var template1 = $('#data-template1').html();
                   var template2 = $('#last-row').html();
                   $.each(data, function (i, item) {
                       totalQuantity += item.SoLuong;
                       var Price = parseInt(item.SoLuong) * parseInt(item.DonGia);
                       var Price1 = parseInt(item.SoLuong) * parseInt(item.GiaKhuyenMai);
                    
                          
                       html += Mustache.render(template, {
                           Id: item.Id,
                           Ten: item.TenSP,
                           SoLuong: item.SoLuong,
                           DonGia: item.DonGia,                      
                           HinhAnh: item.HinhAnh,
                           ThanhTien: item.ThanhTien,
                           TongThanhTien:item.TongThanhTien})
                      
                       //if (item.GiaKhuyenMai != "") {
                       //    html += Mustache.render(template1, {
                       //        Id: item.Id,
                       //        Ten: item.TenSP,
                       //        SoLuong: item.SoLuong,
                       //        GiaKhuyenMai: item.GiaKhuyenMai,
                       //        HinhAnh: item.HinhAnh,
                       //        ThanhTien: function () {
                       //            return Price1
                       //        }
                       //    });
                       //    totalPrice += Price1;
                       //}
                       html2 = Mustache.render(template2, {
                           TongSoLuong: totalQuantity,
                           TongThanhTien: item.TongThanhTien,
                       })
                    });                
                   $('#table-data').html(html);                  
                   $('#table-data').append(html2);
            }
        });
    }
}
homeController.init();