let currentPaymentMethod = 'card';
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
    // Lấy tất cả các nút và bỏ chọn
    const deliveryButtons = document.querySelectorAll('.delivery-btn');
    deliveryButtons.forEach(button => button.classList.remove('selected'));

    // Chọn nút hiện tại
    const selectedButton = document.querySelector(`.delivery-btn[onclick="selectDeliveryMethod('${method}')"]`);
    selectedButton.classList.add('selected');

}
document.addEventListener('DOMContentLoaded', () => {
    const button = document.querySelector('.checkout-btn');
    if (button) {
        button.addEventListener('click', validateAndSubmit);
    }
});
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

        // Chuyển đến trang khác
        window.location.href = "/Payments/" + currentPaymentMethod;
        
    } else {
        // Nếu không hợp lệ, hiển thị lỗi
        alert("Vui lòng điền đầy đủ thông tin và kiểm tra lại các trường.");
        contactForm.reportValidity(); // Hiển thị lỗi cho form liên hệ
        deliveryForm.reportValidity(); // Hiển thị lỗi cho form giao hàng
    }
}

