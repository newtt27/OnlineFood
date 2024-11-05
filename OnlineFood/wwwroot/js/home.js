document.addEventListener('DOMContentLoaded', function () {
    // Hàm mở modal giỏ hàng
    document.getElementById('cartButton').onclick = function () {
        var cartModal = new bootstrap.Modal(document.getElementById('cartModal'));
        cartModal.show();
    };

    // Gán sự kiện cho nút đóng modal
    document.querySelector('.btn-close').onclick = closeCartModal;

    // Đảm bảo rằng các hàm này có sẵn trong phạm vi toàn cục
    window.closeCartModal = closeCartModal;
    window.goToPayment = goToPayment;
});

// Hàm đóng modal giỏ hàng
function closeCartModal() {
    var cartModal = bootstrap.Modal.getInstance(document.getElementById('cartModal'));
    if (cartModal) {
        cartModal.hide();
    }
}

// Hàm chuyển đến trang thanh toán (chỉ là ví dụ)
function goToPayment() {
    // Bạn có thể thay đổi URL hoặc thực hiện hành động khác tại đây
    alert("Chuyển đến trang thanh toán.");
}
