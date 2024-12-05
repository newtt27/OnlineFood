$(document).ready(function () {
    
    // Lắng nghe sự kiện khi người dùng nhấn Enter
    $('#searchInput').on('keypress', function (e) {
        if (e.which === 13) { // 13 là mã của phím Enter
            let keyword = $(this).val().trim();
            searchFoods(keyword); // Gọi hàm tìm kiếm
        }
    });
});

// Hàm gọi tìm kiếm và hiển thị kết quả
function searchFoods(keyword) {
    $.ajax({
        url: '/Home/SearchResult', // URL của action SearchResult
        type: 'GET',
        data: { keyword: keyword }, // Truyền từ khóa tìm kiếm qua query string
        success: function (response) {
            // Nếu tìm thấy kết quả, cập nhật danh sách sản phẩm
            $('#foods-container-result').html(response).show();

            // Nếu có kết quả tìm kiếm, ẩn fast-food-menu
            if (response && response.trim() !== "") {
                $('#fast-food-menu').hide();
            } else {
                $('#fast-food-menu').show();
            }
            // Cuộn đến phần kết quả tìm kiếm
            $('html, body').animate({
                scrollTop: $('#foods-container-result').offset().top
            }, 500); // Thời gian cuộn (500ms)
            // Cập nhật URL với từ khóa tìm kiếm
            window.history.pushState(null, '', '/Home/SearchResult?keyword=' + keyword);
        },
        error: function () {
            alert("Có lỗi xảy ra trong quá trình tìm kiếm.");
        }
    });
}
