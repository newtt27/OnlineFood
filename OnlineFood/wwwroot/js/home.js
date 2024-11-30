

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



