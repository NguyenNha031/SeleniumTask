> **Test Case Descriptions** :
Đây là mô tả ngắn gọn về các test case.

_LoginTest:

+LoginWithValidCredentials: Đăng nhập vào hệ thống với tài khoản hợp lệ (admin_example/123456), sau đó xác minh đăng nhập thành công bằng cách kiểm tra logo trên trang Dashboard.

+LoginWithInvalidPassword: Đăng nhập với email hợp lệ (admin@example.com) nhưng mật khẩu sai (wrong-password), sau đó xác minh thông báo lỗi "Invalid Login." hiển thị đúng.

_DashboardTest:

+VerifyDashboardElementsDisplay: Đăng nhập vào hệ thống, xác minh trang Dashboard hiển thị đúng với logo chính, sau đó nhấp vào liên kết hồ sơ người dùng và xác minh thao tác thành công.

_ProfileTest:

+ProfileTestCase: Đăng nhập, điều hướng từ Dashboard đến trang Profile, nhấp vào mục "Basic Information", xác minh mục hiển thị đúng, chỉnh sửa thông tin (tên, họ, số điện thoại), và xác minh cập nhật thành công.

_ProjectTest:

+ProjectTestCase: Đăng nhập, điều hướng từ Dashboard đến trang Project, xác minh trang hiển thị đúng, thêm dự án mới với tiêu đề, mức độ ưu tiên và mô tả, sau đó tìm kiếm dự án vừa tạo để xác minh nó xuất hiện trong danh sách.

_RequestsTest:

+RequestsTestCase: Đăng nhập, điều hướng từ Dashboard đến trang Leave Requests, xác minh trang hiển thị đúng, thêm một Leave Type mới, xác minh thêm thành công, tìm kiếm và xóa Leave Type vừa tạo, sau đó xác minh xóa thành công qua thông báo toast.

_TaskTest:

+TaskTestCase: Đăng nhập, điều hướng từ Dashboard đến trang Task, xác minh trang hiển thị đúng, thêm task mới với tiêu đề, tìm kiếm task vừa tạo để xác minh tồn tại, hover và nhấp để xem chi tiết task, cập nhật trạng thái task, và xác minh cập nhật thành công qua thông báo toast.

*Notes:

+Tất cả test case đều đăng xuất sau khi hoàn tất và chụp ảnh màn hình nếu test thất bại.
