function openModal() {
    document.getElementById('bill-detail-container').style.display = 'block';
}

function closeModal() {
    document.getElementById('bill-detail-container').style.display = 'none';
}

// Close modal when clicking outside the content
window.onclick = function (event) {
    const modal = document.getElementById('bill-detail-container');
    if (event.target === modal) {
        closeModal();
    }
};