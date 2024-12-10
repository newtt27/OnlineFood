document.addEventListener("DOMContentLoaded", function () {

    var totalAmount = sessionStorage.getItem('totalAmount');
    document.getElementById('totalAmount').textContent = totalAmount ? totalAmount : "0 VND"; // Hiển thị giá trị trong phần tử có id "totalAmount"

    // Lấy phần tử hiển thị bộ đếm
    var timerElement = document.getElementById('timer');
    var countdown = 60; // Thời gian đếm ngược (60 giây)

    // Cập nhật bộ đếm mỗi giây
    var interval = setInterval(function () {
        countdown--; // Giảm giá trị đếm

        // Cập nhật phần tử hiển thị bộ đếm
        timerElement.textContent = countdown;

        // Nếu bộ đếm hết, dừng và chuyển hướng
        if (countdown <= 0) {
            clearInterval(interval);
            //localStorage.clear();
            window.location.href = '/'; // Chuyển hướng trang
        }
    }, 1000); // Cập nhật mỗi giây (1000ms)
});