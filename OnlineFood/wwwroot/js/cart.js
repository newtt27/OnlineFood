$(document).ready(function () {
    // Khởi tạo modal giỏ hàng
    const cartModal = new bootstrap.Modal(document.getElementById('cartModal'));

    // Nút mở modal giỏ hàng
    $('#cartButton').on('click', function () {
        cartModal.show();
    });

    // Đảm bảo các hàm này có sẵn trong phạm vi toàn cục
    window.closeCartModal = closeCartModal;
    window.goToPayment = goToPayment;
    // Hàm đóng modal giỏ hàng
    function closeCartModal() {
        $('#cartModal').modal('hide');
    }

});

let cart = [];

function addToCart(foodId) {
    const quantityInput = event.target.closest('.food-item').querySelector('.quantity');
    const quantity = parseInt(quantityInput.value);

    if (quantity <= 0) {
        showNotification('error', 'Vui lòng chọn số lượng lớn hơn 0');
        return;
    }

    $.ajax({
        url: '/Home/AddToCart',
        type: 'POST',
        data: { foodId: foodId, quantity: quantity },
        success: function (response) {
            if (response.success) {
                const existingItem = cart.find(item => item.id === response.item.id);
                if (existingItem) {
                    existingItem.quantity += quantity;
                    showNotification('success', 'Đã cập nhật số lượng trong giỏ hàng');
                } else {
                    cart.push(response.item);
                    showNotification('success', 'Đã thêm món ăn vào giỏ hàng');
                }
                updateCartUI();
            } else {
                showNotification('error', response.message || 'Có lỗi xảy ra');
            }
        },
        error: function () {
            showNotification('error', 'Có lỗi xảy ra khi thêm vào giỏ hàng');
        }
    });
}

function updateCartUI() {
    const cartItems = document.querySelector('.cart-items');
    cartItems.innerHTML = '';

    let subtotal = 0;

    cart.forEach(item => {
        const itemTotal = item.price * item.quantity;
        subtotal += itemTotal;

        cartItems.innerHTML += `
            <li class="list-group-item bg-dark text-white border-secondary">
                <div class="d-flex justify-content-between align-items-center">
                    <div class="d-flex align-items-center">
                        <img src="${item.image}" alt="${item.name}" style="width: 50px; height: 50px; object-fit: cover;">
                        <div class="ms-3">
                            <h6 class="mb-0">${item.name}</h6>
                            <small>${item.price.toLocaleString()} VND x ${item.quantity}</small>
                        </div>
                    </div>
                    <button class="btn btn-sm btn-danger" onclick="removeFromCart(${item.id})">
                        <i class="bi bi-trash"></i>
                    </button>
                </div>
            </li>
        `;
    });

    document.getElementById('cart-subtotal').textContent = `${subtotal.toLocaleString()} VND`;
    document.getElementById('cart-total').textContent = `${subtotal.toLocaleString()} VND`;
}

function removeFromCart(foodId) {
    cart = cart.filter(item => item.id !== foodId);
    updateCartUI();
}