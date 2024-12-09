function openDetailPage(id) {
    // Ví dụ: Kiểm tra nếu ID hợp lệ, sau đó chuyển trang
    if (id) {
        window.location.href = `/details/${id}`;
    } else {
        alert("Invalid ID!");
    }
}

