document.getElementById("creditCardForm").addEventListener("submit", function (e) {
    e.preventDefault();

    // Lấy giá trị từ form
    const cardholderName = document.getElementById("cardholderName").value;
    const cardNumber = document.getElementById("cardNumber").value;
    const expiryDate = document.getElementById("expiryDate").value;
    const cvv = document.getElementById("cvv").value;

    // Kiểm tra tính hợp lệ cơ bản
    if (!cardholderName || !cardNumber || !expiryDate || !cvv) {
        alert("Vui lòng điền đầy đủ thông tin.");
        return;
    }

    // Xử lý logic thanh toán tại đây
    alert("Thông tin thẻ đã được gửi.");
});