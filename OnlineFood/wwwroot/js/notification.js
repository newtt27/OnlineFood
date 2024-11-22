function showNotification(status, message) {
    const notificationModal = document.getElementById('notification-modal');
    const notificationIcon = document.getElementById('notification-icon');
    const notificationMessage = document.getElementById('notification-message');
    const modalContent = notificationModal.querySelector('.modal-content-notify');

    // Reset classes
    modalContent.classList.remove('success', 'error', 'warning');
    notificationIcon.classList.remove('bi-check-circle-fill', 'bi-x-circle-fill', 'bi-exclamation-triangle-fill');

    // Set content based on status
    switch (status) {
        case 'success':
            modalContent.classList.add('success');
            notificationIcon.classList.add('bi-check-circle-fill');
            break;
        case 'error':
            modalContent.classList.add('error');
            notificationIcon.classList.add('bi-x-circle-fill');
            break;
        case 'warning':
            modalContent.classList.add('warning');
            notificationIcon.classList.add('bi-exclamation-triangle-fill');
            break;
    }

    notificationMessage.textContent = message;
    notificationModal.style.display = 'flex';
    modalContent.classList.add('slide-in');

    // Auto hide after 3 seconds
    setTimeout(() => {
        modalContent.classList.remove('slide-in');
        modalContent.classList.add('slide-out');
        setTimeout(() => {
            notificationModal.style.display = 'none';
            modalContent.classList.remove('slide-out');
        }, 500);
    }, 3000);
}