

let selectedCategoryId = null; // Biến toàn cục lưu ID danh mục đã chọn

function loadFoods(page = 1, categoryId = null) {
    selectedCategoryId = categoryId; // Cập nhật danh mục hiện tại
    const isCategorySelected = categoryId !== null;

    const url = isCategorySelected
        ? `/Home/GetFoodsByCategory/${categoryId}`
        : getPagedFoodsUrl;

    const data = isCategorySelected
        ? { categoryId: categoryId }
        : { page: page };

    $.ajax({
        url: url,
        type: 'GET',
        data: data,
        success: function (response) {
            
            // Cập nhật danh sách món ăn
            $('#foods-container').html(response);

            // Kiểm tra xem có chọn danh mục hay không
            if (isCategorySelected) {
                // Ẩn phân trang và cho phép scroll
                $('#pagination').css('display', 'none'); // Ẩn bằng cách thay đổi display
                $('#foods-container').css({
                    height: 'auto',  // Điều chỉnh chiều cao
                    overflowY: 'unset' // Bỏ scroll nếu có
                });
            } else {
                // Hiển thị phân trang và cập nhật trạng thái nút
                $('#pagination').css('display', 'block'); // Hiển thị dưới dạng flexbox
                updatePaginationButtons(page);
            }
        },
        error: function () {
            alert("Đã xảy ra lỗi khi tải dữ liệu.");
        }
    });
}

function updatePaginationButtons(page) {
    currentPage = page; // Cập nhật `currentPage` với trang hiện tại

    // Xóa lớp `active` khỏi tất cả các nút phân trang
    $('#pagination .page-item').removeClass('active');

    // Thêm lớp `active` vào nút của trang hiện tại
    $(`#page-${page}`).addClass('active');

    // Cập nhật trạng thái nút "Previous"
    if (currentPage === 1) {
        $('#previous').addClass('disabled');
    } else {
        $('#previous').removeClass('disabled');
    }

    // Cập nhật trạng thái nút "Next"
    if (currentPage === totalPages) {
        $('#next').addClass('disabled');
    } else {
        $('#next').removeClass('disabled');
    }
}


function filterFoodsByCategory(categoryId) {
    // Lấy thông tin từ HTML của danh mục được click
    const categoryElement = $(`div.food-item[onclick="filterFoodsByCategory(${categoryId})"]`);
    const imgSrc = categoryElement.data('img');
    const categoryName = categoryElement.data('name');

    // Cập nhật hình ảnh và tên trong chi tiết món ăn
    $('#detailImg').attr('src', imgSrc);
    $('#detailName').text(categoryName);

    // Tải dữ liệu món ăn thuộc danh mục
    loadFoods(1, categoryId);
}
$(document).ready(function () {
    // Lắng nghe sự kiện click trên các liên kết trong navbar
    $('.nav-link-flex li a').on('click', function (e) {
        e.preventDefault(); // Ngừng hành động mặc định của liên kết

        // Lấy ID của phần cần cuộn đến
        var target = $(this).attr('href').toLowerCase(); // Dùng href để lấy thông tin về phần cần cuộn đến

            // Nếu nhấp vào các phần khác, cuộn trang đến phần tương ứng
            $('html, body').animate({
                scrollTop: $(target).offset().top
            }, 1000); // Thời gian cuộn là 1000ms (1 giây)
        
    });
});


function showFoodDetails(foodId) {
    const modal = new bootstrap.Modal(document.getElementById('foodDetailsModal')); // Lấy modal Bootstrap
    modal.show(); // Hiển thị modal


    $.ajax({
        url: `/Home/GetFoodDetails`, // Đường dẫn API
        type: 'GET',
        data: { id: foodId }, // Gửi id món ăn
        success: function (response) {
            // Thay thế nội dung modal bằng thông tin chi tiết món ăn
            $('#foodDetailsContent').html(response);
        },
        error: function () {
            // Hiển thị lỗi nếu xảy ra vấn đề
            $('#foodDetailsContent').html('<div class="text-center text-danger"><p>Không thể tải thông tin món ăn. Vui lòng thử lại sau.</p></div>');
        }
    });
}

