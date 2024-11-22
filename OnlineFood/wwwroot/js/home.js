


function ShowDetails(imageUrl, name) {
    document.getElementById("detailImg").src = imageUrl;
    document.getElementById("detailName").Text = name;
}


function loadFoods(page) {
    // Cập nhật `currentPage` với trang được yêu cầu
    currentPage = page;

    $.ajax({
        url: getPagedFoodsUrl, // URL phân trang
        type: 'GET',
        data: { page: page },
        success: function (response) {
            // Cập nhật danh sách món ăn
            $('#foods-container').html(response);

            // Xóa lớp active trên tất cả các nút phân trang
            $('#pagination .page-item').removeClass('active');

            // Thêm lớp active vào nút phân trang của trang hiện tại
            $('#page-' + page).addClass('active');

            // Cập nhật trạng thái của nút "Previous" và "Next"
            updatePaginationButtons();
        },
        error: function () {
            alert("Lỗi khi tải dữ liệu.");
        }
    });
}
//Cập nhật phân trang
function updatePaginationButtons() {
    // Vô hiệu hóa nút "Previous" nếu ở trang đầu tiên
    if (currentPage === 1) {
        $('#previous').addClass('disabled');
    } else {
        $('#previous').removeClass('disabled');
    }

    // Vô hiệu hóa nút "Next" nếu ở trang cuối cùng
    if (currentPage === totalPages) {
        $('#next').addClass('disabled');
    } else {
        $('#next').removeClass('disabled');
    }
}
function showFoodDetails(productId) {
    $.ajax({
        url: getFoodDetailsUrl, // URL gọi tới action để lấy chi tiết
        type: 'GET',
        data: { id: productId },
        success: function (response) {
            // Hiển thị chi tiết sản phẩm trong modal hoặc container được chỉ định
            $('#product-details-container').html(response);
            $('#productDetailsModal').modal('show'); // Hiển thị modal nếu có
        },
        error: function () {
            alert("Không tải được chi tiết sản phẩm.");
        }
    });
}

