function selectPaymentMethod(method) {
    // Lấy tất cả các nút và bỏ chọn
    const paymentButtons = document.querySelectorAll('.payment-btn');
    paymentButtons.forEach(button => button.classList.remove('selected'));

    // Chọn nút hiện tại
    const selectedButton = document.querySelector(`.payment-btn[onclick="selectPaymentMethod('${method}')"]`);
    selectedButton.classList.add('selected');

    // Ẩn hoặc hiện phần thông tin thẻ
    const cardInfo = document.getElementById('card-info');
    if (method === 'card') {
        cardInfo.style.display = 'block';
        } else {
        cardInfo.style.display = 'none';
        }
}
function selectDeliveryMethod(method) {
    // Lấy tất cả các nút và bỏ chọn
    const deliveryButtons = document.querySelectorAll('.delivery-btn');
    deliveryButtons.forEach(button => button.classList.remove('selected'));

    // Chọn nút hiện tại
    const selectedButton = document.querySelector(`.delivery-btn[onclick="selectDeliveryMethod('${method}')"]`);
    selectedButton.classList.add('selected');

}
function validateForms() {
    // Kiểm tra form contact
    var contactForm = document.getElementById('contactForm');
    var isContactValid = contactForm.checkValidity();  // Kiểm tra tính hợp lệ của form contact
    if (!isContactValid) {
        // Thông báo lỗi cho trường không hợp lệ trong form contact
        alert(getFormValidationErrors(contactForm));
        return;
    }

    // Kiểm tra form paymentMethod
    var paymentForm = document.getElementById('paymentMethodForm');
    var isPaymentValid = paymentForm.checkValidity();  // Kiểm tra tính hợp lệ của form paymentMethod
    if (!isPaymentValid) {
        // Thông báo lỗi cho trường không hợp lệ trong form paymentMethod
        alert(getFormValidationErrors(paymentForm));
        return;
    }

    // Kiểm tra form delivery
    var deliveryForm = document.getElementById('deliveryForm');
    var isDeliveryValid = deliveryForm.checkValidity();  // Kiểm tra tính hợp lệ của form delivery
    if (!isDeliveryValid) {
        // Thông báo lỗi cho trường không hợp lệ trong form delivery
        alert(getFormValidationErrors(deliveryForm));
        return;
    }

    // Nếu tất cả các form hợp lệ, submit chúng
    if (isContactValid && isPaymentValid && isDeliveryValid) {
        alert("Thông tin hợp lệ! Tiến hành thanh toán.");
        // Nếu muốn submit các form sau khi kiểm tra
        contactForm.submit();
        paymentForm.submit();
        deliveryForm.submit();
    } else {
        alert("Vui lòng kiểm tra lại thông tin trước khi thanh toán.");
    }
}

// Hàm kiểm tra các lỗi trong form và trả về thông báo lỗi
function getFormValidationErrors(form) {
    var errorMessages = [];
    var inputs = form.querySelectorAll('input');

    inputs.forEach(function (input) {
        if (!input.validity.valid) {
            var errorMessage = getErrorMessageForInput(input);
            errorMessages.push(errorMessage);
        }
    });

    return errorMessages.join('\n');
}

// Hàm trả về thông báo lỗi cụ thể cho mỗi trường nhập liệu
function getErrorMessageForInput(input) {
    if (input.validity.valueMissing) {
        return input.placeholder + " là bắt buộc.";
    }
    if (input.validity.patternMismatch) {
        return input.placeholder + " không đúng định dạng.";
    }
    if (input.validity.tooShort) {
        return input.placeholder + " quá ngắn.";
    }
    if (input.validity.tooLong) {
        return input.placeholder + " quá dài.";
    }
    return "Thông tin không hợp lệ.";
}
