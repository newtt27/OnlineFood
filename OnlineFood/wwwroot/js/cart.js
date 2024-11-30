let cart = []; // Khởi tạo giỏ hàng rỗng

// Khi trang được tải, giỏ hàng sẽ được lấy từ localStorage
document.addEventListener('DOMContentLoaded', function () {
    loadCartFromLocalStorage();
    updateCartUI();
});

// Hàm thêm sản phẩm vào giỏ hàng
function addToCart(foodId) {
    const quantityInput = event.target.closest('.food-item').querySelector('.quantity');
    const quantity = parseInt(quantityInput.value);

    if (quantity <= 0) {
        showNotification('error', 'Vui lòng chọn số lượng lớn hơn 0');
        return;
    }

    // Gửi yêu cầu đến server
    $.ajax({
        url: '/Home/AddToCart',
        type: 'POST',
        data: { foodId: foodId, quantity: quantity },
        success: function (response) {
            console.log('Phản hồi từ server:', response);

            if (response.success) {
                const item = response.localStorageItem || response.item;
                if (!cart || cart.length === 0) {
                    cart = []; // Đảm bảo `cart` là mảng
                }

                if (item) {
                    const existingItem = cart.find(cartItem => cartItem.id === item.id);
                    if (existingItem) {
                        existingItem.quantity += quantity;
                        showNotification('success', 'Đã cập nhật số lượng trong giỏ hàng');
                    } else {
                        cart.push(item);
                        showNotification('success', 'Đã thêm món ăn vào giỏ hàng');
                    }
                    saveCartToLocalStorage(); // Lưu giỏ hàng vào localStorage
                }

                updateCartUI(); // Cập nhật giao diện giỏ hàng
            } else {
                showNotification('error', response.message || 'Có lỗi xảy ra');
            }
        },
        error: function () {
            showNotification('error', 'Có lỗi xảy ra khi thêm vào giỏ hàng');
        }
    });
}

// Hàm cập nhật giao diện giỏ hàng
function updateCartUI() {
    const cartItems = document.querySelector('.cart-items');
    const cartCountElement = document.getElementById('cart-count'); // Hiển thị số lượng sản phẩm trên biểu tượng giỏ hàng
    let subtotal = 0;


    cartItems.innerHTML = ''; // Xóa nội dung cũ
    cart.forEach(item => {
        const itemTotal = item.price * item.quantity;
        subtotal += itemTotal;

        cartItems.innerHTML += `
            <li class="list-group-item bg-dark text-white border-secondary">
                <div class="d-flex justify-content-between align-items-center">
                    <div class="d-flex align-items-center">
                        <img src="${item.image || '/assets/default-image.jpg'}" alt="${item.name}" style="width: 50px; height: 50px; object-fit: cover;">
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
    // Cập nhật tổng số lượng sản phẩm
    if (cartCountElement) {
        const totalItems = cart.reduce((acc, item) => acc + item.quantity, 0); // Tính tổng số lượng sản phẩm
        cartCountElement.textContent = totalItems; // Gán giá trị cho `cart-count`
    }
    // Cập nhật tổng tiền và số lượng
    const discount = 0; // Logic giảm giá (nếu có)
    const total = subtotal - discount;

    document.getElementById('cart-subtotal').textContent = `${subtotal.toLocaleString()} VND`;
    document.getElementById('cart-discount').textContent = `-${discount.toLocaleString()} VND`;
    document.getElementById('cart-total').textContent = `${total.toLocaleString()} VND`;

    // Hiển thị số lượng sản phẩm trên biểu tượng giỏ hàng
    if (cartCountElement) {
        cartCountElement.textContent = cart.length; // Số lượng sản phẩm trong giỏ hàng
    }
}

// Hàm xóa sản phẩm khỏi giỏ hàng
function removeFromCart(foodId) {
    cart = cart.filter(item => item.id !== foodId);
    saveCartToLocalStorage(); // Cập nhật localStorage
    updateCartUI(); // Cập nhật giao diện
    showNotification('success', 'Đã xóa sản phẩm khỏi giỏ hàng');
}

// Lưu giỏ hàng vào localStorage
function saveCartToLocalStorage() {
    try {
        localStorage.setItem('cart', JSON.stringify(cart));
        console.log('Dữ liệu đã được lưu vào localStorage:', cart);
    } catch (error) {
        console.error('Lỗi khi lưu vào localStorage:', error.message);
    }
}

// Tải giỏ hàng từ localStorage
function loadCartFromLocalStorage() {
    const savedCart = localStorage.getItem('cart');
    if (savedCart) {
        try {
            cart = JSON.parse(savedCart);
            if (!Array.isArray(cart)) {
                cart = []; // Đảm bảo cart luôn là mảng
            }
        } catch (error) {
            console.error('Lỗi khi tải giỏ hàng từ localStorage:', error.message);
            cart = [];
        }
    }
}

// Hàm thông báo (giả lập)
function showNotification(type, message) {
    console.log(`[${type.toUpperCase()}]: ${message}`);
}
