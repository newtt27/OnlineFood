let currentPaymentMethod = 'card';
let PaymentCart = [];
let subtotal = 0;
let total = 0;
let userId = 2; // !

document.addEventListener('DOMContentLoaded', () => {
    const button = document.querySelector('.checkout-btn');
    if (button) {
        button.addEventListener('click', validateAndSubmit);
    }
});
window.onload = fetchCartItems;
// >>>> ĐÃ LẤY ĐƯỢC THÔNG TIN ITEM TỪ CART DỰA TRÊN ĐĂNG NHẬP HOẶC KHÔNG 
function selectPaymentMethod(method) {
    currentPaymentMethod = method;
    // Lấy tất cả các nút và bỏ chọn
    const paymentButtons = document.querySelectorAll('.payment-btn');
    paymentButtons.forEach(button => button.classList.remove('selected'));

    // Chọn nút hiện tại
    const selectedButton = document.querySelector(`.payment-btn[onclick="selectPaymentMethod('${method}')"]`);
    selectedButton.classList.add('selected');
}
function selectDeliveryMethod(method) {
    // Lấy tất cả các nút và     bỏ chọn
    const deliveryButtons = document.querySelectorAll('.delivery-btn');
    deliveryButtons.forEach(button => button.classList.remove('selected'));

    // Chọn nút hiện tại
    const selectedButton = document.querySelector(`.delivery-btn[onclick="selectDeliveryMethod('${method}')"]`);
    selectedButton.classList.add('selected');

}

function validateContactForm() {
    let isValid = true;

    // Name validation
    const name = document.getElementById('name').value.trim();
    const nameError = document.getElementById('nameError');
    if (name.length < 3 || name.length > 50) {
        nameError.textContent = 'Name must be between 3 and 50 characters.';
        isValid = false;
    } else {
        nameError.textContent = '';
    }

    // Phone validation
    const phone = document.getElementById('phone').value.trim();
    const phoneError = document.getElementById('phoneError');
    const phoneRegex = /^\d{10,11}$/;
    if (!phoneRegex.test(phone)) {
        phoneError.textContent = 'Phone number must be 10-11 digits.';
        isValid = false;
    } else {
        phoneError.textContent = '';
    }

    // Email validation
    const email = document.getElementById('email').value.trim();
    const emailError = document.getElementById('emailError');
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        emailError.textContent = 'Invalid email format.';
        isValid = false;
    } else {
        emailError.textContent = '';
    }

    return isValid;
}

function validateDeliveryForm() {
    let isValid = true;

    // Address validation
    const address = document.getElementById('address').value.trim();
    const addressError = document.getElementById('addressError');
    if (address.length < 5 || address.length > 50) {
        addressError.textContent = 'Address must be between 5 and 50 characters.';
        isValid = false;
    } else {
        addressError.textContent = '';
    }

    return isValid;
}
function validateAndSubmit() {
    console.log("Running validateAndSubmit");
    // Lấy tham chiếu tới các form
    const contactForm = document.getElementById("contactForm");
    const deliveryForm = document.getElementById("deliveryForm");

    // Kiểm tra form hợp lệ
    const isContactValid = contactForm.checkValidity();
    const isDeliveryValid = deliveryForm.checkValidity();

    if (isContactValid && isDeliveryValid) {
        // Nếu tất cả đều hợp lệ, gửi dữ liệu
        const formData = {
            name: contactForm.name.value,
            phone: contactForm.phone.value,
            email: contactForm.email.value,
            address: deliveryForm.address.value,
            currentPaymentMethod: currentPaymentMethod
        };
        // Lưu formData vào localStorage
        localStorage.setItem("formData", JSON.stringify(formData));
        // Gửi dữ liệu thanh toán đến server và tạo Bill
        submitPayment(formData, subtotal, total, userId);
        // Chuyển đến trang khác
        
    } else {
        // Nếu không hợp lệ, hiển thị lỗi
        alert("Vui lòng điền đầy đủ thông tin và kiểm tra lại các trường.");
        contactForm.reportValidity(); // Hiển thị lỗi cho form liên hệ
        deliveryForm.reportValidity(); // Hiển thị lỗi cho form giao hàng
    }
}

// Hàm gửi dữ liệu thanh toán đến server
function submitPayment(formData, subtotal, total, userId) {
    // Lấy thông tin phương thức thanh toán và các thông tin cần thiết từ formData
    const paymentData = {
        paymentData: {
            PhuongThucThanhToan: formData.currentPaymentMethod,
            Mota: formData.currentPaymentMethod
        },
        tongTienTruoc: subtotal,
        tongTienSau: total,
        userId: userId
    };
    console.log('Sending Payment Data:', paymentData); // Log dữ liệu gửi đi
    // Gửi request đến server để tạo Bill
    fetch('/Payments/ProcessPayment', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(paymentData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`); // Ném lỗi nếu HTTP không thành công
            }
            return response.json(); // Parse JSON
        })
        .then(data => {
            if (data.success) {
                alert(data.message);
                // Lưu giá trị vào session và chuyển hướng 
                sessionStorage.setItem('totalAmount', total);
                localStorage.clear();
                window.location.href = "/Payments/" + currentPaymentMethod;

            } else {
                alert(data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Đã có lỗi xảy ra, vui lòng thử lại.');
        });
}   
//CART
async function fetchCartItems() {
    try {
        const sessionResponse = await fetch('/Carts/CheckSession');
        const sessionData = await sessionResponse.json();

        if (!sessionData.loggedIn) { //Nếu chưa đăng nhập
            loadCartFromLocalStorage();
            PaymentCart = cart;
            updateCartSummary();
        }

        const cartResponse = await fetch('/Carts/GetCartItems');
        const cartData = await cartResponse.json();

        if (cartData.success) {
            PaymentCart = cartData.items; // Gán dữ liệu giỏ hàng
            console.log("Cart items:", PaymentCart);
            updateCartSummary(); // Gọi hàm để cập nhật giao diện giỏ hàng
        } else {
            alert(cartData.message || "Không thể lấy dữ liệu giỏ hàng.");
        }
    } catch (error) {
        console.error("Error fetching cart items:", error);
        alert("Đã xảy ra lỗi khi lấy dữ liệu giỏ hàng. Vui lòng thử lại.");
    }
}

function updateCartSummary() {
    if (PaymentCart && PaymentCart.length > 0) {
        const productContainer = document.querySelector('.summary .product-item');
        const productsList = document.querySelector('.summary');
        subtotal = 0;
        let totalItems = 0;

        PaymentCart.forEach(item => {
            const productDiv = document.createElement('div');
            productDiv.classList.add('product-item');
            const itemTotal = item.price * item.quantity;
            subtotal += itemTotal;
            totalItems += item.quantity;

            productDiv.innerHTML = `
                <img src="${item.IdFood}" alt="Product Image" class="product-image">
                <div class="product-info">
                    <p>${item.name}</p>
                    <p>Số lượng: ${item.quantity}</p>
                    <p>${item.price} VNĐ</p>
                </div>
            `;
            productsList.insertBefore(productDiv, productsList.querySelector('.promotions'));
        });

        document.getElementById('productCount').textContent = PaymentCart.length;
        discount = 0;
        deliveryPrice = subtotal > 100000 ? 0 : 30000 * (1 - 20 / 100 * totalItems);
        total = subtotal - discount + deliveryPrice;

        document.getElementById('subtotal').textContent = `Subtotal: ${subtotal.toLocaleString()} VND`;
        document.getElementById('discount').textContent = `Discount: -${discount.toLocaleString()} VND`;
        document.getElementById('delivery-price').textContent = `Delivery Price: ${deliveryPrice.toLocaleString()} VND`;
        document.getElementById('total').textContent = `Total: ${total.toLocaleString()} VND`;
    }
}
