function handleSubmit(event) {
    event.preventDefault(); // Ngăn chặn gửi biểu mẫu mặc định

    // Lấy thông tin từ biểu mẫu
        const name = document.getElementById('name').value;
    const email = document.getElementById('email').value;
    const message = document.getElementById('message').value;

    // Xử lý thông tin (ví dụ: gửi đến máy chủ hoặc hiển thị thông báo)
    // Ở đây, chúng ta chỉ hiển thị thông báo thành công
    const responseMessage = document.getElementById('responseMessage');
    responseMessage.textContent = `Cảm ơn ${name}! Tin nhắn của bạn đã được gửi thành công.`;
    responseMessage.style.display = 'block';

    // Xóa nội dung biểu mẫu
    document.querySelector('.contact-form').reset();

    // Ẩn thông báo sau 3 giây
    setTimeout(() => {
        responseMessage.style.display = 'none';
    }, 3000);
}