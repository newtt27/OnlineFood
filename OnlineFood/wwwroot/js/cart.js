let cart = []; // Khởi tạo giỏ hàng rỗng

// Khi trang được tải, kiểm tra session và tải giỏ hàng
document.addEventListener('DOMContentLoaded', function () {
    checkSessionAndLoadCart();
});

// Hàm kiểm tra session và tải giỏ hàng
function checkSessionAndLoadCart() {
    $.ajax({
        url: '/Carts/CheckSession', // Đường dẫn tới API kiểm tra session
        type: 'GET',
        success: function (response) {
            if (response.loggedIn) {
                // Nếu có tài khoản đăng nhập, tải CartItems từ server
                loadCartItems();
            } else {
                // Nếu không có tài khoản, tải từ localStorage
                loadCartFromLocalStorage();
                updateCartUI();
            }
        },
        error: function () {
            console.error('Không thể kiểm tra trạng thái session');
            loadCartFromLocalStorage(); // Fallback
            updateCartUI();
        }
    });
}


// Hàm tải danh sách CartItems từ server
function loadCartItems() {
    $.ajax({
        url: '/Carts/GetCartItems', // Đường dẫn API lấy giỏ hàng
        type: 'GET',
        success: function (response) {
            if (response.success) {
                cart = response.items; // Gán danh sách sản phẩm từ server
                updateCartUI(); // Cập nhật giao diện giỏ hàng
            } else {
                console.error('Lỗi:', response.message);
            }
        },
        error: function () {
            console.error('Lỗi khi tải giỏ hàng từ server.');
        }
    });
}


// Hàm thêm sản phẩm vào giỏ hàng
function addToCart(foodId) {
    const quantityInput = event.target.closest('.food-item').querySelector('.quantity');
    const quantity = parseInt(quantityInput.value);

    if (quantity <= 0) {
        showNotification('error', 'Vui lòng chọn số lượng lớn hơn 0');
        return;
    }

    // Kiểm tra trạng thái đăng nhập
    $.ajax({
        url: '/Carts/CheckSession',
        type: 'GET',
        success: function (response) {
            if (response.loggedIn) {
                addToCartLoggedIn(foodId, quantity); // Gọi hàm xử lý khi đã đăng nhập
            } else {
                addToCartLocalStorage(foodId, quantity); // Gọi hàm xử lý khi chưa đăng nhập
            }
        },
        error: function () {
            showNotification('error', 'Không thể kiểm tra trạng thái đăng nhập');
        }
    });
}
function addToCartLoggedIn(foodId, quantity) {
    $.ajax({
        url: '/Carts/AddToCart',
        type: 'POST',
        data: { foodId: foodId, quantity: quantity },
        success: function (response) {
            console.log('Phản hồi từ server:', response);
            if (response.success) {
                loadCartItems(); // Tải lại giỏ hàng từ server
                showNotification('success', 'Đã thêm món ăn vào giỏ hàng');
            } else {
                showNotification('error', response.message || 'Có lỗi xảy ra');
            }
        },
        error: function () {
            showNotification('error', 'Không thể thêm sản phẩm vào giỏ hàng');
        }
    });
}
function addToCartLocalStorage(foodId, quantity) {
    $.ajax({
        url: '/Carts/AddToCart',
        type: 'POST',
        data: { foodId: foodId, quantity: quantity },
        success: function (response) {
            console.log('Phản hồi từ server:', response);
            if (response.success) {
                const item = response.localStorageItem || response.item;
                const existingItem = cart.find(cartItem => cartItem.id === item.id);
                if (existingItem) {
                    existingItem.quantity += quantity;
                    showNotification('success', 'Đã cập nhật số lượng trong giỏ hàng');
                } else {
                    cart.push(item);
                    showNotification('success', 'Đã thêm món ăn vào giỏ hàng');
                }
                saveCartToLocalStorage(); // Lưu vào localStorage
                updateCartUI(); // Cập nhật giao diện
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
    const cartItemsContainer = document.querySelector('.cart-items');
    const cartCountElement = document.getElementById('cart-count'); // Phần hiển thị số lượng sản phẩm trên biểu tượng giỏ hàng

    let subtotal = 0;
    let totalItems = 0; // Tổng số lượng sản phẩm

    cartItemsContainer.innerHTML = ''; // Xóa nội dung cũ
    cart.forEach(item => {
        const itemTotal = item.price * item.quantity;
        subtotal += itemTotal;
        totalItems += item.quantity; // Cộng số lượng sản phẩm

        cartItemsContainer.innerHTML += `
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

    const discount = 0; // Logic giảm giá nếu có
    const total = subtotal - discount;

    document.getElementById('cart-subtotal').textContent = `${subtotal.toLocaleString()} VND`;
    document.getElementById('cart-discount').textContent = `-${discount.toLocaleString()} VND`;
    document.getElementById('cart-total').textContent = `${total.toLocaleString()} VND`;

    // Cập nhật số lượng sản phẩm trên biểu tượng giỏ hàng
    if (cartCountElement) {
        cartCountElement.textContent = totalItems; // Hiển thị tổng số lượng sản phẩm
        cartCountElement.style.display = totalItems > 0 ? 'inline' : 'none'; // Ẩn nếu không có sản phẩm
    }
}

function removeFromCart(foodId) {
    // Kiểm tra trạng thái đăng nhập
    $.ajax({
        url: '/Carts/CheckSession', // API kiểm tra session
        type: 'GET',
        success: function (response) {
            if (response.loggedIn) {
                // Nếu có tài khoản đăng nhập
                removeFromCartServer(foodId);
            } else {
                // Nếu không có tài khoản đăng nhập
                removeFromCartLocalStorage(foodId);
            }
        },
        error: function () {
            showNotification('error', 'Không thể kiểm tra trạng thái đăng nhập');
        }
    });
}

// Hàm xóa sản phẩm khỏi database
function removeFromCartServer(foodId) {
    $.ajax({
        url: `/Carts/RemoveFromCart/${foodId}`, // API xóa sản phẩm khỏi database
        type: 'POST',
        data: {foodId: foodId},
        success: function (response) {
            if (response.success) {
                showNotification('success', 'Đã xóa sản phẩm khỏi giỏ hàng');
                loadCartItems(); // Tải lại giỏ hàng từ server sau khi xóa
            } else {
                showNotification('error', response.message || 'Có lỗi xảy ra');
            }
        },
        error: function () {
            showNotification('error', 'Không thể xóa sản phẩm khỏi giỏ hàng');
        }
    });
}

// Hàm xóa sản phẩm khỏi localStorage
function removeFromCartLocalStorage(foodId) {
    const existingItemIndex = cart.findIndex(cartItem => cartItem.id === foodId);
    if (existingItemIndex !== -1) {
        cart.splice(existingItemIndex, 1); // Xóa sản phẩm khỏi mảng
        saveCartToLocalStorage(); // Cập nhật localStorage
        updateCartUI(); // Cập nhật giao diện
        showNotification('success', 'Đã xóa sản phẩm khỏi giỏ hàng');
    } else {
        showNotification('error', 'Sản phẩm không tồn tại trong giỏ hàng');
    }
}


// Lưu giỏ hàng vào localStorage
function saveCartToLocalStorage() {
    localStorage.setItem('cart', JSON.stringify(cart));
}

// Tải giỏ hàng từ localStorage
function loadCartFromLocalStorage() {
    const savedCart = localStorage.getItem('cart');
    if (savedCart) {
        cart = JSON.parse(savedCart);
    }
}

// Hàm thông báo
function showNotification(type, message) {
    console.log(`[${type.toUpperCase()}]: ${message}`);
}

// Khi nhấn vào icon Cart
document.getElementById('cart-icon').addEventListener('click', function () {
    checkSessionAndLoadCart(); // Tự động kiểm tra session và tải giỏ hàng
    $('#cartModal').modal('show'); // Hiển thị modal giỏ hàng
});
