// Lấy tất cả các thẻ <li> trong danh sách food-menu
const menuItems = document.querySelectorAll('.food-menu li');
// Lấy phần tử có class "title" để thay đổi nội dung sau khi click
const titleElement = document.querySelector('.title');
// Lặp qua từng thẻ <li> và lắng nghe sự kiện click
menuItems.forEach(item => {
    item.addEventListener('click', function () {
        const title = this.querySelector('span').textContent;

        titleElement.innerHTML = title;

        // Loại bỏ class "active" khỏi tất cả các mục menu
        menuItems.forEach(i => i.classList.remove('active'));

        this.classList.add('active');
    })
});
